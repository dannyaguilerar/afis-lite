using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.EnrolmentAggregate.Models;
using AfisLite.Broker.Core.EnrolmentAggregate.Specifications;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.VerifyAggregate.Models;
using AfisLite.Broker.Core.VerifyAggregate.Specifications;
using MediatR;

namespace AfisLite.Broker.Core.VerifyAggregate.Commands
{
    public class CreateSingleVerifyCommandHandler : IRequestHandler<CreateSingleVerifyCommand, VerificationResponse>
    {
        private readonly IRepository<Verify> _verifyRepository;
        private readonly IReadRepository<Enrolment> _enrolmentReadRepository;
        private readonly IExtractorService _extractorService;
        private readonly IMatcherService _matcherService;

        public CreateSingleVerifyCommandHandler(
            IReadRepository<Enrolment> enrolmentReadRepository,
            IRepository<Verify> verifyRepository,
            IExtractorService extractorService,
            IMatcherService matcherService)
        {
            _enrolmentReadRepository = enrolmentReadRepository;
            _verifyRepository = verifyRepository;
            _extractorService = extractorService;
            _matcherService = matcherService;
        }

        public async Task<VerificationResponse> Handle(CreateSingleVerifyCommand request, CancellationToken cancellationToken)
        {            
            var starDate = DateTime.UtcNow;
            var template = ExtractTemplateFromRequest(request.ProbeBase64);

            var canditate = await GetEnrolmentRecordWithPersonId(request.CandidatePersonId, cancellationToken);
            var result = _matcherService.MatchProbeTemplateWithCandidateTemplates(template.Template, canditate.Fingerprints.Select(fp => fp.Template).ToList());
            var endDate = DateTime.UtcNow;
            
            await SaveVerify(new Verify
            {
                CandidatePersonId = request.CandidatePersonId,
                Score = result.Score,
                StartedDate = starDate,
                FinishedDate = endDate,
            }, cancellationToken);

            var response = new VerificationSuccessResponse(result.IsSuccess, result.Score, request.CandidatePersonId);
            return response;
        }

        private ExtractorResponse ExtractTemplateFromRequest(string probeBase64)
        {
            var serialize = Convert.FromBase64String(probeBase64);
            return _extractorService.ExtractTemplate(serialize);
        }

        private async Task<EnrolmentRecord> GetEnrolmentRecordWithPersonId(int personId, CancellationToken token)
        {
            var spec = new GetEnrolmentRecordByPersonIdSpec(personId);
            var record = await _enrolmentReadRepository.FirstOrDefaultAsync(spec, token);
            return record;
        }

        private async Task SaveVerify(Verify verify, CancellationToken token)
        {
            await _verifyRepository.AddAsync(verify, token);
        }
    }
}
