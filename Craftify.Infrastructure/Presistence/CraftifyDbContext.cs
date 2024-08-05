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

        public DbSet<Message> Messages { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<MessageMedia> MessageMedia { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = Guid.NewGuid(), FirstName="ADMIN",Role=AppConstants.Role_Admin,EmailConfirmed=true,Email= "craftify.onion0.122@gmail.com",PasswordHash="pass@FY04"}
            );

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.RoomId).IsUnique();

                entity.HasOne(c => c.PeerOne)
                    .WithMany()
                    .HasForeignKey(c => c.PeerOneId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.PeerTwo)
                    .WithMany()
                    .HasForeignKey(c => c.PeerTwoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(c => c.IsBlocked).HasDefaultValue(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(m => m.Conversation)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(m => m.ConversationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(m => m.IsRead).HasDefaultValue(false);
            });

            modelBuilder.Entity<MessageMedia>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(mm => mm.Message)
                    .WithMany(m => m.Media)
                    .HasForeignKey(mm => mm.MessageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
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


        }



    }


}
