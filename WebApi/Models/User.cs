using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;


    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Story> StoriesNavigation { get; set; } = new List<Story>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Team> TeamsNavigation { get; set; } = new List<Team>();
}
