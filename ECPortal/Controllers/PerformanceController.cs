using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using Pk.Com.Jazz.ECP.Utilities;
using Pk.Com.Jazz.ECP.ViewModels;
using System.Linq;
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
        public async Task<IActionResult> Index(int month = 0, int year = 0, int? employeeNumber = null, int? regionId = null, int? ecId = null)
        {
           /* if (month == 0) month = DateTime.Now.Month;
            if (year == 0) year = DateTime.Now.Year;*/

            if (User.IsInRole("Agent"))
            {
                employeeNumber = GetEmployeeNumber();

                if (employeeNumber == null)
                {
                    return NotFound("Employee not found.");
                }

                // Fetch performance data for the agent
                var performanceModel = await GetPerformanceModel(employeeNumber.Value, month, year);
                if (performanceModel == null)
                {
                    return View();
                }



                return View(performanceModel);
            }
            else if (User.IsInRole("TL") || User.IsInRole("ECM"))
            {
                var agents = _context.Employee
                    .Select(e => new { e.EmployeeNumber, e.EmployeeName })
                    .ToList();

                ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                if (employeeNumber.HasValue)
                {
                    // Fetch performance data for the selected employee
                    var performanceModel = await GetPerformanceModel(employeeNumber.Value, month, year);
                    if (performanceModel != null)
                    {
                        return View("AgentsPerformance", performanceModel);
                    }
                }

                return View("AgentsPerformance");
            }
            else if (User.IsInRole("RCCH"))
            {

                var currentEmployee = GetCurrentEmployee();
               


                if (currentEmployee == null)
                {
                    return NotFound("Employee not found.");
                }

                regionId = currentEmployee.RegionID;

                var agents = _context.Employee
                    .Where(e => e.RegionID == regionId)
                    .Select(e => new { e.EmployeeNumber, e.EmployeeName })
                    .ToList();

                ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                if (employeeNumber.HasValue)
                {
                    // Fetch performance data for the selected employee
                    var performanceModel = await GetPerformanceModel(employeeNumber.Value, month, year);
                    if (performanceModel != null)
                    {
                        return View("AgentsPerformance", performanceModel);
                    }
                }

                return View("AgentsPerformance");
            }
            else if (User.IsInRole("HOD") || User.IsInRole("Admin"))
            {
                var regions = _context.ECRegions
                    .Select(r => new { r.ECRegionID, r.ECRegionName })
                    .ToList();

                var ecs = _context.ECs
                    .Select(e => new { e.ECID, e.PhysicalAddress })
                    .ToList();

                ViewBag.Regions = new SelectList(regions, "ECRegionID", "ECRegionName");
                ViewBag.ECs = new SelectList(ecs, "ECID", "PhysicalAddress");

                if (regionId.HasValue || ecId.HasValue)
                {
                    var agentsQuery = _context.Employee.AsQueryable();

                    if (regionId.HasValue)
                    {
                        agentsQuery = agentsQuery.Where(e => e.RegionID == regionId);
                    }

                    if (ecId.HasValue)
                    {
                        agentsQuery = agentsQuery.Where(e => e.ECID == ecId);
                    }

                    var agents = await agentsQuery
                        .Select(e => new { e.EmployeeNumber, e.EmployeeName })
                        .ToListAsync();

                    ViewBag.Agents = new SelectList(agents, "EmployeeNumber", "EmployeeName");

                    if (employeeNumber.HasValue)
                    {
                        // Fetch performance data for the selected employee
                        var performanceModel = await GetPerformanceModel(employeeNumber.Value, month, year);
                        if (performanceModel != null)
                        {
                            return View("AgentsPerformance", performanceModel);
                        }
                    }
                }

                return View("AgentsPerformance");
            }

            return View();
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
                .Where(e => e.ECID == ecId)
                .Select(e => new { value = e.EmployeeNumber, text = e.EmployeeName })
                .ToList();

            return Json(agents);
        }


        private string GetUserId()
        {
            // Implement logic to get the user ID for the current user
            // This can be based on the user's profile or other data in your system
            return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }



        [Authorize(Roles = "HOD, ECM, RCCH, Admin")]
        public async Task<IActionResult> ECPerformance(int month = 0, int year = 0, int? ECID = null)
        {
            // Set default month and year if not provided
            if (month == 0) month = DateTime.Now.Month;
            if (year == 0) year = DateTime.Now.Year;

            if (User.IsInRole("HOD"))
            {
                // Fetch ECs and populate the ViewBag
                var ecs = _context.ECs.Include(t => t.ECRegion)
                    .Select(e => new { e.ECID, e.PhysicalAddress, e.ECRegion.ECRegionName })
                    .ToList();

                ViewBag.ECs = new SelectList(ecs, "ECID", "PhysicalAddress");

                if (ECID.HasValue)
                {
                    // Fetch performance data for the selected EC
                    var performanceModel = await GetECPerformanceModel(ECID.Value, month, year);
                    if (performanceModel != null)
                    {
                        return View("ECPerformance", performanceModel);
                    }
                   
                }
                return View("ECPerformance");
            }
            else
            {
                // Return a view for non-HOD users
                return View(); // Replace "NonHODView" with the appropriate view name for non-HOD users
            }
        }


        private async Task<ECPerformanceViewModel> GetECPerformanceModel(int ECID, int month, int year)
        {
            var dailySales = await _context.ECSales
                .Where(a => a.ECID == ECID && a.SalesDate.Month == month && a.SalesDate.Year == year)
                .ToListAsync();

            var ECTargets = await _context.ECTargets
                .FirstOrDefaultAsync(t => t.Month == month && t.Year == year && t.ECID == ECID);

            if (ECTargets == null || dailySales == null)
            {
                return null;
            }

            var salesIds = dailySales.Select(d => new
            {
                d.ECPrepaidSaleId,
                d.ECPostpaidSaleId,
                d.ECDeviceSaleId,
                d.ECMWalletSaleId,
                d.ECFourGSaleId,
                d.ECRoxNewSaleId,
                d.ECRoxConversionSaleId
            }).ToList();

            var prepaidSales = await _context.ECPrepaidSales
                .Where(p => salesIds.Select(s => s.ECPrepaidSaleId).Contains(p.Id))
                .ToListAsync();

            var postpaidSales = await _context.ECPostpaidSales
                .Where(p => salesIds.Select(s => s.ECPostpaidSaleId).Contains(p.Id))
                .ToListAsync();

            var deviceSales = await _context.ECDeviceSales
                .Where(d => salesIds.Select(s => s.ECDeviceSaleId).Contains(d.Id))
                .ToListAsync();

            var mWalletSales = await _context.ECMWalletSales
                .Where(m => salesIds.Select(s => s.ECMWalletSaleId).Contains(m.Id))
                .ToListAsync();

            var fourGSales = await _context.ECFourGSales
                .Where(f => salesIds.Select(s => s.ECFourGSaleId).Contains(f.Id))
                .ToListAsync();

            var roxNewSales = await _context.ECRoxNewSales
                .Where(r => salesIds.Select(s => s.ECRoxNewSaleId).Contains(r.Id))
                .ToListAsync();

            var roxConversionSales = await _context.ECRoxConversionSales
                .Where(r => salesIds.Select(s => s.ECRoxConversionSaleId).Contains(r.Id))
                .ToListAsync();

            var dailyPerformances = dailySales.GroupBy(d => d.SalesDate.Date)
                .Select(g => new DailyPerformanceViewModel
                {
                    Date = g.Key,
                    PrepaidSales = prepaidSales.Where(p => g.Select(d => d.ECPrepaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    PostpaidSales = postpaidSales.Where(p => g.Select(d => d.ECPostpaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    DeviceSales = deviceSales.Where(d => g.Select(ds => ds.ECDeviceSaleId).Contains(d.Id)).Sum(d => d.Total),
                    MWalletSales = mWalletSales.Where(m => g.Select(d => d.ECMWalletSaleId).Contains(m.Id)).Sum(m => m.Total),
                    FourGSales = fourGSales.Where(f => g.Select(d => d.ECFourGSaleId).Contains(f.Id)).Sum(f => f.Total),
                    RoxNewSales = roxNewSales.Where(r => g.Select(d => d.ECRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total),
                    RoxConversionSales = roxConversionSales.Where(r => g.Select(d => d.ECRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    TotalSales = prepaidSales.Where(p => g.Select(d => d.ECPrepaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 postpaidSales.Where(p => g.Select(d => d.ECPostpaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 deviceSales.Where(d => g.Select(ds => ds.ECDeviceSaleId).Contains(d.Id)).Sum(d => d.Total) +
                                 mWalletSales.Where(m => g.Select(d => d.ECMWalletSaleId).Contains(m.Id)).Sum(m => m.Total) +
                                 fourGSales.Where(f => g.Select(d => d.ECFourGSaleId).Contains(f.Id)).Sum(f => f.Total) +
                                 roxNewSales.Where(r => g.Select(d => d.ECRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total) +
                                 roxConversionSales.Where(r => g.Select(d => d.ECRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    Performance = CalculatePerformance(
                        prepaidSales.Where(p => g.Select(d => d.ECPrepaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                        postpaidSales.Where(p => g.Select(d => d.ECPostpaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                        deviceSales.Where(d => g.Select(ds => ds.ECDeviceSaleId).Contains(d.Id)).Sum(d => d.Total) +
                        mWalletSales.Where(m => g.Select(d => d.ECMWalletSaleId).Contains(m.Id)).Sum(m => m.Total) +
                        fourGSales.Where(f => g.Select(d => d.ECFourGSaleId).Contains(f.Id)).Sum(f => f.Total) +
                        roxNewSales.Where(r => g.Select(d => d.ECRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total) +
                        roxConversionSales.Where(r => g.Select(d => d.ECRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                        ECTargets.ECPrepaidSaleTarget) // Adjust this to use daily targets if available
                }).ToList();

            // Create the performance model
            return new ECPerformanceViewModel
            {
                Month = ECTargets.Month,
                Year = ECTargets.Year,
                PrepaidSalesTarget = ECTargets.ECPrepaidSaleTarget,
                PostpaidSalesTarget = ECTargets.ECPostpaidSaleTarget,
                DeviceSalesTarget = ECTargets.ECDeviceSaleTarget,
                MWalletSalesTarget = ECTargets.ECMWalletSaleTarget,
                FourGSalesTarget = ECTargets.ECFourGSaleTarget,
                RoxNewSalesTarget = ECTargets.ECRoxNewSaleTarget,
                RoxConversionSalesTarget = ECTargets.ECRoxConversionSaleTarget,
                TotalPrepaidSales = prepaidSales.Sum(p => p.Total),
                TotalPostpaidSales = postpaidSales.Sum(p => p.Total),
                TotalDeviceSales = deviceSales.Sum(d => d.Total),
                TotalMWalletSales = mWalletSales.Sum(m => m.Total),
                TotalFourGSales = fourGSales.Sum(f => f.Total),
                TotalRoxNewSales = roxNewSales.Sum(r => r.Total),
                TotalRoxConversionSales = roxConversionSales.Sum(r => r.Total),
                PrepaidSalesPerformance = CalculatePerformance(prepaidSales.Sum(p => p.Total), ECTargets.ECPrepaidSaleTarget),
                PostpaidSalesPerformance = CalculatePerformance(postpaidSales.Sum(p => p.Total), ECTargets.ECPostpaidSaleTarget),
                DeviceSalesPerformance = CalculatePerformance(deviceSales.Sum(d => d.Total), ECTargets.ECDeviceSaleTarget),
                MWalletSalesPerformance = CalculatePerformance(mWalletSales.Sum(m => m.Total), ECTargets.ECMWalletSaleTarget),
                FourGSalesPerformance = CalculatePerformance(fourGSales.Sum(f => f.Total), ECTargets.ECFourGSaleTarget),
                RoxNewSalesPerformance = CalculatePerformance(roxNewSales.Sum(r => r.Total), ECTargets.ECRoxNewSaleTarget),
                RoxConversionSalesPerformance = CalculatePerformance(roxConversionSales.Sum(r => r.Total), ECTargets.ECRoxConversionSaleTarget),
                DailyPerformances = dailyPerformances
            };
        }

        private async Task<EmployeePerformanceViewModel> GetPerformanceModel(int employeeNumber, int month, int year)
        {
            var dailySales = await _context.EmployeeSales
                .Where(a => a.EmployeeNumber == employeeNumber && a.SalesDate.Month == month && a.SalesDate.Year == year)
                .ToListAsync();

            var employeeTargets = await _context.EmployeeTargets
                .FirstOrDefaultAsync(t => t.Month == month && t.Year == year && t.EmployeeNumber == employeeNumber);

            if (employeeTargets == null || dailySales == null)
            {
                return null;
            }

            var salesIds = dailySales.Select(d => new
            {
                d.EmployeePrepaidSaleId,
                d.EmployeePostpaidSaleId,
                d.EmployeeDeviceSaleId,
                d.EmployeeMWalletSaleId,
                d.EmployeeFourGSaleId,
                d.EmployeeRoxNewSaleId,
                d.EmployeeRoxConversionSaleId
            }).ToList();

            var prepaidSales = await _context.EmployeePrepaidSales
                .Where(p => salesIds.Select(s => s.EmployeePrepaidSaleId).Contains(p.Id))
                .ToListAsync();

            var postpaidSales = await _context.EmployeePostpaidSales
                .Where(p => salesIds.Select(s => s.EmployeePostpaidSaleId).Contains(p.Id))
                .ToListAsync();

            var deviceSales = await _context.EmployeeDeviceSales
                .Where(d => salesIds.Select(s => s.EmployeeDeviceSaleId).Contains(d.Id))
                .ToListAsync();

            var mWalletSales = await _context.EmployeeMWalletSales
                .Where(m => salesIds.Select(s => s.EmployeeMWalletSaleId).Contains(m.Id))
                .ToListAsync();

            var fourGSales = await _context.EmployeeFourGSales
                .Where(f => salesIds.Select(s => s.EmployeeFourGSaleId).Contains(f.Id))
                .ToListAsync();

            var roxNewSales = await _context.EmployeeRoxNewSales
                .Where(r => salesIds.Select(s => s.EmployeeRoxNewSaleId).Contains(r.Id))
                .ToListAsync();

            var roxConversionSales = await _context.EmployeeRoxConversionSales
                .Where(r => salesIds.Select(s => s.EmployeeRoxConversionSaleId).Contains(r.Id))
                .ToListAsync();

            var dailyPerformances = dailySales.GroupBy(d => d.SalesDate.Date)
                .Select(g => new DailyPerformanceViewModel
                {
                    Date = g.Key,
                    PrepaidSales = prepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    PostpaidSales = postpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    DeviceSales = deviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total),
                    MWalletSales = mWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total),
                    FourGSales = fourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total),
                    RoxNewSales = roxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total),
                    RoxConversionSales = roxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    TotalSales = prepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 postpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 deviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total) +
                                 mWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total) +
                                 fourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total) +
                                 roxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total) +
                                 roxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    Performance = CalculatePerformance(
                        prepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                        postpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                        deviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total) +
                        mWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total) +
                        fourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total) +
                        roxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total) +
                        roxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                        employeeTargets.EmployeePrepaidSaleTarget) // Adjust this to use daily targets if available
                }).ToList();

            // Create the performance model
            return new EmployeePerformanceViewModel
            {
                EmployeeEmail = _context.Employee
                                     .Where(e => e.EmployeeNumber == employeeNumber)
                                     .Select(e => e.EmailAddress).FirstOrDefault(),
                Month = employeeTargets.Month,
                Year = employeeTargets.Year,
                PrepaidSalesTarget = employeeTargets.EmployeePrepaidSaleTarget,
                PostpaidSalesTarget = employeeTargets.EmployeePostpaidSaleTarget,
                DeviceSalesTarget = employeeTargets.EmployeeDeviceSaleTarget,
                MWalletSalesTarget = employeeTargets.EmployeeMWalletSaleTarget,
                FourGSalesTarget = employeeTargets.EmployeeFourGSaleTarget,
                RoxNewSalesTarget = employeeTargets.EmployeeRoxNewSaleTarget,
                RoxConversionSalesTarget = employeeTargets.EmployeeRoxConversionSaleTarget,
                TotalPrepaidSales = prepaidSales.Sum(p => p.Total),
                TotalPostpaidSales = postpaidSales.Sum(p => p.Total),
                TotalDeviceSales = deviceSales.Sum(d => d.Total),
                TotalMWalletSales = mWalletSales.Sum(m => m.Total),
                TotalFourGSales = fourGSales.Sum(f => f.Total),
                TotalRoxNewSales = roxNewSales.Sum(r => r.Total),
                TotalRoxConversionSales = roxConversionSales.Sum(r => r.Total),
                PrepaidSalesPerformance = CalculatePerformance(prepaidSales.Sum(p => p.Total), employeeTargets.EmployeePrepaidSaleTarget),
                PostpaidSalesPerformance = CalculatePerformance(postpaidSales.Sum(p => p.Total), employeeTargets.EmployeePostpaidSaleTarget),
                DeviceSalesPerformance = CalculatePerformance(deviceSales.Sum(d => d.Total), employeeTargets.EmployeeDeviceSaleTarget),
                MWalletSalesPerformance = CalculatePerformance(mWalletSales.Sum(m => m.Total), employeeTargets.EmployeeMWalletSaleTarget),
                FourGSalesPerformance = CalculatePerformance(fourGSales.Sum(f => f.Total), employeeTargets.EmployeeFourGSaleTarget),
                RoxNewSalesPerformance = CalculatePerformance(roxNewSales.Sum(r => r.Total), employeeTargets.EmployeeRoxNewSaleTarget),
                RoxConversionSalesPerformance = CalculatePerformance(roxConversionSales.Sum(r => r.Total), employeeTargets.EmployeeRoxConversionSaleTarget),
                DailyPerformances = dailyPerformances
            };
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

        private Employee GetCurrentEmployee()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployee = _context.Employee.FirstOrDefault(e => e.AppUserId == userId);

            return currentEmployee;
        }

        [HttpPost]
        public async Task<IActionResult> BulkExport(int employeeNumber, DateTime startDate, DateTime endDate)
        {
            var performanceData = await GetBulkPerformanceData(employeeNumber, startDate, endDate);

            if (performanceData == null || !performanceData.Any())
            {
                return Json(new { success = false, message = "No performance data found for the selected criteria." });
            }

            using (var workbook = new XLWorkbook())
            {
                // Daily Performance Sheet
                var dailySheet = workbook.Worksheets.Add("Daily Sales");
                var currentRow = 1;

                // Daily Performance Header
                dailySheet.Cell(currentRow, 1).Value = "Date";
                dailySheet.Cell(currentRow, 2).Value = "Prepaid Sales";
                dailySheet.Cell(currentRow, 3).Value = "Postpaid Sales";
                dailySheet.Cell(currentRow, 4).Value = "Device Sales";
                dailySheet.Cell(currentRow, 5).Value = "M-Wallet Sales";
                dailySheet.Cell(currentRow, 6).Value = "4G Sales";
                dailySheet.Cell(currentRow, 7).Value = "Rox New Sales";
                dailySheet.Cell(currentRow, 8).Value = "Rox Conversion Sales";
                dailySheet.Cell(currentRow, 9).Value = "Total Sales";

                // Add Daily Performance Data
                foreach (var data in performanceData)
                {
                    foreach (var dailyPerformance in data.DailyPerformances)
                    {
                        currentRow++;
                        dailySheet.Cell(currentRow, 1).Value = dailyPerformance.Date.ToShortDateString();
                        dailySheet.Cell(currentRow, 2).Value = dailyPerformance.PrepaidSales;
                        dailySheet.Cell(currentRow, 3).Value = dailyPerformance.PostpaidSales;
                        dailySheet.Cell(currentRow, 4).Value = dailyPerformance.DeviceSales;
                        dailySheet.Cell(currentRow, 5).Value = dailyPerformance.MWalletSales;
                        dailySheet.Cell(currentRow, 6).Value = dailyPerformance.FourGSales;
                        dailySheet.Cell(currentRow, 7).Value = dailyPerformance.RoxNewSales;
                        dailySheet.Cell(currentRow, 8).Value = dailyPerformance.RoxConversionSales;
                        dailySheet.Cell(currentRow, 9).Value = dailyPerformance.TotalSales;
                    }
                }

                // Monthly Targets and Performance Sheet
                var monthlySheet = workbook.Worksheets.Add("Monthly Targets and Performance");
                currentRow = 1;

                // Monthly Targets and Performance Header
                monthlySheet.Cell(currentRow, 1).Value = "Month-Year";
                monthlySheet.Cell(currentRow, 2).Value = "Prepaid Sales Target";
                monthlySheet.Cell(currentRow, 3).Value = "Postpaid Sales Target";
                monthlySheet.Cell(currentRow, 4).Value = "Device Sales Target";
                monthlySheet.Cell(currentRow, 5).Value = "M-Wallet Sales Target";
                monthlySheet.Cell(currentRow, 6).Value = "4G Sales Target";
                monthlySheet.Cell(currentRow, 7).Value = "Rox New Sales Target";
                monthlySheet.Cell(currentRow, 8).Value = "Rox Conversion Sales Target";
                monthlySheet.Cell(currentRow, 9).Value = "Total Sales Target";
                monthlySheet.Cell(currentRow, 10).Value = "Prepaid Sales Performance (%)";
                monthlySheet.Cell(currentRow, 11).Value = "Postpaid Sales Performance (%)";
                monthlySheet.Cell(currentRow, 12).Value = "Device Sales Performance (%)";
                monthlySheet.Cell(currentRow, 13).Value = "M-Wallet Sales Performance (%)";
                monthlySheet.Cell(currentRow, 14).Value = "4G Sales Performance (%)";
                monthlySheet.Cell(currentRow, 15).Value = "Rox New Sales Performance (%)";
                monthlySheet.Cell(currentRow, 16).Value = "Rox Conversion Sales Performance (%)";


                // Aggregated data for each month in the date range
                var monthlyData = GetMonthlyPerformanceData(performanceData, startDate, endDate);

                // Add Monthly Targets and Performance Data
                foreach (var data in monthlyData)
                {
                    currentRow++;
                    monthlySheet.Cell(currentRow, 1).Value = data.MonthYear;
                    monthlySheet.Cell(currentRow, 2).Value = data.PrepaidSalesTarget;
                    monthlySheet.Cell(currentRow, 3).Value = data.PostpaidSalesTarget;
                    monthlySheet.Cell(currentRow, 4).Value = data.DeviceSalesTarget;
                    monthlySheet.Cell(currentRow, 5).Value = data.MWalletSalesTarget;
                    monthlySheet.Cell(currentRow, 6).Value = data.FourGSalesTarget;
                    monthlySheet.Cell(currentRow, 7).Value = data.RoxNewSalesTarget;
                    monthlySheet.Cell(currentRow, 8).Value = data.RoxConversionSalesTarget;
                    monthlySheet.Cell(currentRow, 9).Value = data.TotalSalesTarget;
                    monthlySheet.Cell(currentRow, 10).Value = data.PrepaidSalesPerformance;
                    monthlySheet.Cell(currentRow, 11).Value = data.PostpaidSalesPerformance;
                    monthlySheet.Cell(currentRow, 12).Value = data.DeviceSalesPerformance;
                    monthlySheet.Cell(currentRow, 13).Value = data.MWalletSalesPerformance;
                    monthlySheet.Cell(currentRow, 14).Value = data.FourGSalesPerformance;
                    monthlySheet.Cell(currentRow, 15).Value = data.RoxNewSalesPerformance;
                    monthlySheet.Cell(currentRow, 16).Value = data.RoxConversionSalesPerformance;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var base64 = Convert.ToBase64String(content);

                    return Json(new { success = true, fileContent = base64, fileName = $"PerformanceData_{employeeNumber}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.xlsx" });
                }
            }
        }

        private List<MonthlyPerformanceViewModel> GetMonthlyPerformanceData(List<EmployeePerformanceViewModel> performanceData, DateTime startDate, DateTime endDate)
        {
            var monthlyData = new List<MonthlyPerformanceViewModel>();
            var groupedByMonth = performanceData.GroupBy(p => new { p.Month, p.Year });

            foreach (var group in groupedByMonth)
            {
                var monthYear = $"{group.Key.Month}-{group.Key.Year}";

                var firstEntry = group.First(); // Use the first entry to get the targets for the month

                var monthlyPerformance = new MonthlyPerformanceViewModel
                {
                    MonthYear = monthYear,
                    PrepaidSalesTarget = firstEntry.PrepaidSalesTarget,
                    PostpaidSalesTarget = firstEntry.PostpaidSalesTarget,
                    DeviceSalesTarget = firstEntry.DeviceSalesTarget,
                    MWalletSalesTarget = firstEntry.MWalletSalesTarget,
                    FourGSalesTarget = firstEntry.FourGSalesTarget,
                    RoxNewSalesTarget = firstEntry.RoxNewSalesTarget,
                    RoxConversionSalesTarget = firstEntry.RoxConversionSalesTarget,
                   

                    // Calculate the performance for the month based on total sales and single target
                    PrepaidSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalPrepaidSales), firstEntry.PrepaidSalesTarget),
                    PostpaidSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalPostpaidSales), firstEntry.PostpaidSalesTarget),
                    DeviceSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalDeviceSales), firstEntry.DeviceSalesTarget),
                    MWalletSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalMWalletSales), firstEntry.MWalletSalesTarget),
                    FourGSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalFourGSales), firstEntry.FourGSalesTarget),
                    RoxNewSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalRoxNewSales), firstEntry.RoxNewSalesTarget),
                    RoxConversionSalesPerformance = CalculatePerformance(group.Sum(p => p.TotalRoxConversionSales), firstEntry.RoxConversionSalesTarget),
                  
                };
                monthlyData.Add(monthlyPerformance);
            }

            return monthlyData;
        }

        private double CalculatePerformance(int sales, int target)
        {
            return target == 0 ? 0 : Math.Round(((double)sales / target) * 100, 1);
        }


        private async Task<List<EmployeePerformanceViewModel>> GetBulkPerformanceData(int employeeNumber, DateTime startDate, DateTime endDate)
        {
            var dailySales = await _context.EmployeeSales
                .Where(a => a.EmployeeNumber == employeeNumber && a.SalesDate >= startDate && a.SalesDate <= endDate)
                .ToListAsync();

            var employeeTargets = await _context.EmployeeTargets
                .Where(t => t.EmployeeNumber == employeeNumber && t.Month >= startDate.Month && t.Month <= endDate.Month && t.Year >= startDate.Year && t.Year <= endDate.Year)
                .ToListAsync();

            if (employeeTargets == null || dailySales == null)
            {
                return null;
            }

            var salesIds = dailySales.Select(d => new
            {
                d.EmployeePrepaidSaleId,
                d.EmployeePostpaidSaleId,
                d.EmployeeDeviceSaleId,
                d.EmployeeMWalletSaleId,
                d.EmployeeFourGSaleId,
                d.EmployeeRoxNewSaleId,
                d.EmployeeRoxConversionSaleId
            }).ToList();

            var prepaidSales = await _context.EmployeePrepaidSales
                .Where(p => salesIds.Select(s => s.EmployeePrepaidSaleId).Contains(p.Id))
                .ToListAsync();

            var postpaidSales = await _context.EmployeePostpaidSales
                .Where(p => salesIds.Select(s => s.EmployeePostpaidSaleId).Contains(p.Id))
                .ToListAsync();

            var deviceSales = await _context.EmployeeDeviceSales
                .Where(d => salesIds.Select(s => s.EmployeeDeviceSaleId).Contains(d.Id))
                .ToListAsync();

            var mWalletSales = await _context.EmployeeMWalletSales
                .Where(m => salesIds.Select(s => s.EmployeeMWalletSaleId).Contains(m.Id))
                .ToListAsync();

            var fourGSales = await _context.EmployeeFourGSales
                .Where(f => salesIds.Select(s => s.EmployeeFourGSaleId).Contains(f.Id))
                .ToListAsync();

            var roxNewSales = await _context.EmployeeRoxNewSales
                .Where(r => salesIds.Select(s => s.EmployeeRoxNewSaleId).Contains(r.Id))
                .ToListAsync();

            var roxConversionSales = await _context.EmployeeRoxConversionSales
                .Where(r => salesIds.Select(s => s.EmployeeRoxConversionSaleId).Contains(r.Id))
                .ToListAsync();

            var dailyPerformances = dailySales.GroupBy(d => d.SalesDate.Date)
                .Select(g => new DailyPerformanceViewModel
                {
                    Date = g.Key,
                    PrepaidSales = prepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    PostpaidSales = postpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    DeviceSales = deviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total),
                    MWalletSales = mWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total),
                    FourGSales = fourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total),
                    RoxNewSales = roxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total),
                    RoxConversionSales = roxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    TotalSales = prepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 postpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 deviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total) +
                                 mWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total) +
                                 fourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total) +
                                 roxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total) +
                                 roxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    PrepaidSalesPerformance = CalculatePerformance(
                        prepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeePrepaidSaleTarget ?? 0),
                    PostpaidSalesPerformance = CalculatePerformance(
                        postpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeePostpaidSaleTarget ?? 0),
                    DeviceSalesPerformance = CalculatePerformance(
                        deviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeeDeviceSaleTarget ?? 0),
                    MWalletSalesPerformance = CalculatePerformance(
                        mWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeeMWalletSaleTarget ?? 0),
                    FourGSalesPerformance = CalculatePerformance(
                        fourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeeFourGSaleTarget ?? 0),
                    RoxNewSalesPerformance = CalculatePerformance(
                        roxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeeRoxNewSaleTarget ?? 0),
                    RoxConversionSalesPerformance = CalculatePerformance(
                        roxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                        employeeTargets.FirstOrDefault(t => t.Month == g.Key.Month && t.Year == g.Key.Year)?.EmployeeRoxConversionSaleTarget ?? 0)
                }).ToList();

            // Create the performance model list
            var performanceModels = dailyPerformances.Select(dp => new EmployeePerformanceViewModel
            {
                Month = dp.Date.Month,
                Year = dp.Date.Year,
                PrepaidSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeePrepaidSaleTarget ?? 0,
                PostpaidSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeePostpaidSaleTarget ?? 0,
                DeviceSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeeDeviceSaleTarget ?? 0,
                MWalletSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeeMWalletSaleTarget ?? 0,
                FourGSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeeFourGSaleTarget ?? 0,
                RoxNewSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeeRoxNewSaleTarget ?? 0,
                RoxConversionSalesTarget = employeeTargets.FirstOrDefault(t => t.Month == dp.Date.Month && t.Year == dp.Date.Year)?.EmployeeRoxConversionSaleTarget ?? 0,
                TotalPrepaidSales = dp.PrepaidSales,
                TotalPostpaidSales = dp.PostpaidSales,
                TotalDeviceSales = dp.DeviceSales,
                TotalMWalletSales = dp.MWalletSales,
                TotalFourGSales = dp.FourGSales,
                TotalRoxNewSales = dp.RoxNewSales,
                TotalRoxConversionSales = dp.RoxConversionSales,
                PrepaidSalesPerformance = dp.PrepaidSalesPerformance,
                PostpaidSalesPerformance = dp.PostpaidSalesPerformance,
                DeviceSalesPerformance = dp.DeviceSalesPerformance,
                MWalletSalesPerformance = dp.MWalletSalesPerformance,
                FourGSalesPerformance = dp.FourGSalesPerformance,
                RoxNewSalesPerformance = dp.RoxNewSalesPerformance,
                RoxConversionSalesPerformance = dp.RoxConversionSalesPerformance,
                DailyPerformances = new List<DailyPerformanceViewModel> { dp }
            }).ToList();

            return performanceModels;
        }



    }
}
