using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.EnrolmentAggregate.Specifications;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.VerifyAggregate.Models;
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
            var serialize = Convert.FromBase64String(request.ProbeBase64);
            var template = _extractorService.ExtractTemplate(serialize);

            var candidates = await _enrolmentReadRepository.ListAsync(new EnrolmentRecordSpec(), cancellationToken);

            var canditate = candidates.FirstOrDefault(c => c.PersonId == request.CandidatePersonId);

            var result = _matcherService.MatchTemplates(template.Template, canditate.Fingerprints.FirstOrDefault().Template);


            throw new NotImplementedException();            
        }
    }
}
