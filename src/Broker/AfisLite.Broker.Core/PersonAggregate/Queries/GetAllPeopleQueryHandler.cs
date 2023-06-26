using AfisLite.Broker.Core.Interfaces;
using MediatR;

namespace AfisLite.Broker.Core.PersonAggregate.Queries
{
    public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<Person>>
    {
        private readonly IReadRepository<Person> _personReadRepository;
        public GetAllPeopleQueryHandler(IReadRepository<Person> personReadRepository)
        {
           _personReadRepository = personReadRepository;
        }

        public async Task<IEnumerable<Person>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        {
            return await _personReadRepository.ListAsync(cancellationToken);
        }
    }
}
