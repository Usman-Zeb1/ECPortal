using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System.Security.Claims;

namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class QuizScoresController : Controller
    {
        private readonly ECContext _context;
        public QuizScoresController(ECContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeId;



            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or handle the case appropriately
            }

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            if (User.IsInRole("Agent"))
            {
                // Retrieve the latest quiz score for the current month
                var quizScoreForCurrentMonth = _context.QuizScores
                .Where(e => e.EmployeeId == agentId && e.QuizDate.Month == currentMonth && e.QuizDate.Year == currentYear)
                .OrderByDescending(e => e.QuizDate)
                .FirstOrDefault();

                // Convert the single result to a list if needed for the view
                var quizScoresList = new List<Pk.Com.Jazz.ECP.Models.QuizScores>();
                if (quizScoreForCurrentMonth != null)
                {
                    quizScoresList.Add(quizScoreForCurrentMonth);
                }

                return View(quizScoresList);

            }

            else if (User.IsInRole("ECM"))
            {
                // Retrieve the latest quiz score for the current month
                var quizScoreForCurrentMonth = _context.ManagersScores
                .Where(e => e.EmployeeId == agentId && e.QuizDate.Month == currentMonth && e.QuizDate.Year == currentYear)
                .OrderByDescending(e => e.QuizDate)
                .FirstOrDefault();

                // Convert the single result to a list if needed for the view
                var quizScoresList = new List<Pk.Com.Jazz.ECP.Models.ManagersScores>();
                if (quizScoreForCurrentMonth != null)
                {
                    quizScoresList.Add(quizScoreForCurrentMonth);
                }

                // Return to ECMScores view with the quizScoresList
                return View("ECMScores", quizScoresList);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult FilterQuizScores(int? month, int? year)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agentId = _context.Employee.FirstOrDefault(a => a.AppUserId == userId)?.EmployeeId;

            if (userId == null)
            {
                return RedirectToAction("Index", "Home"); // Or handle the case appropriately
            }
            var quizScores = _context.QuizScores.AsQueryable();

            if (month.HasValue && year.HasValue)
            {
                quizScores = quizScores.Where(q => q.EmployeeId == agentId && q.QuizDate.Month == month.Value && q.QuizDate.Year == year.Value);
            }
            else if (month.HasValue)
            {
                quizScores = quizScores.Where(q => q.QuizDate.Month == month.Value);
            }
            else if (year.HasValue)
            {
                quizScores = quizScores.Where(q => q.QuizDate.Year == year.Value);
            }

            var model = quizScores.ToList();

            return View("Index", model);
        }


        [HttpGet]
        public IActionResult GetECEmployeesQuizScores()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get the manager's EmployeeId and Experience Center ID (ECID)
            var manager = _context.Employee.FirstOrDefault(a => a.AppUserId == userId);
            if (manager == null)
            {
                return NotFound("Manager not found.");
            }

            var managerECID = manager.ECID;

            // Get the current month and year
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            // Get quiz scores along with employee names for the current month
            var quizScores = (from quiz in _context.QuizScores
                              join emp in _context.Employee on quiz.EmployeeId equals emp.EmployeeId
                              where emp.ECID == managerECID
                                    && quiz.QuizDate.Month == currentMonth
                                    && quiz.QuizDate.Year == currentYear
                              select new QuizScoreViewModel
                              {
                                  Id = quiz.Id,
                                  EmployeeId = quiz.EmployeeId,
                                  EmployeeName = emp.EmployeeName,
                                  QuizDate = quiz.QuizDate,
                                  asTarget = quiz.asTarget,
                                  AgentSatisfaction = quiz.AgentSatisfaction,
                                  asPercentage = quiz.asPercentage,
                                  vsTarget = quiz.vsTarget,
                                  visitSatisfaction = quiz.visitSatisfaction,
                                  vsPercentage = quiz.vsPercentage,
                                  QuizTarget = quiz.QuizTarget,
                                  QuizOnline = quiz.QuizOnline,
                                  QuizPercentage = quiz.QuizPercentage,
                                  RamTarget = quiz.RamTarget,
                                  FatalError = quiz.FatalError,
                                  RamPercentage = quiz.RamPercentage,
                                  ResponsesCount = quiz.ResponsesCount
                              }).ToList();

            // Passing the data directly to the Index view
            return View("EcScores", quizScores);
        }

        [HttpGet]
        public IActionResult FilterECQuizScores(int month, int year)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get the manager's EmployeeId and Experience Center ID (ECID)
            var manager = _context.Employee.FirstOrDefault(a => a.AppUserId == userId);
            if (manager == null)
            {
                return NotFound("Manager not found.");
            }

            var managerECID = manager.ECID;

            // Validate the input month and year
            if (month <= 0 || month > 12 || year <= 0)
            {
                return BadRequest("Invalid month or year.");
            }

            // Get quiz scores along with employee names for the selected month and year
            var quizScores = (from quiz in _context.QuizScores
                              join emp in _context.Employee on quiz.EmployeeId equals emp.EmployeeId
                              where emp.ECID == managerECID
                                    && quiz.QuizDate.Month == month
                                    && quiz.QuizDate.Year == year
                              select new QuizScoreViewModel
                              {
                                  Id = quiz.Id,
                                  EmployeeId = quiz.EmployeeId,
                                  EmployeeName = emp.EmployeeName,
                                  QuizDate = quiz.QuizDate,
                                  asTarget = quiz.asTarget,
                                  AgentSatisfaction = quiz.AgentSatisfaction,
                                  asPercentage = quiz.asPercentage,
                                  vsTarget = quiz.vsTarget,
                                  visitSatisfaction = quiz.visitSatisfaction,
                                  vsPercentage = quiz.vsPercentage,
                                  QuizTarget = quiz.QuizTarget,
                                  QuizOnline = quiz.QuizOnline,
                                  QuizPercentage = quiz.QuizPercentage,
                                  RamTarget = quiz.RamTarget,
                                  FatalError = quiz.FatalError,
                                  RamPercentage = quiz.RamPercentage,
                                  ResponsesCount = quiz.ResponsesCount
                              }).ToList();

            // Pass the filtered data to the Index view
            return View("EcScores", quizScores);
        }



        [HttpGet]
        public async Task<IActionResult> GetECQuizScoresAsync(int? experienceCenterId)
        {
            // Populate experience centers in the dropdown
            ViewBag.ExperienceCenters = _context.ECs.Select(ec => new { ec.ECID, ec.PhysicalAddress }).ToList();

            // If no experience center is selected, return the view with an empty list
            if (!experienceCenterId.HasValue)
            {
                return View("NationwideScores", new List<QuizScoreViewModel>());
            }

            //// Fetch employees in the selected experience center
            //var employeesInEC = _context.Employee
            //    .Where(e => e.ECID == experienceCenterId.Value)
            //    .ToList();

            // Get the current month and year (or modify to filter by other conditions)
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            //// Fetch quiz scores for the employees of the selected experience center
            //var quizScores = (from quiz in _context.QuizScores
            //                  join emp in employeesInEC on quiz.EmployeeId equals emp.EmployeeId
            //                  where quiz.QuizDate.Month == currentMonth && quiz.QuizDate.Year == currentYear
            //                  select new QuizScoreViewModel
            //                  {
            //                      EmployeeName = emp.EmployeeName,
            //                      QuizDate = quiz.QuizDate,
            //                      asTarget = quiz.asTarget,
            //                      AgentSatisfaction = quiz.AgentSatisfaction,
            //                      asPercentage = quiz.asPercentage,
            //                      vsTarget = quiz.vsTarget,
            //                      visitSatisfaction = quiz.visitSatisfaction,
            //                      vsPercentage = quiz.vsPercentage,
            //                      QuizTarget = quiz.QuizTarget,
            //                      QuizOnline = quiz.QuizOnline,
            //                      QuizPercentage = quiz.QuizPercentage
            //                  }).ToList();

            //// Return the populated list to the view
            //return View("NationwideScores", quizScores);

            // Fetch all relevant quiz scores and employees separately
            var employees = await _context.Employee
                .Where(e => e.ECID == experienceCenterId)
                .ToListAsync();

            var quizScores = await _context.QuizScores
            .Where(q => q.InsertDate.Month == currentMonth && q.InsertDate.Year == currentYear)
                .ToListAsync();

            // Join the results in memory
            var result = quizScores
                .Join(employees,
                      quiz => quiz.EmployeeId,
                      emp => emp.EmployeeId,
                      (quiz, emp) => new
                      {
                          Quiz = quiz,
                          Employee = emp
                      })
                .ToList();


            
            // Map the results to the expected view model
            var viewModel = result.Select(result => new QuizScoreViewModel
            {
                // Assuming QuizScoreViewModel has properties corresponding to Quiz and Employee
                Id = result.Quiz.Id,
                EmployeeId = result.Quiz.EmployeeId,
                EmployeeName = result.Employee.EmployeeName,
                QuizDate = result.Quiz.QuizDate,
                asTarget = result.Quiz.asTarget,
                AgentSatisfaction = result.Quiz.AgentSatisfaction,
                asPercentage = result.Quiz.asPercentage,
                vsTarget = result.Quiz.vsTarget,
                visitSatisfaction = result.Quiz.visitSatisfaction,
                vsPercentage = result.Quiz.vsPercentage,
                QuizTarget = result.Quiz.QuizTarget,
                QuizOnline = result.Quiz.QuizOnline,
                QuizPercentage = result.Quiz.QuizPercentage,
                RamTarget = result.Quiz.RamTarget,
                FatalError = result.Quiz.FatalError,
                RamPercentage = result.Quiz.RamPercentage,
                ResponsesCount = result.Quiz.ResponsesCount
            }).ToList();

            return View("NationwideScores", viewModel);
        }



    }
}
