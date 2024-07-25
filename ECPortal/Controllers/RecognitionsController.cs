using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class RecognitionsController : Controller
    {
        private readonly ECContext _context;

        public RecognitionsController(ECContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeId;

            if (agentId == null)
            {
                return RedirectToAction("Index", "Home"); // Or handle the case appropriately
            }

            var recognitions = _context.EmployeeRecognitions
                .Where(t => t.EmployeeId == agentId)
                .OrderBy(t => t.RecognitionDate)
                .ToList() ?? null;

            return View(recognitions);
        }
    }
}
