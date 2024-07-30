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
    [Authorize]
    public class StocksController : Controller
    {
        private readonly ECContext _context;

        public StocksController(ECContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, TeamLead, HOD, ECM, RCCH, Agent")]
        // GET: ECStocks
        public async Task<IActionResult> Index()
        {
            var stocks = await _context.ECStocks.ToListAsync();
            return View(stocks);
        }

        // GET: ECStocks/Create
        public async Task<IActionResult> Create()
        {
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = new SelectList(ecs, "ECID", "PhysicalAddress");
            return View();
        }



        // POST: ECStocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ECID,ProductName,Quantity,Price,Comments")] ECStocks eCStocks)
        {
            if (ModelState.IsValid)
            {
                eCStocks.Status = "Available"; // Set default status
                _context.Add(eCStocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = new SelectList(ecs, "ECID", "PhysicalAddress", eCStocks.ECID);
            return View(eCStocks);
        }

        // GET: ECStocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eCStocks = await _context.ECStocks.FindAsync(id);
            if (eCStocks == null)
            {
                return NotFound();
            }
            var ecs = await _context.ECs.ToListAsync();
            ViewBag.ECID = new SelectList(ecs, "ECID", "PhysicalAddress", eCStocks.ECID);
            return View(eCStocks);
        }

        // POST: ECStocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ECID,ProductName,Quantity,Price,Comments")] ECStocks eCStocks)
        {
            if (id != eCStocks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    eCStocks.Status = "Available"; // Set default status
                    _context.Update(eCStocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ECStocksExists(eCStocks.Id))
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
            ViewBag.ECID = new SelectList(ecs, "ECID", "PhysicalAddress", eCStocks.ECID);
            return View(eCStocks);
        }

        // GET: ECStocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eCStocks = await _context.ECStocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eCStocks == null)
            {
                return NotFound();
            }

            return View(eCStocks);
        }

        // POST: ECStocks/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.ECStocks.FindAsync(id);
            _context.ECStocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ECStocksExists(int id)
        {
            return _context.ECStocks.Any(e => e.Id == id);
        }
    }
}
