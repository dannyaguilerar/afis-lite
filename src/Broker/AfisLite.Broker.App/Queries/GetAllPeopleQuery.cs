using AfisLite.Broker.Core.PersonAggregate;
using MediatR;

namespace AfisLite.Broker.App.Queries
{
    public  class GetAllPeopleQuery : IRequest<IEnumerable<Person>>
    {

    }
}
