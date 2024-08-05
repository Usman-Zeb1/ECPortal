using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var employee = _context.Employee.FirstOrDefault(e => e.AppUserId == employeeId);
            if (employee == null)
            {
                return NotFound();
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
                Role = roles?.FirstOrDefault(), // Assuming a user has at least one role and taking the first one

                EmployeeCommissionsCount = _context.EmployeeCommissions.Count(c => c.EmployeeId == employee.EmployeeId),
                LastCommissionDate = _context.EmployeeCommissions
                                              .Where(c => c.EmployeeId == employee.EmployeeId)
                                              .OrderByDescending(c => c.CommissionDate)
                                              .FirstOrDefault()?.CommissionDate ?? null,

                EmployeeFeedbacksCount = _context.EmployeeFeedbacks.Count(f => f.EmployeeId == employee.EmployeeId),
                LastFeedbackDate = _context.EmployeeFeedbacks
                                           .Where(f => f.EmployeeId == employee.EmployeeId)
                                           .OrderByDescending(f => f.FeedbackDate)
                                           .FirstOrDefault()?.FeedbackDate ?? null,

                EmployeePerformancesCount = _context.EmployeePerformances.Count(p => p.EmployeeId == employee.EmployeeId),
                EmployeePerformanceScore = _context.EmployeePerformances
                                                   .Where(p => p.EmployeeId == employee.EmployeeId)
                                                   .OrderByDescending(p => p.PerformanceEndDate)
                                                   .FirstOrDefault()?.PerformanceScore ?? 0,
                LastPerformanceDate = _context.EmployeePerformances
                                              .Where(p => p.EmployeeId == employee.EmployeeId)
                                              .OrderByDescending(p => p.PerformanceEndDate)
                                              .FirstOrDefault()?.PerformanceEndDate ?? null,

                EmployeeTrainingsCount = _context.EmployeeTrainings.Count(t => t.EmployeeId == employee.EmployeeId),
                LastTrainingDate = _context.EmployeeTrainings
                                           .Where(t => t.EmployeeId == employee.EmployeeId)
                                           .OrderByDescending(t => t.TrainingDate)
                                           .FirstOrDefault()?.TrainingDate ?? null,

                EmployeeSalesCount = _context.EmployeeSales.Count(s => s.EmployeeNumber == employee.EmployeeNumber),
                LastSalesDate = _context.EmployeeSales
                                        .Where(s => s.EmployeeNumber == employee.EmployeeNumber)
                                        .OrderByDescending(s => s.SalesDate)
                                        .FirstOrDefault()?.SalesDate ?? null,

                EmployeeRecognitionsCount = _context.EmployeeRecognitions.Count(r => r.EmployeeId == employee.EmployeeId),
                LastRecognitionDate = _context.EmployeeRecognitions
                                              .Where(r => r.EmployeeId == employee.EmployeeId)
                                              .OrderByDescending(r => r.RecognitionDate)
                                              .FirstOrDefault()?.RecognitionDate ?? null,

                EmployeeTargetsCount = _context.EmployeeTargets.Count(t => t.EmployeeNumber == employee.EmployeeId),
                LastTargetDate = _context.EmployeeTargets
                                         .Where(t => t.EmployeeNumber == employee.EmployeeNumber)
                                         .OrderByDescending(t => t.Month)
                                         .FirstOrDefault()?.InsertDate ?? null,
            };

            return View(viewModel);
        }

    }
}
