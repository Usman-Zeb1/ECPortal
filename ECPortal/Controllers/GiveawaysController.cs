using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class GiveawaysController : Controller
    {

        [Authorize(Roles = "Admin, TeamLead, HOD, ECM, RCCH")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
