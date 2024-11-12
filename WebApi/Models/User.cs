using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<Story> StoriesNavigation { get; set; } = new List<Story>();

    public virtual ICollection<Team> TeamsNavigation { get; set; } = new List<Team>();
}