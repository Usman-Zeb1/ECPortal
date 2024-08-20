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
                List<Employee> agents = new List<Employee>();

                if (User.IsInRole("TeamLead"))
                {
                    // Fetch agents in the same Experience Center (EC) as the TeamLead
                    var currentEmployee = GetCurrentEmployee();
                    agents = _context.Employee
                        .Where(e => e.ECID == currentEmployee.ECID && e.Title == "Agent")
                        .ToList();
                }
                else if (User.IsInRole("ECM"))
                {
                    // Fetch agents in the same Experience Center (EC)
                    var currentEmployee = GetCurrentEmployee();
                    agents = _context.Employee
                        .Where(e => e.ECID == currentEmployee.ECID && e.Title == "Agent")
                        .ToList();
                }
                else if (User.IsInRole("RCCH"))
                {
                    // Fetch agents in the same region
                    var currentEmployee = GetCurrentEmployee();
                    agents = _context.Employee
                        .Where(e => e.RegionID == currentEmployee.RegionID && e.Title == "Agent")
                        .ToList();
                }
                else if (User.IsInRole("HOD"))
                {
                    // Fetch all agents
                    agents = _context.Employee
                        .Where(e => e.Title == "Agent")
                        .ToList();
                }

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

        private Employee GetCurrentEmployee()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployee = _context.Employee.FirstOrDefault(e => e.AppUserId == userId);

            if (currentEmployee == null)
            {

                throw new Exception("Employee Number not found.");

            }

            return currentEmployee;
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
