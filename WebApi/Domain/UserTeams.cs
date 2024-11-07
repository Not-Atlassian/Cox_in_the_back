using System;

namespace WebApi.Domain;

public class UserTeams
{

    // Primary Key and Foreign Key user_id references user_id in User
    public int UserId { get; set; }

    // Primary Key and Foreign Key team_id references team_id in Teams
    public int TeamId { get; set; }

    
    public virtual User User { get; set; } = null!;
    public virtual Teams Team { get; set; } = null!;



}
