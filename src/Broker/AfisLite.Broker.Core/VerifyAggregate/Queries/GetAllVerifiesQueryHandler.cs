using AfisLite.Broker.Core.Interfaces;
using MediatR;

namespace AfisLite.Broker.Core.VerifyAggregate.Queries
{
    public class GetAllVerifiesQueryHandler : IRequestHandler<GetAllVerifiesQuery, IEnumerable<Verify>>
    {
        private readonly IReadRepository<Verify> _verifyReadRepository;

        public GetAllVerifiesQueryHandler(IReadRepository<Verify> verifyReadRepository)
        {
            _verifyReadRepository = verifyReadRepository;
        }

        public async Task<IEnumerable<Verify>> Handle(GetAllVerifiesQuery request, CancellationToken cancellationToken)
        {
            return await _verifyReadRepository.ListAsync();
        }
    }
}
