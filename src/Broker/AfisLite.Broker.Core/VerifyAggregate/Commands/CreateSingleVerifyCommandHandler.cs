using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.VerifyAggregate.Models;
using MediatR;

namespace AfisLite.Broker.Core.VerifyAggregate.Commands
{
    public class CreateSingleVerifyCommandHandler : IRequestHandler<CreateSingleVerifyCommand, VerifyResponse>
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

        public Task<VerifyResponse> Handle(CreateSingleVerifyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
