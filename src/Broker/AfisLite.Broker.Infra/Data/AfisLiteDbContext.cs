using AfisLite.Broker.Core.EnrolmentAggregate;
using AfisLite.Broker.Core.FingerprintAggregate;
using AfisLite.Broker.Core.PersonAggregate;
using AfisLite.Broker.Core.SearchAggregate;
using AfisLite.Broker.Core.VerifyAggregate;
using Microsoft.EntityFrameworkCore;

namespace AfisLite.Broker.Infra.Data
{
    public class AfisLiteDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Fingerprint> Fingerprints { get; set; }
        public DbSet<Verify> Verifies { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Search> Searches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "db";
            var database = Environment.GetEnvironmentVariable("DB_DATABASE") ?? "afislite";
            var username = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "afisliteadmin";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "afisliteadmin";

            var connectionString = $"Host={host};Database={database};Username={username};Password={password}";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
