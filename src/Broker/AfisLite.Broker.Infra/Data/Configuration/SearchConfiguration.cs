using AfisLite.Broker.Core.SearchAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfisLite.Broker.Infra.Data.Configuration
{
    public class SearchConfiguration : IEntityTypeConfiguration<Search>
    {
        public void Configure(EntityTypeBuilder<Search> builder)
        {
            builder.Property(s => s.Id)
                .UseIdentityAlwaysColumn();

            builder.HasOne(s => s.Match)
                .WithMany(p => p.Searches)
                .HasForeignKey(s => s.MatchPersonId)
                .HasPrincipalKey(p => p.Id);

        }
    }
}
