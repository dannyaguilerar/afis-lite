﻿using AfisLite.Broker.Core.EnrolmentAggregate.Models;
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

        public CreateEnrolmentCommandHandler(
            IRepository<Enrolment> enrolmentRepository,
            IRepository<Person> personRepository,
            IRepository<Fingerprint> fingerprintRepository,
            IExtractorService extractorService)
        {
            _enrolmentRepository = enrolmentRepository;
            _personRepository = personRepository;
            _fingerprintRepository = fingerprintRepository;
            _extractorService = extractorService;
        }

        public async Task Handle(CreateEnrolmentCommand request, CancellationToken cancellationToken)
        {
            var probe = new List<FingerprintRecord>();
            foreach (var fp in request.Fingerprints)
            {
                var serialize = Convert.FromBase64String(fp.Data);
                var probeFingerprint = new FingerprintRecord
                {
                    Type = fp.Type,
                    Template = _extractorService.ExtractTemplate(serialize).Template
                };
                probe.Add(probeFingerprint);
            }

            var candidates = await _enrolmentRepository.ListAsync(new EnrolmentRecordSpec(), cancellationToken);

            double score = 0;
            if (candidates.Any())
            {
                foreach (var candidate in candidates)
                {

                }
            } 
            
            if (candidates.Any() || score >= 40)
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
                
                var enrolment = new Enrolment
                {
                    PersonId = person.Id,
                    UniqueId = request.UniqueId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Status = EnrolmentStatus.Principal,
                };
                await _enrolmentRepository.AddAsync(enrolment, cancellationToken);
                await _enrolmentRepository.SaveChangesAsync();

                var fingerprints = probe.Select(p => new Fingerprint
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
}
