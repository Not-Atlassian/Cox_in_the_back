namespace WebApi.Model;

using System;

public class Story
{
    /*CREATE TABLE Stories (
    story_id INT IDENTITY(1,1),
    title NVARCHAR(50) NOT NULL,
    description NVARCHAR(1000),
    status NVARCHAR(15) DEFAULT 'open' CHECK (status IN ('open', 'in progress', 'completed')),
    epic_id INT,
    sprint_id INT,
    created_by INT,
    story_points INT CHECK (story_points >= 0),

	PRIMARY KEY(story_id),
    FOREIGN KEY (epic_id) REFERENCES Epics(epic_id),
    FOREIGN KEY (sprint_id) REFERENCES Sprints(sprint_id),
    FOREIGN KEY (created_by) REFERENCES Users(user_id)
);
*/
    // Primary Key story_id
    public int StoryId { get; set; }
    public String Title { get; set; } = null!;
    public String Description { get; set; } = null!;
    public String Status { get; set; } = "open";
    // Foreign Key epic_id referencing Epics
    public int EpicId { get; set; }
    // Foreign Key sprint_id referencing Sprints
    public int SprintId { get; set; }
    // Foreign Key created_by referencing Users
    public int CreatedBy { get; set; }
    public int StoryPoints { get; set; }

    // Navigation properties
    public virtual Epic Epic { get; set; } = null!;
    public virtual Sprint Sprint { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}