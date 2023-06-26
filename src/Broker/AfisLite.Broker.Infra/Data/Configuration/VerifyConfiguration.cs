using AfisLite.Broker.Core.VerifyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AfisLite.Broker.Infra.Data.Configuration
{
    public class VerifyConfiguration : IEntityTypeConfiguration<Verify>
    {
        private const string FK_VERIFY_PERSON_CANDIDATEID = "FK_VERIFY_PERSON_CANDIDATEPERSONID";

        public void Configure(EntityTypeBuilder<Verify> builder)
        {
            builder.Property(v => v.Id)
                .UseIdentityAlwaysColumn();

            builder.Property(v => v.CandidatePersonId)
                .IsRequired();
            builder.HasIndex(v => v.CandidatePersonId);

            builder.HasOne(v => v.Candidate)
                .WithMany(p => p.Verifies)
                .HasForeignKey(v => v.CandidatePersonId)
                .HasPrincipalKey(p => p.Id)
                .HasConstraintName(FK_VERIFY_PERSON_CANDIDATEID);
        }
    }
}
