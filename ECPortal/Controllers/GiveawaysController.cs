using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize(Roles = "Admin, TeamLead, HOD, ECM, RCCH, Agent")]
    public class GiveawaysController : Controller
    {
        private readonly ECContext _context;

        public GiveawaysController(ECContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ECGiveaways.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = ecs.Any() ? new SelectList(ecs, "ECID", "PhysicalAddress") : null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ECID,Availability,GiveawayItem,QuantityAvailable,Price,Description")] ECGiveaways eCGiveaways)
        {
            eCGiveaways.Status = "Active"; // Set default status
            if (ModelState.IsValid)
            {
                _context.Add(eCGiveaways);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = ecs.Any() ? new SelectList(ecs, "ECID", "PhysicalAddress", eCGiveaways.ECID) : null;
            return View(eCGiveaways);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giveaway = await _context.ECGiveaways.FindAsync(id);
            if (giveaway == null)
            {
                return NotFound();
            }
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = ecs.Any() ? new SelectList(ecs, "ECID", "PhysicalAddress", giveaway.ECID) : null;
            return View(giveaway);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ECID,Availability,GiveawayItem,QuantityAvailable,Price,Description")] ECGiveaways giveaway)
        {
            if (id != giveaway.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giveaway);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiveawayExists(giveaway.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = ecs.Any() ? new SelectList(ecs, "ECID", "PhysicalAddress", giveaway.ECID) : null;
            return View(giveaway);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giveaway = await _context.ECGiveaways
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giveaway == null)
            {
                return NotFound();
            }

            return View(giveaway);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giveaway = await _context.ECGiveaways.FindAsync(id);
            _context.ECGiveaways.Remove(giveaway);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiveawayExists(int id)
        {
            return _context.ECGiveaways.Any(e => e.Id == id);
        }
    }
}
