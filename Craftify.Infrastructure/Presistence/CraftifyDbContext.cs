using Craftify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Craftify.Infrastructure.Presistence
{
    public class CraftifyDbContext(DbContextOptions<CraftifyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Domain.Entities.Authentication> Authentications { get; set; } = null!;

        public DbSet<Service> Services { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Construction",
                    MaximumPrice = 2000,
                    MinmumPrice = 1000
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Electrical",
                    MaximumPrice = 2000,
                    MinmumPrice = 1000
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Plumbing",
                    MaximumPrice = 2000,
                    MinmumPrice = 1000
                }

                );
        }
    }
}
