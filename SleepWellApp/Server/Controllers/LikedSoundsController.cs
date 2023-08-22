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

        public LikedSoundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{audioId}/like")]
        public async Task<IActionResult> LikedSound(int audioID)
        {

            var audio = await _context.LikedSound.FirstOrDefaultAsync(a => a.Liked_sound_Id == audioID);

            if (audio == null)
            {
                return NotFound();
            }

            var currUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            audio.Id = Convert.ToInt32(currUser);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
