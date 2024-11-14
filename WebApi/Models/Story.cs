using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Story
{
    public int StoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? ParentId { get; set; }

    public int? SprintId { get; set; }

    public int? CreatedBy { get; set; }

    public int? StoryPoints { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]

    public virtual User? CreatedByNavigation { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]

    public virtual ICollection<Story> InverseParent { get; set; } = new List<Story>();
    [System.Text.Json.Serialization.JsonIgnore]

    public virtual Story? Parent { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]

    public virtual Sprint? Sprint { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
