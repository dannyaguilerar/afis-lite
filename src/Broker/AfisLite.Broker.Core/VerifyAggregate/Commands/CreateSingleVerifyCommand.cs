using AfisLite.Broker.Core.VerifyAggregate.Models;
using MediatR;

namespace AfisLite.Broker.Core.VerifyAggregate.Commands
{
    public class CreateSingleVerifyCommand : IRequest<VerificationResponse>
    {
        public required int CandidatePersonId { get; set; }
        public required string ProbeBase64 { get; set; }        
    }
}
