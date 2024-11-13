using System;
using System.Collections.Generic;

namespace WebApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;
}