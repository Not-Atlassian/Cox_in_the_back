using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class TeamMemberAvailability
{  
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SprintId { get; set; }

    public int AvailabilityPoints { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Sprint? Sprint { get; set; }

}
