using AfisLite.Broker.Core.Interfaces;
using MediatR;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Queries
{
    public class GetAllEnrolmentsQueryHandler : IRequestHandler<GetAllEnrolmentsQuery, IEnumerable<Enrolment>>
    {
        private readonly IReadRepository<Enrolment> _enrolmentReadRepository;

        public GetAllEnrolmentsQueryHandler(IReadRepository<Enrolment> enrolmentReadRepository)
        {
            _enrolmentReadRepository = enrolmentReadRepository;
        }

        public async Task<IEnumerable<Enrolment>> Handle(GetAllEnrolmentsQuery request, CancellationToken cancellationToken)
        {
            return await _enrolmentReadRepository.ListAsync(cancellationToken);
        }
    }
}
