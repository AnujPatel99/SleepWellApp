using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SleepWellApp.Shared;
using SleepWellApp.Server.Data;
using SleepWellApp.Server.Models;

namespace SleepWellApp.Server.Controllers
{
    public class LikedSoundsController : Controller
    {

        private readonly ApplicationDbContext? _context;

        public LikedSoundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/User")]
        public async Task<ActionResult<LikedSounds>> InsertToLikedSounds()
        {
            var user = await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                }).FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            /*var currentAudioId = await _context.SoundBoard
                .Select

            for (int i = 0; i < product.Count; i++)
            {
                _context.LikedSound.Add(product[i]);
            }
            _context.SaveChanges();*/
            return Ok();
        }
    }
}
