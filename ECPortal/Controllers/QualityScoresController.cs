using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class QualityScoresController : Controller
    {
        private readonly ECContext _context;
        public QualityScoresController(ECContext context)
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

            var qualityScores = _context.QualityScores
                .Where(e => e.EID == agentId)
                .OrderBy(e => e.RecordDate)
                .ToList() ?? null;

            return View(qualityScores);
        }
    }
}
