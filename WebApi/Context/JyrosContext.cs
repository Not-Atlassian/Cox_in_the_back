using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class JyrosContext : DbContext
{
    public JyrosContext()
    {
        
    }

    public JyrosContext(DbContextOptions<JyrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.SprintId).HasName("PK__Sprints__396C1802EFA36E05");

            entity.Property(e => e.SprintId).HasColumnName("sprint_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Goal)
                .HasMaxLength(50)
                .HasColumnName("goal");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("planned")
                .HasColumnName("status");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__Stories__66339C56B86254B8");

            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.SprintId).HasColumnName("sprint_id");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("open")
                .HasColumnName("status");
            entity.Property(e => e.StoryPoints).HasColumnName("story_points");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__F82DEDBCAAB0C3AF");

            entity.HasIndex(e => e.TeamName, "UQ__Teams__29E35E0C9F1894C7").IsUnique();

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamDescription)
                .HasMaxLength(500)
                .HasColumnName("team_description");
            entity.Property(e => e.TeamLeadId).HasColumnName("team_lead_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .HasColumnName("team_name");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FA0824176");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572843AEA6F").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}