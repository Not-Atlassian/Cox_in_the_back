namespace WebApi.Model;

using System;

public class Team
{
    // Primary Key team_id
    public int TeamId { get; set; }

    public String TeamName { get; set; } = null!;

    public String TeamDescription { get; set; } = null!;

    // Foreign Key team_leader_id
    public int TeamLeaderId { get; set; }

}