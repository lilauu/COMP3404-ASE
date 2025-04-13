﻿using Microsoft.EntityFrameworkCore;
using COMP3404_Server.Models;
using COMP3404_Shared.Models.Accounts;

namespace COMP3404_Server.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<UserAccount> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>(e =>
        {
            e.HasKey(ua => ua.AccountId);
        });
    }
}
