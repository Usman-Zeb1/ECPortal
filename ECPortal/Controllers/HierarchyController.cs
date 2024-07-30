using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
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
            var viewModel = new EmployeeAppUserViewModel
            {
                Employee = !string.IsNullOrEmpty(id) ? _context.Employee.FirstOrDefault(e => e.AppUserId == id) : new Employee(),
                AppUser = !string.IsNullOrEmpty(id) ? _context.AppUsers.Find(id) : new AppUser()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EditBy = HttpContext.Session.GetString("UserID"); 
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
                ViewBag.UploadStatus = "Success";
                return RedirectToAction("Index", "AppUsers");
            }
            return View();
        }
    }
}
