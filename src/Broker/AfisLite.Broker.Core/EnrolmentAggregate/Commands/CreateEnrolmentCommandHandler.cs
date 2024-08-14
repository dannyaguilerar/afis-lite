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
            var probeFingerprints = ExtractFingerprints(request.Fingerprints);

            var candidates = await _enrolmentRepository.ListAsync(new EnrolmentRecordsSpec(), cancellationToken);
            var matchedPersonId = FindMatchingPersonId(probeFingerprints, candidates);
            var (personId, status) = await GetPersonIdAndStatus(request, matchedPersonId, cancellationToken);

            var enrolment = await CreateEnrolment(request, personId, status, cancellationToken);
            await SaveFingerprints(probeFingerprints, enrolment.Id, cancellationToken);
        }

        private List<FingerprintRecord> ExtractFingerprints(IEnumerable<CreateFingerprint> fingerprints)
        {
            return fingerprints.Select(fp =>
            {
                var serialize = Convert.FromBase64String(fp.Data);
                return new FingerprintRecord
                {
                    Type = fp.Type,
                    Template = _extractorService.ExtractTemplate(serialize).Template
                };
            }).ToList();
        }

        private int FindMatchingPersonId(List<FingerprintRecord> probeFingerprints, IEnumerable<EnrolmentRecord> candidates)
        {
            foreach (var candidate in candidates)
            {
                var response = _matcherService.MatchFingerprints(probeFingerprints, candidate.Fingerprints);
                if (response.IsMatch)
                {
                    return candidate.PersonId;
                }
            }
            return 0;
        }

        private async Task<(int personId, EnrolmentStatus status)> GetPersonIdAndStatus(CreateEnrolmentCommand request, int matchedPersonId, CancellationToken cancellationToken)
        {
            if (matchedPersonId != 0)
            {
                return (matchedPersonId, EnrolmentStatus.Duplicate);
            }

            var person = new Person
            {
                UniqueId = request.UniqueId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
            };
            await _personRepository.AddAsync(person, cancellationToken);
            await _personRepository.SaveChangesAsync(cancellationToken);

            return (person.Id, EnrolmentStatus.Principal);
        }

        private async Task<Enrolment> CreateEnrolment(CreateEnrolmentCommand request, int personId, EnrolmentStatus status, CancellationToken cancellationToken)
        {
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
            await _enrolmentRepository.SaveChangesAsync(cancellationToken);

            return enrolment;
        }

        private async Task SaveFingerprints(List<FingerprintRecord> probeFingerprints, int enrolmentId, CancellationToken cancellationToken)
        {
            var fingerprints = probeFingerprints.Select(p => new Fingerprint
            {
                EnrolmentId = enrolmentId,
                Type = p.Type,
                Template = p.Template,
            });
            await _fingerprintRepository.AddRangeAsync(fingerprints, cancellationToken);
            await _fingerprintRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
