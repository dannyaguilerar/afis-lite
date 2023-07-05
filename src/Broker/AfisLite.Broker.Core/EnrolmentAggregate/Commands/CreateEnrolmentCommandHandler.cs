using AfisLite.Broker.Core.EnrolmentAggregate.Models;
using AfisLite.Broker.Core.EnrolmentAggregate.Specifications;
using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.FingerprintAggregate.Models;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.PersonAggregate;
using MediatR;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Commands
{
    public class CreateEnrolmentCommandHandler : IRequestHandler<CreateEnrolmentCommand>
    {
        private readonly IRepository<Enrolment> _enrolmentRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Fingerprint> _fingerprintRepository;
        private readonly IExtractorService _extractorService;
        private readonly IMatcherService _matcherService;

        public CreateEnrolmentCommandHandler(
            IRepository<Enrolment> enrolmentRepository,
            IRepository<Person> personRepository,
            IRepository<Fingerprint> fingerprintRepository,
            IExtractorService extractorService,
            IMatcherService matcherService)
        {
            _enrolmentRepository = enrolmentRepository;
            _personRepository = personRepository;
            _fingerprintRepository = fingerprintRepository;
            _extractorService = extractorService;
            _matcherService = matcherService;

        }

        public async Task Handle(CreateEnrolmentCommand request, CancellationToken cancellationToken)
        {
            /// Extract the fingerprints in base64 and convert them into byte array.
            /// Validation should come from a behaviour in the pipeline.
            var probeFingerprints = new List<FingerprintRecord>();
            foreach (var fp in request.Fingerprints)
            {
                var serialize = Convert.FromBase64String(fp.Data);
                var probeFingerprint = new FingerprintRecord
                {
                    Type = fp.Type,
                    Template = _extractorService.ExtractTemplate(serialize).Template
                };
                probeFingerprints.Add(probeFingerprint);
            }

            var candidates = await _enrolmentRepository.ListAsync(new EnrolmentRecordSpec(), cancellationToken);

            MatcherResponse? response = null;
            var matchedPersonId = 0;
            if (candidates.Any())
            {
                foreach (var candidate in candidates)
                {
                    response = _matcherService.MatcheFingerprints(probeFingerprints, candidate.Fingerprints);
                    if (response.IsMatch)
                    {
                        matchedPersonId = candidate.PersonId;
                        break;
                    }
                }
            }

            var personId = 0;
            var status = EnrolmentStatus.Archived;
            if (response != null && response.IsMatch)
            {
                personId = matchedPersonId;
                status = EnrolmentStatus.Duplicate;
            }
            else
            {
                var person = new Person
                {
                    UniqueId = request.UniqueId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                };
                await _personRepository.AddAsync(person, cancellationToken);
                await _personRepository.SaveChangesAsync(cancellationToken);

                personId = person.Id;
                status = EnrolmentStatus.Principal;
            }


            var enrolment = new Enrolment
            {
                PersonId = personId,
                UniqueId = request.UniqueId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Status = status,
            };
            await _enrolmentRepository.AddAsync(enrolment, cancellationToken);
            await _enrolmentRepository.SaveChangesAsync();

            var fingerprints = probeFingerprints.Select(p => new Fingerprint
            {
                EnrolmentId = enrolment.Id,
                Type = p.Type,
                Template = p.Template,
            });
            await _fingerprintRepository.AddRangeAsync(fingerprints, cancellationToken);
            await _fingerprintRepository.SaveChangesAsync();
        }
    }
}
