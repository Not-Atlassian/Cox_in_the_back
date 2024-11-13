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

            entity.HasOne(d => d.Team).WithMany(p => p.Sprints)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Sprints__team_id__693CA210");

            //story sprint relation
            entity.HasMany(d => d.Stories).WithOne(p => p.Sprint)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK__Stories__sprint___6EF57B66");
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Stories)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Stories__created__6FE99F9F");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Stories__parent___70DDC3D8");

            entity.HasOne(d => d.Sprint).WithMany(p => p.Stories)
                .HasForeignKey(d => d.SprintId)
                .HasConstraintName("FK__Stories__sprint___6EF57B66");

            entity.HasMany(d => d.Users).WithMany(p => p.StoriesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersStory",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersStor__user___74AE54BC"),
                    l => l.HasOne<Story>().WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersStor__story__73BA3083"),
                    j =>
                    {
                        j.HasKey("StoryId", "UserId").HasName("PK__UsersSto__8DA87F2683494916");
                        j.ToTable("UsersStories");
                        j.IndexerProperty<int>("StoryId").HasColumnName("story_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
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

            entity.HasOne(d => d.TeamLead).WithMany(p => p.Teams)
                .HasForeignKey(d => d.TeamLeadId)
                .HasConstraintName("FK__Teams__team_lead__60A75C0F");

            entity.HasMany(d => d.Users).WithMany(p => p.Teams)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersTeam",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersTeam__user___6477ECF3"),
                    l => l.HasOne<Team>().WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UsersTeam__team___6383C8BA"),
                    j =>
                    {
                        j.HasKey("UserId", "TeamId").HasName("PK__UsersTea__663CE9D4131E7ED3");
                        j.ToTable("UsersTeams");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("TeamId").HasColumnName("team_id");
                    });

            entity.HasMany(d => d.Sprints).WithOne(p => p.Team)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Sprints__team_id__693CA210");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FA0824176");

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC572843AEA6F").IsUnique();

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
                        j.HasKey("UserId", "TeamId").HasName("PK__UsersTea__663CE9D4131E7ED3");
                        j.ToTable("UsersTeams");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("TeamId").HasColumnName("team_id");
                    });

            entity.HasMany(d => d.StoriesNavigation).WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UsersStory",
                r => r.HasOne<Story>().WithMany()
                    .HasForeignKey("StoryId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersStor__story__74AE54BC"),
                l => l.HasOne<User>().WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsersStor__user___73BA3083"),
                j =>
                {
                    j.HasKey("UserId", "StoryId").HasName("PK__UsersSto__8DA87F2683494916");
                    j.ToTable("UsersStories");
                    j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    j.IndexerProperty<int>("StoryId").HasColumnName("story_id");
                });

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}