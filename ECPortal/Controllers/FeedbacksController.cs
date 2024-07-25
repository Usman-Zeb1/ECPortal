using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Linq;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class FeedbacksController : Controller
    {
        private readonly ECContext _context;

        public FeedbacksController(ECContext context)
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

            var feedbacks = _context.EmployeeFeedbacks
                .Where(fb => fb.EmployeeId == agentId)
                .OrderBy(fb => fb.FeedbackDate)
                .ToList() ?? null;

            return View(feedbacks);
        }

        // Optionally, you can add Create, Edit, Delete actions here for managing feedbacks.
    }
}
