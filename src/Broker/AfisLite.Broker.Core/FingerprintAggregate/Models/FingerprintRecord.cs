namespace AfisLite.Broker.Core.FingerprintAggregate.Models
{
    public class FingerprintRecord
    {
        public FingerprintType Type { get; set; }
        public byte[] Template { get; set; }
    }
}
