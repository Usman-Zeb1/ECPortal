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


        public IActionResult RequestedTrainings()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requestedTrainings = _context.TrainingRequests
                .Where(rt => rt.RequestedBy == User.Identity.Name) // Assuming there's a RequestedById field
                .OrderBy(rt => rt.RequestDate)
                .ToList();

            return View(requestedTrainings);
        }


        public IActionResult AddTraining()
        {

            return View(new TrainingRequests());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTraining(TrainingRequests trainingRequests)
        {
            trainingRequests.RequestedBy = User.Identity.Name;
            if (ModelState.IsValid)
            {
                // Ensure SubmissionDate is set to the current date and time
                trainingRequests.SubmissionDate = DateTime.Now;

                //Set to Pending Status
                trainingRequests.Status = "Pending";

                _context.TrainingRequests.Add(trainingRequests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to a list or index page
            }

            return View(trainingRequests);
        }
    }
    }
