using Ardalis.Specification;

namespace AfisLite.Broker.Core.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {

    }
}
