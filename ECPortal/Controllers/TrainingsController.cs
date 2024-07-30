using Microsoft.AspNetCore.Mvc;
using Pk.Com.Jazz.ECP.Models;
using Pk.Com.Jazz.ECP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // GET: Training/AddTraining
        public IActionResult AddTraining()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingRequests trainingRequest)
        {
            if (ModelState.IsValid)
            {
                // Ensure SubmissionDate is set to the current date and time
                trainingRequest.SubmissionDate = DateTime.Now;

                //Set to Pending Status
                trainingRequest.Status = "Pending";

                _context.TrainingRequests.Add(trainingRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to a list or index page
            }

            return View(trainingRequest);
        }
    }
    }
