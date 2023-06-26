using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.Shared;
using MediatR;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Commands
{
    public class CreateEnrolmentCommand : BaseRequest, IRequest
    {
        public string UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public IEnumerable<CreateFingerprint> Fingerprints { get; set; }
    }

    public class CreateFingerprint
    {
        public FingerprintType Type { get; set; }
        public string Data { get; set; }
    }
}
