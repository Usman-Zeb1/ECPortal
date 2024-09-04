using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pk.Com.Jazz.ECP.Controllers
{
    public class HierarchyController : Controller
    {
        private readonly ECContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
      

        public HierarchyController(ECContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Use the provided id or fall back to the userId
            var effectiveId = !string.IsNullOrEmpty(id) ? id : userId;
            var viewModel = new EmployeeAppUserViewModel
            {
                Employee = _context.Employee.FirstOrDefault(e => e.AppUserId == effectiveId) ?? new Employee(),
                AppUser = _context.AppUsers.Find(effectiveId) ?? new AppUser()
            };

            var userRoles = await _userManager.GetRolesAsync(viewModel.AppUser);

            if (userRoles.Any())
            {
                viewModel.Employee.Title = userRoles.First(); // Assuming one role per user
            }

            // Repopulate dropdowns if model state is invalid
            //ViewBag.ExperienceCentres = _context.ECs.Select(ec => new SelectListItem
            //{
            //    Value = ec.ECID.ToString(),  // This will be assigned to Employee.ECID
            //    Text = ec.PhysicalAddress    // This will be displayed in the dropdown
            //}).ToList();


            //// Populate region dropdown if model state is valid
            //ViewBag.Regions = _context.ECRegions.Select(ec => new SelectListItem
            //{
            //    Value = ec.ECRegionID.ToString(),
            //    Text = ec.ECRegionName
            //}).ToList();

            // Fetch specific values for Experience Centre, Manager, and Region based on the employee data
            var experienceCentre = _context.ECs.FirstOrDefault(ec => ec.ECID == viewModel.Employee.ECID);
            var manager = _context.Employee.FirstOrDefault(e => e.EmployeeId == viewModel.Employee.ManagerID);
            var region = _context.ECRegions.FirstOrDefault(r => r.ECRegionID == viewModel.Employee.RegionID);

            // Populate ViewBag with specific values
            ViewBag.ExperienceCentre = experienceCentre?.PhysicalAddress;
            ViewBag.ManagerName = _context.Employee.FirstOrDefault(e => e.EmployeeNumber == viewModel.Employee.ManagerID);
            ViewBag.Region = region?.ECRegionName;


            var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var empNumber = _context.Employee.FirstOrDefault(e => e.AppUserId == userId)?.EmployeeNumber;
            var title = _context.Employee.FirstOrDefault(e => e.AppUserId == Id)?.Title;
            var reporting = _context.Employee
                             .Where(e => e.Title == title)
                             .Select(e => new SelectListItem
                             {
                                 Value = e.EmployeeNumber.ToString(),
                                 Text = e.UserAdLogin
                             })
                             .ToList();

            ViewBag.reporting = reporting;



            if (id == null) {
                return View("Hierarchy", viewModel);
            }

            return View(viewModel);
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        employee.EditBy = HttpContext.Session.GetString("UserID"); 
        //        _context.Employee.Add(employee);
        //        await _context.SaveChangesAsync();
        //        ViewBag.UploadStatus = "Success";
        //        return RedirectToAction("Index", "AppUsers");
        //    }
        //    return View();
        //}


        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var empNumber = _context.Employee.FirstOrDefault(e => e.AppUserId == userId)?.EmployeeNumber;

            var Users = await _context.AppUsers
                .Where(appUser => _context.Employee.Any(e => e.AppUserId == appUser.Id)&&
                       _context.Employee.Any(e => e.ManagerID == empNumber && e.AppUserId == appUser.Id)
                )
                .OrderByDescending(o => o.ModifiedDate)
                .ToListAsync();
            return View(Users);

        }

        //public async Task<IActionResult> NonProfilerList()
        //{
        //    List<AppUser> appUsers = await _context.AppUsers
        //    .Where(appUser => !_context.Employee.Any(e => e.AppUserId == appUser.Id))
        //    .OrderByDescending(o => o.ModifiedDate)
        //    .ToListAsync();

        //    return RedirectToAction("Index", "AppUsers", appUsers );

        //}

        public async Task<IActionResult> NonProfilerList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var empNumber = _context.Employee.FirstOrDefault(e => e.AppUserId == userId)?.EmployeeNumber;
            var title = _context.Employee.FirstOrDefault(e => e.AppUserId == userId)?.Title;
            if (title == "ECM")
            {

                //var appUsers = await _context.AppUsers
                //        .Where(appUser =>
                //            !_context.Employee.Any(e => e.AppUserId == appUser.Id) 
                //        )
                //        .OrderByDescending(o => o.ModifiedDate)
                //        .ToListAsync();
                // Get the role ID for "TeamLead"
                var teamLeadRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "TeamLead");

                var appUsers = await _context.AppUsers
                                 .Where(appUser =>
                                     !_context.Employee.Any(e => e.AppUserId == appUser.Id) &&  // Exclude users present in the Employee table
                                     _context.UserRoles.Any(ur => ur.UserId == appUser.Id && ur.RoleId == teamLeadRole.Id)  // Check if user has "TeamLead" role
                                 )
                                 .OrderByDescending(o => o.ModifiedDate)
                                 .ToListAsync();

                var serializedUsers = JsonConvert.SerializeObject(appUsers);
            return View("Index",appUsers);
            }
            else if (title == "TeamLead")
            {

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = await _context.Employee
                                                     .FirstOrDefaultAsync(e => e.AppUserId == employee.AppUserId);

                if (existingEmployee != null)
                {
                    // Update existing employee details
                    existingEmployee.EmployeeNumber = employee.EmployeeNumber;
                    existingEmployee.EmployeeName = employee.EmployeeName;
                    existingEmployee.CNIC = employee.CNIC;
                    existingEmployee.MobileNumber = employee.MobileNumber;
                    existingEmployee.UserAdLogin = employee.UserAdLogin;
                    existingEmployee.Title = employee.Title;
                    existingEmployee.IsEnabled = employee.IsEnabled;
                    existingEmployee.DOJ = employee.DOJ;
                    existingEmployee.DateOfJoiningBC = employee.DateOfJoiningBC;
                    existingEmployee.DateOfLeaving = employee.DateOfLeaving;
                    existingEmployee.EmailAddress = employee.EmailAddress;
                    existingEmployee.ECID = employee.ECID;
                    existingEmployee.ManagerID = employee.ManagerID;
                    existingEmployee.DeviceIMIE = employee.DeviceIMIE;
                    existingEmployee.PosIds = employee.PosIds;
                    existingEmployee.PosName = employee.PosName;
                    existingEmployee.SalesId = employee.SalesId;
                    existingEmployee.WaridSalesId = employee.WaridSalesId;
                    existingEmployee.TabsId = employee.TabsId;
                    existingEmployee.MfsId = employee.MfsId;
                    existingEmployee.SiebelId = employee.SiebelId;
                    existingEmployee.EficsId = employee.EficsId;
                    existingEmployee.EficsId2 = employee.EficsId2;
                    existingEmployee.QmaticLogin = employee.QmaticLogin;
                    existingEmployee.QmaticPowerLogin = employee.QmaticPowerLogin;
                    existingEmployee.EditBy = HttpContext.Session.GetString("UserID");
                    existingEmployee.RegionID = employee.RegionID;

                    _context.Employee.Update(existingEmployee);
                }
                else
                {
                    // Create a new employee
                    employee.EditBy = HttpContext.Session.GetString("UserID");
                    _context.Employee.Add(employee);
                }

                await _context.SaveChangesAsync();
                ViewBag.UploadStatus = "Success";
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
