using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.PersonAggregate;

namespace AfisLite.Broker.Core.EnrolmentAggregate
{
    public class Enrolment : IAggregateRoot
    {
        public int Id { get; set; }

        /// <summary>
        /// This unique for ABIS
        /// </summary>
        public int PersonId { get; set; }
        public EnrolmentStatus Status { get; set; }

        /// <summary>
        /// This unique should be from the bussines of the customer
        /// </summary>
        public string UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<Fingerprint> Fingerprints { get; set; } = new HashSet<Fingerprint>();
    }
}
