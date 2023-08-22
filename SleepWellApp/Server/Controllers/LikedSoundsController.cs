using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SleepWellApp.Shared;
using SleepWellApp.Server.Data;
using SleepWellApp.Server.Models;

namespace SleepWellApp.Server.Controllers
{
    [Route("api/audio")]
    [ApiController]
    public class LikedSoundsController : Controller
    {
        private readonly ApplicationDbContext? _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikedSoundsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost("{audioId}/like")]
        public async Task<IActionResult> LikedSound(int audioID)
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            LikedSounds ls = new LikedSounds
            {
                Sound_Id = audioID
            };

            if (user.LikedSound.Exists(LikedSound => ls.Sound_Id == LikedSound.Sound_Id))
            {
                user.LikedSound.Remove(ls);
            } 
            else
            {
                user.LikedSound.Add(ls);
            }
            _context.SaveChanges();

            // await _context.SaveChangesAsync(); context.likedsound.exists(audioID)

            return Ok();
        }
    }
}
