﻿using Craftify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Craftify.Infrastructure.Presistence
{
    public class CraftifyDbContext(DbContextOptions<CraftifyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Domain.Entities.Authentication> Authentications { get; set; } = null!;

    }
}
