using Craftify.Domain.Constants;
using Craftify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Craftify.Infrastructure.Presistence
{
    public class CraftifyDbContext(DbContextOptions<CraftifyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Domain.Entities.Authentication> Authentications { get; set; } = null!;

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Booking> Bookings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), FirstName="ADMIN",Role=AppConstants.Role_Admin,EmailConfirmed=true,Email= "craftify.onion0.122@gmail.com",PasswordHash="pass@FY04"}
            );


            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(w => w.PerHourPrice)
                    .HasPrecision(18, 2); // Adjust precision and scale as needed
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(sp => sp.Price)
                    .HasPrecision(18, 2); // Adjust precision and scale as needed
            });


            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Provider)
                .WithMany()
                .HasForeignKey(b => b.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }



    }


}
