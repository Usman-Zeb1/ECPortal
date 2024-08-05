using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.ViewModels;
using System.Security.Claims;


namespace Pk.Com.Jazz.ECP.Controllers
{
    [Authorize]
    public class PerformanceController : Controller
    {
        private readonly ECContext _context;

        public PerformanceController(ECContext context)
        {
            _context = context;
        }
        /* public IActionResult Index()
         {
             var agentId = GetCurrentAgentId(); // Implement this method to get the current agent's ID
             var performanceData = _context.EmployeePerformances
                 .Where(p => p.EmployeeId == agentId)
                 .ToList() ?? null;

             return View(performanceData);


         }*/
        public IActionResult Index(int month = 0, int year = 0, int? employeeNumber = null)
        {
            if (month == 0) month = DateTime.Now.Month;
            if (year == 0) year = DateTime.Now.Year;

            if (User.IsInRole("Agent"))
            {
                employeeNumber = GetEmployeeNumber();

                if (employeeNumber == null)
                {
                    return NotFound("Employee not found.");
                }

                // Fetch performance data for the agent
                var performanceModel = GetPerformanceModel(employeeNumber.Value, month, year);
                if (performanceModel == null)
                {
                    return View();
                }
                return View(performanceModel);
            }
            else
            {
                var agents = _context.Employee
                    .Select(e => new { e.EmployeeNumber, e.EmployeeName })
                    .ToList();

                ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                if (employeeNumber.HasValue)
                {
                    // Fetch performance data for the selected employee
                    var performanceModel = GetPerformanceModel(employeeNumber.Value, month, year);
                    if (performanceModel != null)
                    {
                        return View("AgentsPerformance", performanceModel);
                    }
                }

                return View("AgentsPerformance");
            }
        }

        private EmployeePerformanceViewModel GetPerformanceModel(int employeeNumber, int month, int year)
        {
            var dailySales = _context.EmployeeSales
                .Where(a => a.EmployeeNumber == employeeNumber && a.SalesDate.Month == month && a.SalesDate.Year == year)
                .ToList();

            var employeeTargets = _context.EmployeeTargets
                .FirstOrDefault(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

            if (employeeTargets == null || dailySales == null)
            {
                return null;
            }

            // Fetch related data from each sales table
            var prepaidSales = _context.EmployeePrepaidSales
                .Where(p => dailySales.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id))
                .Sum(p => p.Total);

            var postpaidSales = _context.EmployeePostpaidSales
                .Where(p => dailySales.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id))
                .Sum(p => p.Total);

            var deviceSales = _context.EmployeeDeviceSales
                .Where(d => dailySales.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id))
                .Sum(d => d.Total);

            var mWalletSales = _context.EmployeeMWalletSales
                .Where(m => dailySales.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id))
                .Sum(m => m.Total);

            var fourGSales = _context.EmployeeFourGSales
                .Where(f => dailySales.Select(d => d.EmployeeFourGSaleId).Contains(f.Id))
                .Sum(f => f.Total);

            var roxNewSales = _context.EmployeeRoxNewSales
                .Where(r => dailySales.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id))
                .Sum(r => r.Total);

            var roxConversionSales = _context.EmployeeRoxConversionSales
                .Where(r => dailySales.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id))
                .Sum(r => r.Total);

            // Create the performance model
            return new EmployeePerformanceViewModel
            {
                Month = employeeTargets.Month,
                Year = employeeTargets.Year,
                PrepaidSalesTarget = employeeTargets.EmployeePrepaidSaleTarget,
                PostpaidSalesTarget = employeeTargets.EmployeePostpaidSaleTarget,
                DeviceSalesTarget = employeeTargets.EmployeeDeviceSaleTarget,
                MWalletSalesTarget = employeeTargets.EmployeeMWalletSaleTarget,
                FourGSalesTarget = employeeTargets.EmployeeFourGSaleTarget,
                RoxNewSalesTarget = employeeTargets.EmployeeRoxNewSaleTarget,
                RoxConversionSalesTarget = employeeTargets.EmployeeRoxConversionSaleTarget,
                TotalPrepaidSales = prepaidSales,
                TotalPostpaidSales = postpaidSales,
                TotalDeviceSales = deviceSales,
                TotalMWalletSales = mWalletSales,
                TotalFourGSales = fourGSales,
                TotalRoxNewSales = roxNewSales,
                TotalRoxConversionSales = roxConversionSales,
                PrepaidSalesPerformance = CalculatePerformance(prepaidSales, employeeTargets.EmployeePrepaidSaleTarget),
                PostpaidSalesPerformance = CalculatePerformance(postpaidSales, employeeTargets.EmployeePostpaidSaleTarget),
                DeviceSalesPerformance = CalculatePerformance(deviceSales, employeeTargets.EmployeeDeviceSaleTarget),
                MWalletSalesPerformance = CalculatePerformance(mWalletSales, employeeTargets.EmployeeMWalletSaleTarget),
                FourGSalesPerformance = CalculatePerformance(fourGSales, employeeTargets.EmployeeFourGSaleTarget),
                RoxNewSalesPerformance = CalculatePerformance(roxNewSales, employeeTargets.EmployeeRoxNewSaleTarget),
                RoxConversionSalesPerformance = CalculatePerformance(roxConversionSales, employeeTargets.EmployeeRoxConversionSaleTarget)
            };
        }



        private double CalculatePerformance(int totalSales, int target)
        {
            return target > 0 ? Math.Round(((double)totalSales / target) * 100) : 0;
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
