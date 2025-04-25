using COMP3404_Shared.Models.Accounts;
using COMP3404_Shared.Models.Chats;
using Microsoft.EntityFrameworkCore;

namespace COMP3404_Server.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<UserAccount> Accounts { get; set; } = null!;

    public DbSet<Chat> Chats { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence("ChatMessageIds").StartsAt(0).IncrementsBy(1);

        modelBuilder.Entity<UserAccount>(e =>
        {
            e.HasKey(ua => ua.AccountId);
        });

        modelBuilder.Entity<Chat>(e =>
        {
            e.HasKey(c => c.ChatName);
            e.HasKey(c => c.OwnerId);
            e.HasOne(c => c.OwnerInfo)
                .WithMany(ua => ua.Chats)
                .HasForeignKey(c => c.OwnerId);
            e.HasMany(c => c.Messages);
        });

        modelBuilder.Entity<ChatMessage>(e =>
        {
            e.HasKey(cm => cm.Id);
            e.Property(cm => cm.Id)
                .HasDefaultValueSql("nextval('\"ChatMessageIds\"')");

        });
    }
}
