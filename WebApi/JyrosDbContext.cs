using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi;

public class JyrosDbContext : DbContext
{
    public JyrosDbContext(DbContextOptions<JyrosDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.User> Users { get; set; }
    public DbSet<Domain.Teams> Teams { get; set; }
    public DbSet<Domain.UserTeams> UserTeams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
        });

        modelBuilder.Entity<Domain.Teams>(entity =>
        {
            entity.HasKey(e => e.TeamId);
            entity.Property(e => e.TeamName).IsRequired();
            entity.Property(e => e.TeamDescription).IsRequired();
            entity.Property(e => e.TeamLeaderId).IsRequired();
        });

        modelBuilder.Entity<Domain.UserTeams>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TeamId });
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
            entity.HasOne(e => e.Team).WithMany().HasForeignKey(e => e.TeamId);

        });
    }

}