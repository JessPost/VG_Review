using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VG_Review.Areas.Identity.Data;
using VG_Review.Models;

namespace VG_Review.Controllers
{
    public class GameController : Controller
    {
        private readonly VG_ReviewContext _db;
        private readonly UserManager<User> _userManager;

        public GameController(VG_ReviewContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string title)
        {
            var getGames = _db.Games.ToList();
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }
            else
            {
                ViewBag.FirstName = "Guest";
            }

            if (!string.IsNullOrEmpty(title))
            {
                getGames = getGames.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                ViewBag.IsSearchPerformed = true;
            }

            return View(getGames);
        }

        //[Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }
            else
            {
                ViewBag.FirstName = "Guest";
            }

            if (id == null)
            {
                return NotFound();
            }

            var game = await _db.Games.Include(g => g.Reviews).FirstOrDefaultAsync(m => m.GameId == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }
            else
            {
                ViewBag.FirstName = "Guest";
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Add(game);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }
            else
            {
                ViewBag.FirstName = "Guest";
            }

            if (id == null)
            {
                return RedirectToAction("Details");
            }

            var getDetails = await _db.Games.FindAsync(id);

            return View(getDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                _db.Update(game);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }
            else
            {
                ViewBag.FirstName = "Guest";
            }

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getDetails = await _db.Games.FindAsync(id);

            return View(getDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var getDetails = await _db.Games.FindAsync(id);
            _db.Remove(getDetails);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
