using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VG_Review.Areas.Identity.Data;
using VG_Review.Models;

namespace VG_Review.Controllers
{
    public class ReviewController : Controller
    {
        private readonly VG_ReviewContext _db;
        private readonly UserManager<User> _userManager;

        public ReviewController(VG_ReviewContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Create(int gameId)
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
            var review = new Review { GameId = gameId };
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review review)
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
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _db.Reviews.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewBag.FirstName = user.FirstName;
            }
            else
            {
                ViewBag.FirstName = "Guest";
            }

            if (review == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This gets the ID of the current user
            if (review.UserId != currentUserId)
            {
                return Unauthorized(); // Or you could return a custom error view
            }

            return View(review);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,GameId,ReviewText,Rating")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (review.UserId != currentUserId)
            {
                return Unauthorized();
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _db.Reviews.FindAsync(id);      

            if (review == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || review.UserId != currentUser.Id)
            {
                // Optionally, you might want to handle this differently to not reveal that the resource exists but is unauthorized
                return Unauthorized();
            }

            // Setting FirstName for the view, though this might not be necessary for a delete operation
            ViewBag.FirstName = currentUser.FirstName ?? "Guest";


            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _db.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || review.UserId != currentUser.Id)
            {
                // As before, consider how you want to handle unauthorized attempts
                return Unauthorized();
            }


            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", "Game", new { id = review.GameId });
        }
    }
}
