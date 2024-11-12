namespace WebApi.Model;

public class Sprint
{
    // Primary Key sprint_id
    public int SprintId { get; set; }
    public String Name { get; set; } = null!;
    public String Goal { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public String Status { get; set; } = "planned";

    // Foreign Key team_id referencing Team
    public int TeamId { get; set; }

    // Navigation properties
    public virtual Team Team { get; set; } = null!;
}