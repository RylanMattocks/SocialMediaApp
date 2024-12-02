using DropTablesSocial.Models;
using Microsoft.EntityFrameworkCore;

namespace DropTablesSocial.Data;

public class DropTablesContext : DbContext{
    public DropTablesContext() : base() {}
    public DropTablesContext(DbContextOptions<DropTablesContext> options) : base(options) {}
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<User>()
            .HasMany(u => u.Likes)
            .WithMany(p => p.Likes)
            .UsingEntity<Dictionary<string, object>>(
                "UserLikes",
                ul => ul.HasOne<Post>().WithMany().HasForeignKey("PostId").OnDelete(DeleteBehavior.Restrict),
                ul => ul.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade))
                .HasKey("UserId", "PostId");
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany(u => u.Following)
            .UsingEntity<Dictionary<string, object>>(
                "Follows",
                f => f.HasOne<User>().WithMany().HasForeignKey("FollowerId").OnDelete(DeleteBehavior.Restrict),
                f => f.HasOne<User>().WithMany().HasForeignKey("FolloweeId").OnDelete(DeleteBehavior.Cascade))
                .HasKey("FollowerId", "FolloweeId");
        
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}