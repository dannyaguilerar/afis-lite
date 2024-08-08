using AfisLite.Broker.Core.FingerprintAggregate.Models;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Models
{
    public class EnrolmentRecord
    {
        public int EnrolmentId { get; set; }
        public int PersonId { get; set; }
        public IEnumerable<FingerprintRecord> Fingerprints { get; set; } = [];
    }
}
