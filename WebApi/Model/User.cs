using System;

namespace WebApi.Model;

public class User
{

    // Primary Key user_id
    public int UserId { get; set; }

    public String Username { get; set; } = null!;

}