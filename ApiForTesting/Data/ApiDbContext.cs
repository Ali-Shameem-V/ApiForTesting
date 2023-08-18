using Microsoft.EntityFrameworkCore;

namespace ApiForTesting.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<appuser>(entity =>
            {
                entity.HasKey(e => e.AppUserId);
                entity
                .HasOne(x => x.UserType)
                .WithMany(x => x.appusers)
                .HasForeignKey(x => x.UserTypeId);
                

            });
            modelBuilder.Entity<usertype>(entity =>
            {
                entity.HasKey(a => a.UserTypeId);

            });

        }*/

        public DbSet<usertype> usertypes_shameem { get; set; }
        public DbSet<appuser> appusers_shameem { get; set; }

    }
}
