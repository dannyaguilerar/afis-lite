using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.PersonAggregate;

namespace AfisLite.Broker.Core.FingerprintAggregate
{
    public class Fingerprint : IAggregateRoot
    {
        public int Id { get; set; }
        public int EnrolmentId { get; set; }
        public FingerprintType Type { get; set; }
        public byte[] Template { get; set; }

        public virtual Enrolment? Enrolment { get; set; }
    }
}
