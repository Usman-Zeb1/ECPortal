using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using System.Security.Claims;


namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class PerformanceController : Controller
    {
        private readonly ECContext _context;

        public PerformanceController(ECContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var agentId = GetCurrentAgentId(); // Implement this method to get the current agent's ID
            var performanceData = _context.EmployeePerformances
                .Where(p => p.EmployeeId == agentId)
                .ToList() ?? null;

            return View(performanceData);

           
        }

        private int GetCurrentAgentId()
        {
            // Implement logic to get the current agent's ID
            // For example, if using ASP.NET Identity:
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeId;

            if (agentId == null)
            {
                return 0; // or handle the case where agentId is null as needed
            }

            int v = (int) agentId;
            return v;
            //throw new NotImplementedException();
        }
    }
}
