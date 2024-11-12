using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Sprint
{
    public int SprintId { get; set; }

    public string Name { get; set; } = null!;

    public string? Goal { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Status { get; set; }

    public int? TeamId { get; set; }

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    public virtual Team? Team { get; set; }
}
