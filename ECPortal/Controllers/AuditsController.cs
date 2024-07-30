using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize(Roles = "Admin, TeamLead, HOD, ECM, RCCH, Agent")]
    public class AuditsController : Controller
    {

       

        private readonly ECContext _context;

        public AuditsController(ECContext context)
        {
            _context = context;
        }
        // GET: ECAudits


        public async Task<IActionResult> Index()
        {
            return View(await _context.ECAudits.ToListAsync());
        }

        // GET: ECAudits/Create
        public IActionResult Create()
        {
            var experienceCenters = _context.ECs.ToList();
            ViewBag.ExperienceCenters = new SelectList(experienceCenters, "ECID", "PhysicalAddress");
            return View();
        }

        // POST: ECAudits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ECAudits eCAudits)
        {
            if (ModelState.IsValid)
            {
                // Set InsertDate and ModifiedDate to current date and time
                eCAudits.InsertDate = DateTime.Now;
                eCAudits.ModifiedDate = DateTime.Now;

                _context.ECAudits.Add(eCAudits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to a list or index page
            }

            return View(eCAudits);
        }


        // Other actions like Edit, Delete, etc.
    }
}
