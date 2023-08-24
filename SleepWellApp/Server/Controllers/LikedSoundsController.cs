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

        // Liked Sounds Page Method
        [HttpGet("audioIds")]
        public async Task<ActionResult<List<int>>> GetAudioIds()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(user.Id);
            //var likedSounds = _context.Users.Include(a => a.LikedSound).Select(a => new {LikedSound = a.LikedSound}).ToList();
            // var likedSounds = _context.Users.Select(a => a.LikedSound);
            // var audioIds = user.LikedSound.Select(audio => audio.Sound_Id).ToList();
            List<int> likedSounds = _context.LikedSound.Select(i => i.Sound_Id).ToList();
            return Ok(likedSounds);
        }

        // Like Button for AudioCard method
        [HttpPost("{audioID}/like")]
        public async Task<IActionResult> LikedSound(int audioID)
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            LikedSounds ls = new LikedSounds
            {
                Sound_Id = audioID
            };
            // var toggleSound = _context.Users.Include(u => u.LikedSound).FirstOrDefault(u => u.Id == user.Id).LikedSound.FirstOrDefault(s => s.Sound_Id == audioID);

            // if (user.LikedSound.Any(LikedSound => ls.Sound_Id == LikedSound.Sound_Id))
            var toggleSound = _context.Users.Include(u => u.LikedSound).FirstOrDefault(u => u.Id == user.Id).LikedSound.FirstOrDefault(s => s.Sound_Id == audioID);

            //if (user.LikedSound.Exists(LikedSound => ls.Sound_Id == LikedSound.Sound_Id))
            if(toggleSound is null)
            {
                user.LikedSound.Add(ls);
            }
            else
            {
                user.LikedSound.Remove(ls);
            }
            _context.SaveChanges();

            // await _context.SaveChangesAsync(); context.likedsound.exists(audioID)

            return Ok();
        }
    }
}
