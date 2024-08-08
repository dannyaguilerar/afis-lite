using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.Shared;
using MediatR;

namespace AfisLite.Broker.Core.EnrolmentAggregate.Commands
{
    public class CreateEnrolmentCommand : BaseRequest, IRequest
    {
        public required string UniqueId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        
        public required IEnumerable<CreateFingerprint> Fingerprints { get; set; }
    }

    public class CreateFingerprint
    {
        public required FingerprintType Type { get; set; }
        public required string Data { get; set; }
    }
}
