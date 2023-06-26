using AfisLite.Broker.Core.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace AfisLite.Broker.Infra.Data
{
    public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public Repository(AfisLiteDbContext dbContext) : base(dbContext)
        {
                
        }
    }
}
