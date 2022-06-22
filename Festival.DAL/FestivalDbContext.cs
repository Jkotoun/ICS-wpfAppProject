using Festival.DAL.Entities;
using Festival.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Festival.DAL
{
    public class FestivalDbContext:DbContext
    {
        public FestivalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BandEntity> Bands { get; set; }

        public DbSet<EventEntity> Events { get; set; }

        public DbSet<StageEntity> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StageEntity>()
                .HasMany<EventEntity>(i => i.Events)
                .WithOne(i => i.Stage)
                .HasForeignKey(s=>s.StageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BandEntity>()
                .HasMany<EventEntity>(i => i.Events)
                .WithOne(i => i.Band)   
                .HasForeignKey(b =>b.BandId)
                .OnDelete(DeleteBehavior.Cascade);

            StageSeeds.Seed(modelBuilder);
            BandSeeds.Seed(modelBuilder);
            EventSeeds.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
