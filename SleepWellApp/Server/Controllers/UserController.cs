using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SleepWellApp.Shared;
using SleepWellApp.Server.Data;
using SleepWellApp.Server.Models;

namespace SleepWellApp.Server.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    [Route("api/User")]
    public async Task<ActionResult<UserDto>> GetUserInfo()
    {
        var user = await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
            }).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

}
