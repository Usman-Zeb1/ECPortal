using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class CommissionsController : Controller
    {
        private readonly ECContext _context;
        public CommissionsController(ECContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeId;

            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or handle the case appropriately
            }

            var commissions = _context.EmployeeCommissions
                .Where(e => e.EmployeeId == agentId)
                .OrderBy(e => e.CommissionDate)
                .ToList() ?? null;

            return View(commissions);
        }
    }
}
