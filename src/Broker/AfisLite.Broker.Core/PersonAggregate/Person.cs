using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.SearchAggregate;
using AfisLite.Broker.Core.VerifyAggregate;

namespace AfisLite.Broker.Core.PersonAggregate
{
    public class Person : IAggregateRoot
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public virtual ICollection<Enrolment> Enrolments { get; set; } = new HashSet<Enrolment>();
        public virtual ICollection<Search> Searches { get; set; } = new HashSet<Search>();
        public virtual ICollection<Verify> Verifies { get; set; } = new HashSet<Verify>();
    }
}
