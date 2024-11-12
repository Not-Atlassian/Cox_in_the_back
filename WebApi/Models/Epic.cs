using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class Epic
{
    public int EpicId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
}
