using System;

namespace WebApi.Model;

public class Teams
{
    // Primary Key team_id
    public int TeamId { get; set; }

    public String TeamName { get; set; } = null!;

    public String TeamDescription { get; set; } = null!;

    // Foreign Key team_leader_id
    public int TeamLeaderId { get; set; }

}