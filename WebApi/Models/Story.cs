using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Story
{
    public int StoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? EpicId { get; set; }

    public int? SprintId { get; set; }

    public int? CreatedBy { get; set; }

    public int? StoryPoints { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Epic? Epic { get; set; }

    public virtual Sprint? Sprint { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
