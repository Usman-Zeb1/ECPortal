using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ECContext _context;
        public SalesController(ECContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeNumber;

            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or handle the case appropriately
            }

            var sales = _context.EmployeeSales
                .Where(e => e.EmployeeNumber == agentId)
                .OrderBy(e => e.SalesDate)
                .ToList() ?? null;
            
            return View(sales);
        }
    }
}
