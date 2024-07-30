using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        // GET: Feedbacks/Index
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
                .ToList();

            return View(feedbacks);
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            // Fetch employees
            List<Employee> employees = _context.Employee.ToList();

            // Create a dictionary or list to hold the employee IDs and names
            var employeeData = employees.ToDictionary(e => e.EmployeeId, e => e.EmployeeName);
            var feedbackTypes = new List<string> { "Excellent", "Positive", "Negative", "Neutral" };

            // Append employee data to ViewBag
            ViewBag.Employees = employeeData;

            
            ViewBag.FeedbackTypes = feedbackTypes;
            var model = new EmployeeFeedback
            {
                ProvidedBy = User.Identity.Name // Assuming the username is stored in User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeFeedback employeeFeedback)
        {
            if (ModelState.IsValid)
            {
                employeeFeedback.InsertDate = DateTime.Now;
                employeeFeedback.ModifiedDate = DateTime.Now;
                employeeFeedback.Status = "Pending"; // Default status

                _context.Add(employeeFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }

            // Re-fetch employee data in case of validation failure
            List<Employee> employees = _context.Employee.ToList();
            var feedbackTypes = new List<string> { "Positive","Excellent", "Negative", "Neutral" };
            ViewBag.Employees = employees.ToDictionary(e => e.EmployeeId, e => e.EmployeeName);
            
            ViewBag.FeedbackTypes = feedbackTypes;
            return View(employeeFeedback); // Return the model with validation errors
        }


        // GET: Feedbacks/Success
        public IActionResult Success()
        {
            return View();
        }
    }
}
