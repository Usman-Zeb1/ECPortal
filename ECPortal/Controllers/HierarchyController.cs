using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public HierarchyController(ECContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(string id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Use the provided id or fall back to the userId
            var effectiveId = !string.IsNullOrEmpty(id) ? id : userId;
            var viewModel = new EmployeeAppUserViewModel
            {
                Employee = _context.Employee.FirstOrDefault(e => e.AppUserId == effectiveId) ?? new Employee(),
                AppUser = _context.AppUsers.Find(effectiveId) ?? new AppUser()
            };

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
            var Users = await _context.AppUsers
                .Where(appUser => _context.Employee.Any(e => e.AppUserId == appUser.Id))
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
           var appUsers = await _context.AppUsers
                .Where(appUser => !_context.Employee.Any(e => e.AppUserId == appUser.Id))
                .OrderByDescending(o => o.ModifiedDate)
                .ToListAsync();

            // Serialize the list to a string (you can choose how to serialize, e.g., JSON)
            var serializedUsers = JsonConvert.SerializeObject(appUsers);

            // Redirect to the Index action of the AppUsers controller with the serialized data
            return View("Index",appUsers);
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
                return RedirectToAction("Index", "AppUsers");
            }
            return View();
        }

    }
}
