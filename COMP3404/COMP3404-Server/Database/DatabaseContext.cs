using COMP3404_Shared.Models.Accounts;
using COMP3404_Shared.Models.Chats;
using Microsoft.EntityFrameworkCore;

namespace COMP3404_Server.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserAccount> Accounts { get; set; } = null!;

    public DbSet<Chat> Chats { get; set; } = null!;

    public DbSet<ChatMessage> ChatMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence("ChatMessageIds").StartsAt(0).IncrementsBy(1);

        modelBuilder.Entity<UserAccount>(e =>
        {
            e.HasKey(ua => ua.UserAccountId);
        });

        modelBuilder.Entity<Chat>(e =>
        {
            e.HasKey(c => c.ChatId);
            e.HasOne(c => c.OwnerInfo)
                .WithMany(ua => ua.Chats)
                .HasForeignKey(c => c.OwnerId);
            e.HasMany(c => c.Messages)
                .WithOne(cm => cm.ChatInfo)
                .HasForeignKey(cm => cm.ChatId);
        });

        modelBuilder.Entity<ChatMessage>(e =>
        {
            e.HasKey(cm => cm.Id);
            e.Property(cm => cm.Id)
                .HasDefaultValueSql("NEXT VALUE FOR ChatMessageIds");
            e.HasOne(cm => cm.ChatInfo)
                .WithMany(c => c.Messages)
                .HasForeignKey(cm => cm.ChatId);

        });
    }
}
