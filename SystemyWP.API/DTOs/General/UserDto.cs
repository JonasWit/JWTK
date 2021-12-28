using System;
using System.Collections.Generic;
using SystemyWP.Data.Models.RestaurantAppModels.Access;

namespace SystemyWP.API.Dtos.General;

public class UserDto
{
    public string Id { get; set; }
        
    public string Image { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public string Role { get; set; }
        
    public bool EmailConfirmed { get; set; }

    public bool IsLocked { get; set; }
        
    public DateTime? LastLogin  { get; set; }

    public RestaurantAccessKey RestaurantAccessKey { get; set; }
}