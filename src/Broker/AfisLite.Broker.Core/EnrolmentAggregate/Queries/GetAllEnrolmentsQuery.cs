using AfisLite.Broker.Core.Shared;
using MediatR;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Queries
{
    public class GetAllEnrolmentsQuery : BaseRequest, IRequest<IEnumerable<Enrolment>>
    {

    }
}
