using AfisLite.Broker.Core.PersonAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfisLite.Broker.Infra.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        private const string ID = "Person_Id";
        private const string UNIQUEID_KEY = "Person_UniqueId";

        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(p => p.Id)
                .UseIdentityAlwaysColumn();            

            builder.Property(p => p.UniqueId)
                .IsRequired();

            builder.HasIndex(p => p.UniqueId)
                .IsUnique()
                .HasDatabaseName(UNIQUEID_KEY);
        }
    }
}
