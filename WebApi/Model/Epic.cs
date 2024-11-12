namespace WebApi.Model;

using System;

public class Epic
{
    // Primary Key epic_id
    public int EpicId { get; set; }
    public String Title { get; set; } = null!;
    public String Description { get; set; } = null!;
    public String Status { get; set; } = "open";
    // Foreign Key created_by referencing Users
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public virtual User User { get; set; } = null!;
}