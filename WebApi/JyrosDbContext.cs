using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi;

public class JyrosDbContext : DbContext
{
    public JyrosDbContext(DbContextOptions<JyrosDbContext> options) : base(options)
    {
    }

    public DbSet<Model.User> Users { get; set; }
    public DbSet<Model.Team> Team { get; set; }
    public DbSet<Model.UserTeam> UserTeam { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model.User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
        });

        modelBuilder.Entity<Model.Team>(entity =>
        {
            entity.HasKey(e => e.TeamId);
            entity.Property(e => e.TeamName).IsRequired();
            entity.Property(e => e.TeamDescription).IsRequired();
            entity.Property(e => e.TeamLeaderId).IsRequired();
        });

        modelBuilder.Entity<Model.UserTeam>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TeamId });
            entity.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
            entity.HasOne(e => e.Team).WithMany().HasForeignKey(e => e.TeamId);

        });
    }

}