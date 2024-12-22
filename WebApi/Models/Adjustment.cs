using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Adjustment
{
    public int Id { get; set; }
    public int SprintId { get; set; }
    public int AdjustmentPoints { get; set; }
    public string Reason { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Sprint? Sprint { get; set; }

}