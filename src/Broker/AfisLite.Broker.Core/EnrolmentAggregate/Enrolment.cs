using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.PersonAggregate;

namespace AfisLite.Broker.Core.EnrolmentAggregate
{
    public class Enrolment : IAggregateRoot
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public EnrolmentStatus Status { get; set; }
        public string UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Fingerprint> Fingerprints { get; set; } = new HashSet<Fingerprint>();
    }
}
