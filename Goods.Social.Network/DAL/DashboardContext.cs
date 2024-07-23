using DomainModel.Entities.Dashboard;
using Goods.System.Social.Network.DomainModel.Entities.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goods.System.Social.Network.DAL
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<CountUsers> CountUsers { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>().HaveConversion<DateTime>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visit>(VisitConfigure);
        }
        public void VisitConfigure(EntityTypeBuilder<Visit> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Date).IsRequired();
        }
        public void CountUsersConfigure(EntityTypeBuilder<CountUsers> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Date).IsRequired();
            builder.HasKey(p => p.Count);
        }
    }
}
