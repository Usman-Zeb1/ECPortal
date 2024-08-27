using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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


        #region Index Action

        public IActionResult Index(int month = 0, int year = 0, int? employeeNumber = null, int? regionId = null, int? ecId = null)
        {
            if (month == 0) month = DateTime.Now.Month;
            if (year == 0) year = DateTime.Now.Year;

            if (User.IsInRole("Agent"))
            {
                return HandleAgentRole(month, year);
            }
            else if (User.IsInRole("TeamLead"))
            {
                return HandleTeamLeadRole(month, year, employeeNumber);
            }
            else if (User.IsInRole("ECM"))
            {
                return HandleECMRole(month, year, employeeNumber);
            }
            else if (User.IsInRole("RCCH"))
            {
                return HandleRCCHRole(month, year, employeeNumber, regionId, ecId);
            }
            else if (User.IsInRole("HOD") || User.IsInRole("Admin"))
            {
                return HandleHODRole(month, year, employeeNumber, regionId, ecId);
            }

            return View();
        }

        #endregion

        #region Role Handlers

        private IActionResult HandleAgentRole(int month, int year)
        {
            int? employeeNumber = GetEmployeeNumber();

            if (employeeNumber == null)
            {
                return NotFound("Employee not found.");
            }

            var model = _context.EmployeeTargets
                .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

            if (model != null)
            {
                return View("Agent/AgentTargets", model);
            }

            return View("Agent/AgentTargets");
        }


        public IActionResult YourTargets(int month, int year)
        {
            int? employeeNumber = GetEmployeeNumber();

            if (employeeNumber == null)
            {
                return NotFound("Employee not found.");
            }

            var model = _context.EmployeeTargets
                .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

            if (model != null)
            {
                return View("Agent/TLTargets", model);
            }

            return View("Agent/TLTargets");
        }

        private IActionResult HandleTeamLeadRole(int month, int year, int? employeeNumber)
        {
            var agents = GetAgentsForCurrentEmployee("TeamLead");
            ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

            if (employeeNumber.HasValue)
            {
                var model = _context.EmployeeTargets
                    .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

                if (model != null)
                {
                    return View("Agent/TLOrECMAgentTargets", model);
                }
            }

            return View("Agent/TLOrECMAgentTargets");
        }

        private IActionResult HandleECMRole(int month, int year, int? employeeNumber)
        {
            var agents = GetAgentsForCurrentEmployee("ECM");
            ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

            if (employeeNumber.HasValue)
            {
                var model = _context.EmployeeTargets
                    .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

                if (model != null)
                {
                    return View("Agent/TLOrECMAgentTargets", model);
                }
            }

            return View("Agent/TLOrECMAgentTargets");
        }

        private IActionResult HandleRCCHRole(int month, int year, int? employeeNumber, int? regionId, int? ecId)
        {
            var currentEmployee = GetCurrentEmployee();
            var parentRegionName = _context.ECRegions
                .Where(r => r.ECRegionID == currentEmployee.RegionID)
                .Select(r => r.ECParentRegionName)
                .FirstOrDefault();

            var regions = _context.ECRegions
                .Where(r => r.ECParentRegionName == parentRegionName)
                .ToList();

            ViewBag.Regions = new SelectList(regions, "ECRegionID", "ECRegionName");

            if (regionId.HasValue || ecId.HasValue)
            {
                var agents = GetAgentsByRegionAndECID(regionId, ecId);
                ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                if (employeeNumber.HasValue)
                {
                    var model = _context.EmployeeTargets
                        .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

                    if (model != null)
                    {
                        return View("Agent/RCCHAgentTargets", model);
                    }
                }
            }

            return View("Agent/RCCHAgentTargets");
        }

        private IActionResult HandleHODRole(int month, int year, int? employeeNumber, int? regionId, int? ecId)
        {
            var regions = _context.ECRegions
                .Select(r => new { r.ECRegionID, r.ECRegionName })
                .ToList();

            ViewBag.Regions = new SelectList(regions, "ECRegionID", "ECRegionName");

            if (regionId.HasValue || ecId.HasValue)
            {
                var agents = GetAgentsByRegionAndECID(regionId, ecId);
                ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                if (employeeNumber.HasValue)
                {
                    var model = _context.EmployeeTargets
                        .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

                    if (model != null)
                    {
                        return View("Agent/HODorAdminAgentTargets", model);
                    }
                }
            }

            return View("Agent/HODorAdminAgentTargets");
        }

        #endregion


        #region ECTargets

        [Authorize(Roles = "HOD, ECM, RCCH, Admin")]
        public async Task<IActionResult> ECTargets(int month = 0, int year = 0, int? regionID = null, int? ECID = null)
        {
            // Set default month and year if not provided
            if (month == 0) month = DateTime.Now.Month;
            if (year == 0) year = DateTime.Now.Year;

            // Get the current user's role
            var userRole = User.IsInRole("HOD") ? "HOD" :
                           User.IsInRole("ECM") ? "ECM" :
                           User.IsInRole("RCCH") ? "RCCH" : "Admin";

            if (userRole == "ECM")
            {
                var currentEmp = GetCurrentEmployee();
                ECID = currentEmp.ECID;  // Automatically set ECID based on the ECM's entity
            }

            if (userRole == "HOD" || userRole == "RCCH" || userRole == "Admin")
            {
                // Fetch regions for HOD, RCCH, and Admin
                if (userRole == "RCCH")
                {
                    var currentEmployee = GetCurrentEmployee();
                    var parentRegionName = _context.ECRegions
                        .Where(r => r.ECRegionID == currentEmployee.RegionID)
                        .Select(r => r.ECParentRegionName)
                        .FirstOrDefault();

                    var regions = _context.ECRegions
                        .Where(r => r.ECParentRegionName == parentRegionName)
                        .ToList();
                    ViewBag.Regions = new SelectList(regions, "ECRegionID", "ECRegionName");
                }
                else
                {
                    var regions = await _context.ECRegions
                        .Select(r => new { r.ECRegionID, r.ECRegionName })
                        .ToListAsync();
                    ViewBag.Regions = new SelectList(regions, "ECRegionID", "ECRegionName");
                }
            }

            // Fetch target data if ECID is provided
            if (ECID.HasValue)
            {
                var ECTargets = await _context.ECTargets
                    .Where(t => t.Month == month && t.Year == year && t.ECID == ECID)
                    .FirstOrDefaultAsync();

                if (ECTargets != null)
                {
                    // Return the appropriate view with target data based on the user's role
                    ViewBag.ECTargets = ECTargets;

                    switch (userRole)
                    {
                        case "HOD":
                            return View("ExperienceCenter/ECTargetsHOD", ECTargets);
                        case "ECM":
                            return View("ExperienceCenter/ECTargetsECM", ECTargets);
                        case "RCCH":
                            return View("ExperienceCenter/ECTargetsRCCH", ECTargets);
                        case "Admin":
                            return View("ExperienceCenter/ECTargetsHOD", ECTargets);
                        default:
                            return View("ExperienceCenter/ECTargetsHOD", ECTargets);
                    }
                }
            }

            // Return the appropriate empty view based on the user's role
            switch (userRole)
            {
                case "HOD":
                    return View("ExperienceCenter/ECTargetsHOD");
                case "ECM":
                    return View("ExperienceCenter/ECTargetsECM");
                case "RCCH":
                    return View("ExperienceCenter/ECTargetsRCCH");
                case "Admin":
                    return View("ExperienceCenter/ECTargetsHOD");
                default:
                    return View("ExperienceCenter/ECTargetsHOD");
            }
        }

        #endregion


        #region Helper Methods

        private List<Employee> GetAgentsForCurrentEmployee(string role)
        {
            var currentEmployee = GetCurrentEmployee();
            IQueryable<Employee> agentsQuery = _context.Employee.Where(e => (e.Title == "Agent" || e.Title == "TeamLead"));

            if (role == "TeamLead" || role == "ECM")
            {
                agentsQuery = agentsQuery.Where(e => e.ECID == currentEmployee.ECID);
            }
            else if (role == "RCCH")
            {
                agentsQuery = agentsQuery.Where(e => e.RegionID == currentEmployee.RegionID);
            }

            return agentsQuery.ToList();
        }

        private List<Employee> GetAgentsByRegionAndECID(int? regionId, int? ecId)
        {
            IQueryable<Employee> agentsQuery = _context.Employee.Where(e => (e.Title == "Agent" || e.Title == "TeamLead"));

            if (regionId.HasValue)
            {
                agentsQuery = agentsQuery.Where(e => e.RegionID == regionId);
            }

            if (ecId.HasValue)
            {
                agentsQuery = agentsQuery.Where(e => e.ECID == ecId);
            }

            return agentsQuery.ToList();
        }

        public JsonResult GetEcsByRegion(int regionId)
        {
            var ecs = _context.ECs
                .Where(e => e.ECRegionID == regionId)
                .Select(e => new { value = e.ECID, text = e.PhysicalAddress })
                .ToList();

            return Json(ecs);
        }

        public JsonResult GetAgentsByEc(int ecId)
        {
            var agents = _context.Employee
                .Where(e => e.ECID == ecId && (e.Title == "Agent" || e.Title == "TeamLead"))
                .Select(e => new { value = e.EmployeeNumber, text = e.EmployeeName })
                .ToList();

            return Json(agents);
        }


        private IActionResult HandleCommonRole(int month, int year, int? employeeNumber, List<Employee> agents)
        {
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

        #endregion


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
