using AfisLite.Broker.Core.Interfaces;
using AfisLite.Broker.Core.PersonAggregate;

namespace AfisLite.Broker.Core.VerifyAggregate
{
    public class Verify : IAggregateRoot
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int CandidatePersonId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime FinishedDate { get; set; }

        public virtual Person? Candidate { get; set; }
    }
}
