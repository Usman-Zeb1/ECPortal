using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Models;
using Pk.Com.Jazz.ECP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class TrainingsController : Controller
    {
        private readonly ECContext _context;

        public TrainingsController(ECContext context)
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

            var trainings = _context.EmployeeTrainings
                .Where(t => t.EmployeeId == agentId)
                .OrderBy(t => t.TrainingDate)
                .ToList() ?? null;

            return View(trainings);
        }
    }
}
