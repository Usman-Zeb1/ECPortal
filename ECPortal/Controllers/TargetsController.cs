using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class TargetsController : Controller
    {
        private readonly ECContext _context;

        public TargetsController(ECContext context)
        {
            _context = context;
        }


        public IActionResult Index(int month, int year, int? employeeNumber)
        {
            if (month == 0)
                month = DateTime.Now.Month;

            if (year == 0)
                year = DateTime.Now.Year;

            if (User.IsInRole("Agent"))
            {
                employeeNumber = GetEmployeeNumber();

                if (employeeNumber == null)
                {
                    return NotFound("Employee not found.");
                }

                var model = _context.EmployeeTargets
                    .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

                if (model == null)
                {
                    return View();
                }

                return View(model);
            }
            else
            {
                var agents = _context.Employee
                    .Select(e => new { e.EmployeeNumber, e.EmployeeName })
                    .ToList();

                ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                if (employeeNumber.HasValue)
                {
                    var model = _context.EmployeeTargets
                        .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

                    if (model != null)
                    {
                        return View("AgentsTargets", model);
                    }
                }

                return View("AgentsTargets");
            }
        }




        private int GetEmployeeNumber()
        {
            // Implement logic to get the current employee's number
            // For example, if using ASP.NET Identity:
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var empNumber = _context.Employee.FirstOrDefault(e => e.AppUserId == userId)?.EmployeeNumber;

            if (empNumber == null)
            {
                throw new Exception("Employee not found."); // Handle the case where employee number is null
            }

            return empNumber.Value;
        }
    }
}
