using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Sprint
{
    [System.Text.Json.Serialization.JsonIgnore]

    public int SprintId { get; set; }

    public string Name { get; set; } = null!;

    public string? Goal { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Status { get; set; }

    public int? TeamId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<TeamMemberAvailability> TeamMemberAvailabilities { get; set; } = new List<TeamMemberAvailability>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Adjustment> Adjustments { get; set; } = new List<Adjustment>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Team? Team { get; set; }
}