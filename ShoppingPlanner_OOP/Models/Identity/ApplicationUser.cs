using Microsoft.AspNetCore.Identity;

namespace ShoppingPlanner_OOP.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}