using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SleepWellApp.Shared;
using SleepWellApp.Server.Data;
using SleepWellApp.Server.Models;
using Microsoft.AspNetCore.Authorization;

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
    [HttpGet]
    [Authorize(Roles = "admin")]
    [Route("api/get-users")]
    public async Task<List<UserDto>> GetUsers()
    {
        var users = await (from u in _context.Users
                           select new UserDto
                           {
                               Id = u.Id,
                               UserName = u.UserName,
                               FirstName = u.FirstName,
                               LastName = u.LastName
                           }).ToListAsync();
        if (users is not null)
        {
            return users;
        }
        else
        {
            return new List<UserDto>();
        }


    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    [Route("api/get-roles/{id}")]
    public async Task<List<string>> GetRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is not null)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        else { return new List<string>(); }
    }

    //Journaling Methods Implemented
    [HttpPost]
    [Route("api/User/SaveJournalEntry")]
    public async Task<IActionResult> SaveJournalEntry([FromBody] JournalDto journalDto)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();

        }
        //Creating a new journal entry entity and populate the properties 

        var journalEntry = new JournalEntry
        {
            Id = user.Id,
            Journal_Content = journalDto.JournalContent
        };

        //Adding and saving the journal entries to the database
        // _context.JournalEntries.Add(journalEntry);
        user.Journal.Add(journalEntry);
        await _context.SaveChangesAsync();

        return Ok();
    }

}


