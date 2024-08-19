using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using Pk.Com.Jazz.ECP.ViewModels;
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
            var viewModel = new FeedbacksViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Agent"))
            {
                var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeId;

                if (userId == null)
                {
                    return RedirectToAction("Index", "Home"); // Or handle the case appropriately
                }

                viewModel.Feedbacks = _context.EmployeeFeedbacks
                    .Where(fb => fb.EmployeeId == agentId)
                    .OrderBy(fb => fb.FeedbackDate)
                    .Select(fb => new FeedbacksViewModel.FeedbackDetail
                    {
                        Id = fb.Id,
                        FeedbackDate = fb.FeedbackDate,
                        FeedbackType = fb.FeedbackType,
                        Feedback = fb.Feedback,
                        ProvidedBy = fb.ProvidedBy,
                        Comments = fb.Comments,
                        EmailAddress = _context.Employee.FirstOrDefault(e => e.EmployeeId == fb.EmployeeId).EmailAddress
                    })
                    .ToList();

                viewModel.IsAgent = true;
            }
            else
            {
               
                viewModel.Feedbacks = _context.EmployeeFeedbacks
                    .Where(fb => fb.ProvidedBy == User.Identity.Name) // Assuming CreatedBy is the user who gave the feedback
                    .OrderBy(fb => fb.FeedbackDate)
                    .Select(fb => new FeedbacksViewModel.FeedbackDetail
                    {
                        Id = fb.Id,
                        FeedbackDate = fb.FeedbackDate,
                        FeedbackType = fb.FeedbackType,
                        Feedback = fb.Feedback,
                        ProvidedBy = fb.ProvidedBy,
                        Comments = fb.Comments,
                        EmailAddress = _context.Employee.FirstOrDefault(e => e.EmployeeId == fb.EmployeeId).EmailAddress
                    })
                    .ToList();

                viewModel.IsAgent = false;
            }

            return View(viewModel);
        }


        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            // Get the current user role
            var userRole = User.IsInRole("HOD") ? "HOD" :
                           User.IsInRole("RCCH") ? "RCCH" :
                           User.IsInRole("ECM") ? "ECM" :
                           User.IsInRole("TeamLead") ? "TeamLead" : null;

            List<Employee> employees = new List<Employee>();

            if (userRole == "HOD")
            {
                // Fetch all employees
                employees = _context.Employee.ToList();
            }
            else if (userRole == "RCCH")
            {
                // Fetch employees from the same region
                var currentEmployee = GetCurrentEmployee();
                employees = _context.Employee
                                    .Where(e => e.RegionID == currentEmployee.RegionID)
                                    .ToList();
            }
            else if (userRole == "ECM")
            {
                // Fetch employees from the same Experience Center (EC)
                var currentEmployee = GetCurrentEmployee();
                employees = _context.Employee
                                    .Where(e => e.ECID == currentEmployee.ECID && e.Title != "ECM")
                                    .ToList();
            }

            else if (userRole == "TeamLead")
            {
                // Fetch employees from the same Experience Center (EC)
                var currentEmployee = GetCurrentEmployee();
                employees = _context.Employee
                                    .Where(e => e.ECID == currentEmployee.ECID && e.Title != "ECM" && e.Title != "TeamLead")
                                    .ToList();
            }

            // Prepare employee data for the view
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

            // Re-fetch employee data based on the user role in case of validation failure
            var userRole = User.IsInRole("HOD") ? "HOD" :
                           User.IsInRole("RCCH") ? "RCCH" :
                           User.IsInRole("ECM") ? "ECM" :
                           User.IsInRole("TeamLead") ? "TeamLead" : null;

            List<Employee> employees = new List<Employee>();

            if (userRole == "HOD")
            {
                // Fetch all employees
                employees = _context.Employee.ToList();
            }
            else if (userRole == "RCCH")
            {
                // Fetch employees from the same region
                var currentEmployee = GetCurrentEmployee();
                employees = _context.Employee
                                    .Where(e => e.RegionID == currentEmployee.RegionID)
                                    .ToList();
            }
            else if (userRole == "ECM" || userRole == "TeamLead")
            {
                // Fetch employees from the same Experience Center (EC)
                var currentEmployee = GetCurrentEmployee();
                employees = _context.Employee
                                    .Where(e => e.ECID == currentEmployee.ECID)
                                    .ToList();
            }

            var employeeData = employees.ToDictionary(e => e.EmployeeId, e => e.EmployeeName);
            var feedbackTypes = new List<string> { "Positive", "Excellent", "Negative", "Neutral" };

            ViewBag.Employees = employeeData;
            ViewBag.FeedbackTypes = feedbackTypes;

            return View(employeeFeedback); // Return the model with validation errors
        }



        // GET: Feedbacks/Success
        public IActionResult Success()
        {
            return View();
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
    }
}
