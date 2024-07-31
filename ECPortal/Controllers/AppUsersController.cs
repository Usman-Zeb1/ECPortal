using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize(Roles = "Admin, TeamLead, RCCH, HOD, ECM")]
    public class AppUsersController : Controller
    {
        private readonly ECContext _context;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUsersController(ECContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
   
            _userManager = userManager;
            _roleManager = roleManager;
        }


       

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(); // or handle it as per your requirement
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            // Expression to filter AppUsers based on role
            Expression<Func<Employee, bool>> predicate;

            var Users = _context.AppUsers
                            // .Where(predicate)
                            .Where(w => w.Id != null)
                            .OrderByDescending(o => o.ModifiedDate).ToList();

            return View(Users);

        }


        //public async Task<IActionResult> NonProfilerList()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return Unauthorized(); // or handle it as per your requirement
        //    }

        //    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

        //    // Expression to filter AppUsers based on role
        //    Expression<Func<Employee, bool>> predicate;

        //    //var appUsers = _context.AppUsers
        //    //                // .Where(predicate)
        //    //                .Where(w => w.Id != null)
        //    //                .OrderByDescending(o => o.ModifiedDate).ToList();

        //    //return View(appUsers);
        //    var appUsers = await _context.AppUsers
        //    .Where(appUser => !_context.Employee.Any(e => e.AppUserId == appUser.Id))
        //    .OrderByDescending(o => o.ModifiedDate)
        //    .ToListAsync();

        //    return View("Index", appUsers);
        //}

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Employee
                .FirstOrDefaultAsync(m =>  int.Parse(m.AppUserId) == id);

            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public async Task<IActionResult> Create()
        {
            // Assuming GetRoleNameAsync returns a dictionary or list of roles
            var roleNames = await GetRoleNameAsync();
            ViewData["UserRole"] = new SelectList(roleNames, "Key", "Value");

            // Assuming _identityContext.Users contains a collection of AppUser
            var usersWithoutConfirmedPhone = _context.Users
                .Where(w => !w.PhoneNumberConfirmed)
                .Select(user => new { user.Email })
                .ToList(); // You can use ToListAsync if the context supports async operations

            ViewData["UserAdLogin"] = new SelectList(usersWithoutConfirmedPhone, "Email", "Email");

            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,UserDisplayName,UserAdLogin,UserRole,Remarks,IsEnabled")] Employee model)
        {
            model.EditBy = GetAppUserId();

            if (ModelState.IsValid)
            {
                await UpdateIdentityUser(model);

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserRole"] = new SelectList(AppUser.RoleTypes(await GetRoleNameAsync()), "Key", "Value", model.UserRole);
            ViewData["UserAdLogin"] = new SelectList(_identityContext.Users.Where(w => !w.PhoneNumberConfirmed), "Email", "Email");

            return View(model);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber, EmployeeName, UserAdLogin, ProfilePicture, Summary, IsEnabled")] Employee model)
        {
            model.EditBy = GetAppUserId(); // Assuming GetAppUserId() returns the current user's ID


            if (ModelState.IsValid)
            {
                await UpdateIdentityUser(model);

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            var user = await _userManager.FindByIdAsync(model.AppUserId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewData["UserRole"] = new SelectList(roles);
            }

           // ViewData["UserRole"] = new SelectList(AppUser.RoleTypes(await GetRoleNameAsync()), "Key", "Value", model.UserRole);
            ViewData["UserAdLogin"] = new SelectList(_context.Users.Where(w => !w.PhoneNumberConfirmed), "Email", "Email");

            return View(model);
        }


        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Employee.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }


            /*ViewData["UserRole"] = new SelectList(AppUser.RoleTypes(await GetRoleNameAsync()), "Key", "Value", model.UserRole);*/

            var user = await _userManager.FindByIdAsync(model.AppUserId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewData["UserRole"] = new SelectList(roles);
            }

            ViewData["UserAdLogin"] = new SelectList(_context.Users, "Email", "Email", model.UserAdLogin);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppUserId,EmployeeId,UserDisplayName,UserRole,Remarks,IsEnabled")] Employee model)
        {
            if (id != int.Parse(model.AppUserId))
            {
                return NotFound();
            }


            var appUser = _context.Employee.AsNoTracking().FirstOrDefaultAsync(w => int.Parse(w.AppUserId) == id).Result;

            if (appUser == null)
            {
                return NotFound();
            }

            model.UserAdLogin = appUser.UserAdLogin; //model.UserAdLogin = _context.AppUsers.AsNoTracking().FirstOrDefaultAsync(w => w.AppUserId == id).Result.UserAdLogin;
            model.EditBy = GetAppUserId();
            model.ModifiedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateIdentityUser(model);

                    _context.Entry(model).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(int.Parse(model.AppUserId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            /* ViewData["UserRole"] = new SelectList(AppUser.RoleTypes(await GetRoleNameAsync()), "Key", "Value", model.UserRole);*/
            var user = await _userManager.FindByIdAsync(model.AppUserId);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                ViewData["UserRole"] = new SelectList(roles);
            }

            ViewData["UserAdLogin"] = new SelectList(_context.Users, "Email", "Email", model.UserAdLogin);

            return View(model);
        }

        private bool AppUserExists(int id)
        {
            return _context.Employee.Any(e => int.Parse(e.AppUserId) == id);
        }

        private string GetAppUserId()
        {
            return User.FindFirstValue(ClaimTypes.Name);
        }

        private async Task UpdateIdentityUser(Employee model)
        {

            AppUser identityUser = await _userManager.FindByEmailAsync(model.UserAdLogin);
           // IdentityRole identityRole = await _roleManager.FindByNameAsync(model.UserRole);

            // Retrieve the associated AppUser
            var appUser = await _userManager.FindByIdAsync(model.AppUserId.ToString());


            // Find the role by name
            IdentityRole identityRole = await _roleManager.FindByIdAsync(appUser.Id);

          

            if (!model.IsEnabled)
            {
                await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.Now.AddMonths(1000));
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.Now);

            }

            if (!identityUser.PhoneNumberConfirmed)
            {
                var phoneNumber = string.IsNullOrWhiteSpace(identityUser.PhoneNumber) ? "03001234567" : identityUser.PhoneNumber;
                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(identityUser, phoneNumber);
                await _userManager.ChangePhoneNumberAsync(identityUser, phoneNumber, token);
            }

            var roles = await _userManager.GetRolesAsync(identityUser);

            if (roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(identityUser, role);
                }
            }

            await _userManager.AddToRoleAsync(identityUser, identityRole.Name);

        }

        private async Task<string> GetRoleNameAsync()
        {
            var user = await _userManager.FindByNameAsync(GetAppUserId());
            var roles = await _userManager.GetRolesAsync(user);

            var rolename = roles.FirstOrDefault();

            return rolename ?? "unknown";
        }
    }
}
