using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VG_Review.Areas.Identity.Data;
using VG_Review.Models;

namespace VG_Review.Controllers
{
    public class GameController : Controller
    {
        private readonly VG_ReviewContext _db;

        public GameController(VG_ReviewContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var getGames = _db.Games.ToList();
            return View(getGames);
        }

        public async Task<IActionResult> Details(int? id)
        {
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

        [HttpGet]
        public IActionResult Create()
        {
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
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

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
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
