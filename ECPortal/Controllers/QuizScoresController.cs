using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class QuizScoresController : Controller
    {
        private readonly ECContext _context;
        public QuizScoresController(ECContext context)
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

            var quizScores = _context.QuizScores
                .Where(e => e.EmployeeId == agentId)
                .OrderBy(e => e.QuizDate)
                .ToList() ?? null;

            return View(quizScores);
        }
    }
}
