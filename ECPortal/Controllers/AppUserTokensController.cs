using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;


namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize(Roles = "TeanLead, Admin, Agent")]
    public class AppUserTokensController : Controller
    {
        private readonly ECContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppUserTokensController(ECContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AppUserTokens
        public async Task<IActionResult> Index()
        {
            var appUserTokens = _context.AppUserTokens
                    .OrderByDescending(o => o.EntryDate)
                    .Take(100);

            return View(await appUserTokens.ToListAsync());
        }

        // GET: AppUserTokens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserTokens = await _context.AppUserTokens
                .FirstOrDefaultAsync(m => m.AppUserTokenId == id);

            if (appUserTokens == null)
            {
                return NotFound();
            }

            return View(appUserTokens);
        }

        // GET: AppUserTokens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserTokens = await _context.AppUserTokens
                .FirstOrDefaultAsync(m => m.AppUserTokenId == id);

            if (appUserTokens == null)
            {
                return NotFound();
            }

            return View(appUserTokens);
        }

        // POST: AppUserTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUserTokens = await _context.AppUserTokens.FindAsync(id);

            if (appUserTokens == null)
            {
                return NotFound();
            }

            _ = _context.AppUserTokens.Remove(appUserTokens);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //Common.PushMessage(TempData, "alert-danger", $"User token removing failed. Error: {ex.Message}");
                return RedirectToAction("Delete", id);
            }

            //success
            //Common.PushMessage(TempData, "alert-success", $"User token is removed successfully.");
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserTokensExists(int id)
        {
            return _context.AppUserTokens.Any(e => e.AppUserTokenId == id);
        }
    }
}
