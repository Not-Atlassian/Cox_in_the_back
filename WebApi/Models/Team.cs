using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? TeamDescription { get; set; }

    public int? TeamLeadId { get; set; }

    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    public virtual User? TeamLead { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}