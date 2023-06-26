using AfisLite.Broker.Core.PersonAggregate;
using Ardalis.Specification;

namespace AfisLite.Broker.Core.PersonAggregate.Specifications
{
    public class PersonByIdSpec : Specification<Person>, ISingleResultSpecification<Person>
    {
        public PersonByIdSpec(int personId)
        {
            Query.Where(person => person.Id == personId);
        }
    }
}
