using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Team
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string? TeamDescription { get; set; }

    public int? TeamLeadId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual User? TeamLead { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}