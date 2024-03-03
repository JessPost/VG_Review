using Microsoft.AspNetCore.Mvc;
using VG_Review.Areas.Identity.Data;
using VG_Review.Models;

namespace VG_Review.Controllers
{
    public class ReviewController : Controller
    {
        private readonly VG_ReviewContext _db;

        public ReviewController(VG_ReviewContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Create(int gameId)
        {
            var review = new Review { GameId = gameId };
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.DateTime = DateTime.Now;
                _db.Add(review);
                await _db.SaveChangesAsync();

                return RedirectToAction("Details", "Game", new { id = review.GameId });
            }
            return View(review);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _db.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                review.DateTime = DateTime.Now;
                _db.Update(review);
                await _db.SaveChangesAsync();

                return RedirectToAction("Details", "Game", new { id = review.GameId });
            }

            return View(review);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _db.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _db.Reviews.FindAsync(id);
            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", "Game", new { id = review.GameId });
        }
    }
}
