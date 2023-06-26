using AfisLite.Broker.Core.EnrolmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfisLite.Broker.Infra.Data.Configuration
{
    public class EnrolmentConfiguration : IEntityTypeConfiguration<Enrolment>
    {
        public void Configure(EntityTypeBuilder<Enrolment> builder)
        {
            builder.Property(e => e.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(fp => fp.Status)
                .HasConversion(
                    p => ((int)p),
                    p => ((EnrolmentStatus)p)
                ).IsRequired();
        }
    }
}
