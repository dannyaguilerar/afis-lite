using AfisLite.Broker.Core.Shared;
using MediatR;

namespace AfisLite.Broker.Core.VerifyAggregate.Queries
{
    public class GetAllVerifiesQuery : BaseRequest, IRequest<IEnumerable<Verify>>
    {

    }
}
