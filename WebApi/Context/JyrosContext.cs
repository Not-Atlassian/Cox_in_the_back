using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Context;

public partial class JyrosContext : DbContext
{
    public JyrosContext()
    {
    }

    public JyrosContext(DbContextOptions<JyrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Epic> Epics { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Epic>(entity =>
        {
            entity.HasKey(e => e.EpicId).HasName("PK__Epics__22CB43A9240A2A1B");

            entity.Property(e => e.EpicId).HasColumnName("epic_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("open")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Epics)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Epics__created_b__6EF57B66");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.SprintId).HasName("PK__Sprints__396C1802B3E500A8");

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

            entity.HasOne(d => d.Team).WithMany(p => p.Sprints)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Sprints__team_id__693CA210");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__Stories__66339C56985E229F");

            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.EpicId).HasColumnName("epic_id");
            entity.Property(e => e.SprintId).HasColumnName("sprint_id");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasDefaultValue("open")
                .HasColumnName("status");
            entity.Property(e => e.StoryPoints).HasColumnName("story_points");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Stories)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Stories__created__76969D2E");

            entity.HasOne(d => d.Epic).WithMany(p => p.Stories)
                .HasForeignKey(d => d.EpicId)
                .HasConstraintName("FK__Stories__epic_id__74AE54BC");

            entity.HasOne(d => d.Sprint).WithMany(p => p.Stories)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK__Stories__sprint___75A278F5");

            entity.HasMany(d => d.Users).WithMany(p => p.StoriesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersStory",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersStor__user___7A672E12"),
                    l => l.HasOne<Story>().WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersStor__story__797309D9"),
                    j =>
                    {
                        j.HasKey("StoryId", "UserId").HasName("PK__UsersSto__8DA87F26802D0296");
                        j.ToTable("UsersStories");
                        j.IndexerProperty<int>("StoryId").HasColumnName("story_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__F82DEDBC0D00CC43");

            entity.HasIndex(e => e.TeamName, "UQ__Teams__29E35E0CF4BC3028").IsUnique();

            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamDescription)
                .HasMaxLength(500)
                .HasColumnName("team_description");
            entity.Property(e => e.TeamLeadId).HasColumnName("team_lead_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .HasColumnName("team_name");

            entity.HasOne(d => d.TeamLead).WithMany(p => p.Teams)
                .HasForeignKey(d => d.TeamLeadId)
                .HasConstraintName("FK__Teams__team_lead__60A75C0F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F971719A8");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572FDC0763A").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasMany(d => d.TeamsNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersTeam",
                    r => r.HasOne<Team>().WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersTeam__team___6477ECF3"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersTeam__user___6383C8BA"),
                    j =>
                    {
                        j.HasKey("UserId", "TeamId").HasName("PK__UsersTea__663CE9D41025393E");
                        j.ToTable("UsersTeams");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("TeamId").HasColumnName("team_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
