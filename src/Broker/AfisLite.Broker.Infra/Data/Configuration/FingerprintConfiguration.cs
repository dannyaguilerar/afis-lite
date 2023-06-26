using AfisLite.Broker.Core.FingerprintAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfisLite.Broker.Infra.Data.Configuration
{
    public class FingerprintConfiguration : IEntityTypeConfiguration<Fingerprint>
    {
        public void Configure(EntityTypeBuilder<Fingerprint> builder)
        {
            builder.Property(fp => fp.Id)
                .UseIdentityColumn();
            builder.Property(fp => fp.Type)
                .HasConversion(
                    p => ((int)p),
                    p => ((FingerprintType)p)
                );

            builder.HasOne(fp => fp.Enrolment)
                .WithMany(p => p.Fingerprints)
                .HasForeignKey(fp => fp.EnrolmentId)
                .HasPrincipalKey(p => p.Id);
        }
    }
}
