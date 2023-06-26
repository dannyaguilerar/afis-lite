using AfisLite.Broker.Core.PersonAggregate;

namespace AfisLite.Broker.Core.SearchAggregate
{
    public class Search
    {
        public int Id { get; set; }
        public int? MatchPersonId { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime FinishedDate { get; set; }

        public virtual Person? Match { get; set; }
    }
}
