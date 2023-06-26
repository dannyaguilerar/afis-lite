using AfisLite.Broker.Core.Shared;
using MediatR;

namespace AfisLite.Broker.Core.PersonAggregate.Queries
{
    public  class GetAllPeopleQuery : BaseRequest, IRequest<IEnumerable<Person>>
    {

    }
}
