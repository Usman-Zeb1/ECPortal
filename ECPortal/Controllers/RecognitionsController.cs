using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using Pk.Com.Jazz.ECP.ViewModels;
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

            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or handle the case appropriately
            }

            RecognitionsViewModel viewModel = new RecognitionsViewModel();

            if (User.IsInRole("Agent"))
            {
                viewModel.Recognitions = _context.EmployeeRecognitions
                    .Where(t => t.EmployeeId == agentId)
                    .OrderBy(t => t.RecognitionDate)
                    .Select(t => new RecognitionsViewModel.RecognitionDetail
                    {
                        Id = t.Id,
                        RecognitionDate = t.RecognitionDate,
                        RecognitionType = t.RecognitionType,
                        Recognition = t.RecognitionDetail,
                        ProvidedBy = t.RecognizedBy,
                        Comments = t.Comments,
                        RecognizedAgent = _context.Employee.FirstOrDefault(e => e.EmployeeId == t.EmployeeId).EmailAddress
                    })
                    .ToList();
                viewModel.IsAgent = true;
            }
            else
            {
                viewModel.Recognitions = _context.EmployeeRecognitions
                    .Where(t => t.RecognizedBy == User.Identity.Name)
                    .OrderBy(t => t.RecognitionDate)
                    .Select(t => new RecognitionsViewModel.RecognitionDetail
                    {
                        Id = t.Id,
                        RecognitionDate = t.RecognitionDate,
                        RecognitionType = t.RecognitionType,
                        Recognition = t.RecognitionDetail,
                        ProvidedBy = t.RecognizedBy,
                        Comments = t.Comments,
                        RecognizedAgent = _context.Employee.FirstOrDefault(e => e.EmployeeId == t.EmployeeId).EmailAddress
                    })
                    .ToList();
                viewModel.IsAgent = false;
            }

            return View(viewModel);
        }



        // GET: EmployeeRecognition/Create
        public IActionResult Create()
        {
            var employees = _context.Employee.ToList();
            ViewBag.Employees = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.EmployeeName} - {e.EmployeeNumber}"
            }).ToList();
            var recognitionTypes = new List<string> { "Employee of the Month", "Outstanding Performance", "Team Player", "Innovator" }; // Default recognition types
            ViewBag.RecognitionTypes = new SelectList(recognitionTypes);

            var model = new EmployeeRecognition
            {
                RecognizedBy = User.Identity.Name
            };

            return View(model);
        }

        // POST: EmployeeRecognition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeRecognition employeeRecognition)
        {
            employeeRecognition.RecognizedBy = User.Identity.Name;

            if (ModelState.IsValid)
            {
                // Set InsertDate and ModifiedDate to current date and time
                employeeRecognition.InsertDate = DateTime.Now;
                employeeRecognition.ModifiedDate = DateTime.Now;
                employeeRecognition.Status = "Active";

                _context.EmployeeRecognitions.Add(employeeRecognition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to a list or index page
            }

            // Log ModelState errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }

            var employees = _context.Employee.ToList();
            ViewBag.Employees = employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = $"{e.EmployeeName} - {e.EmployeeNumber}"
            }).ToList();

            var recognitionTypes = new List<string> { "Employee of the Month", "Outstanding Performance", "Team Player", "Innovator" }; // Default recognition types
            ViewBag.RecognitionTypes = new SelectList(recognitionTypes);

            return View(employeeRecognition);
        }



    }
}
