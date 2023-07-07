using AfisLite.Broker.Core.Shared;

namespace AfisLite.Broker.Core.VerifyAggregate.Models
{
    public abstract class VerificationResponse : BaseResponse
    {
        public abstract bool IsMatch { get; }
        public abstract double Score { get; }
        public abstract int PersonId { get; }
    }
}
