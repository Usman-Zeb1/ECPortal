using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ECContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(ECContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.AppUserId == employeeId);
            var EC = new Models.EC();

            if (employee != null)
            {
                EC = await _context.ECs?
                    .Include(e => e.ECRegion)  // Include the related ECRegion entity
                    .FirstOrDefaultAsync(t => t.ECID == employee.ECID);
            }

            var user = await _userManager.FindByIdAsync(employeeId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var viewModel = new DashboardViewModel
            {
                Employee = employee,
                EC = EC,
                Role = roles?.FirstOrDefault(), // Assuming a user has at least one role and taking the first one

                EmployeeCommissionsCount = employee != null ? _context.EmployeeCommissions.Count(c => c.EmployeeId == employee.EmployeeId) : 0,
                LastCommissionDate = employee != null ? _context.EmployeeCommissions
                                                      .Where(c => c.EmployeeId == employee.EmployeeId)
                                                      .OrderByDescending(c => c.CommissionDate)
                                                      .FirstOrDefault()?.CommissionDate : (DateTime?)null,

                EmployeeFeedbacksCount = employee != null ? _context.EmployeeFeedbacks.Count(f => f.EmployeeId == employee.EmployeeId) : 0,
                LastFeedbackDate = employee != null ? _context.EmployeeFeedbacks
                                                    .Where(f => f.EmployeeId == employee.EmployeeId)
                                                    .OrderByDescending(f => f.FeedbackDate)
                                                    .FirstOrDefault()?.FeedbackDate : (DateTime?)null,

                EmployeeTrainingsCount = employee != null ? _context.EmployeeTrainings.Count(t => t.EmployeeId == employee.EmployeeId) : 0,
                LastTrainingDate = employee != null ? _context.EmployeeTrainings
                                                    .Where(t => t.EmployeeId == employee.EmployeeId)
                                                    .OrderByDescending(t => t.TrainingDate)
                                                    .FirstOrDefault()?.TrainingDate : (DateTime?)null,

                EmployeeRecognitionsCount = employee != null ? _context.EmployeeRecognitions.Count(r => r.EmployeeId == employee.EmployeeId) : 0,
                LastRecognitionDate = employee != null ? _context.EmployeeRecognitions
                                                       .Where(r => r.EmployeeId == employee.EmployeeId)
                                                       .OrderByDescending(r => r.RecognitionDate)
                                                       .FirstOrDefault()?.RecognitionDate : (DateTime?)null,
            };

            return View(viewModel);
        }


    }
}
