using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanner_OOP.Data;
using ShoppingPlanner_OOP.Models.Entities;
using ShoppingPlanner_OOP.Models.Identity;

namespace ShoppingPlanner_OOP.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // список користувачів
    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    // всі списки покупок
    public async Task<IActionResult> Lists()
    {
        var lists = await _context.ShoppingLists
            .Include(x => x.Items)
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return View(lists);
    }
}