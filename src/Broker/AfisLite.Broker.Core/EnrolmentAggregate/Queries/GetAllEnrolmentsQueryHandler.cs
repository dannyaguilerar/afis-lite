using AfisLite.Broker.Core.Interfaces;
using MediatR;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Queries
{
    public class GetAllEnrolmentsQueryHandler(IReadRepository<Enrolment> enrolmentReadRepository) : IRequestHandler<GetAllEnrolmentsQuery, IEnumerable<Enrolment>>
    {
        public async Task<IEnumerable<Enrolment>> Handle(GetAllEnrolmentsQuery request, CancellationToken cancellationToken)
        {
            return await enrolmentReadRepository.ListAsync(cancellationToken);
        }
    }
}
