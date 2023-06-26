using Ardalis.Specification;

namespace AfisLite.Broker.Core.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {

    }
}
