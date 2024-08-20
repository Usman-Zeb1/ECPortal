using ClosedXML.Excel;
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
    public class SalesController : Controller
    {
        private readonly ECContext _context;
        public SalesController(ECContext context)
        {

            _context = context;
        }

        public IActionResult Index(int? employeeNumber = null)
        {
            if (User.IsInRole("Agent"))
            {
                employeeNumber = GetEmployeeNumber();

                if (employeeNumber == null)
                {
                    return NotFound("Employee not found.");
                }

                // Fetch performance data for the agent
                var salesModel = GetEmployeeSalesModel(employeeNumber.Value);
                if (salesModel == null)
                {
                    return View();
                }
                return View(salesModel);
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
                    var salesModel = GetEmployeeSalesModel(employeeNumber.Value);
                    if (salesModel != null)
                    {
                        ViewBag.EmployeeName = agents.FirstOrDefault(a => a.EmployeeNumber == employeeNumber.Value)?.EmployeeName;
                        return View("AgentSales", salesModel);
                    }
                }

                return View("AgentSales");
            }
        }
        private EmployeeSalesViewModel GetEmployeeSalesModel(int employeeNumber)
        {
            var today = DateTime.Now.Date;

            var dailySales = _context.EmployeeSales
                .Where(a => a.EmployeeNumber == employeeNumber && a.SalesDate.Date == today)
                .ToList();

            if (dailySales == null || !dailySales.Any())
            {
                return null;
            }

            // Fetch related data from each sales table
            var prepaidSales = _context.EmployeePrepaidSales
                .Where(p => dailySales.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id))
                .ToList();

            var postpaidSales = _context.EmployeePostpaidSales
                .Where(p => dailySales.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id))
                .ToList();

            var deviceSales = _context.EmployeeDeviceSales
                .Where(d => dailySales.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id))
                .ToList();

            var mWalletSales = _context.EmployeeMWalletSales
                .Where(m => dailySales.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id))
                .ToList();

            var fourGSales = _context.EmployeeFourGSales
                .Where(f => dailySales.Select(d => d.EmployeeFourGSaleId).Contains(f.Id))
                .ToList();

            var roxNewSales = _context.EmployeeRoxNewSales
                .Where(r => dailySales.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id))
                .ToList();

            var roxConversionSales = _context.EmployeeRoxConversionSales
                .Where(r => dailySales.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id))
                .ToList();

            // Create the performance model
            return new EmployeeSalesViewModel
            {
                PrepaidSale = prepaidSales,
                PostpaidSale = postpaidSales,
                DeviceSale = deviceSales,
                MWalletSale = mWalletSales,
                FourGSale = fourGSales,
                RoxNewSale = roxNewSales,
                RoxConversionSale = roxConversionSales
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


        [HttpPost]
        public async Task<IActionResult> BulkExport(int employeeNumber, DateTime startDate, DateTime endDate)
        {
            var salesData = await GetBulkSalesData(employeeNumber, startDate, endDate);

            if (salesData == null || !salesData.Any())
            {
                return Json(new { success = false, message = "No sales data found for the selected criteria." });
            }

            using (var workbook = new XLWorkbook())
            {
                // Sales Data Sheet
                var salesSheet = workbook.Worksheets.Add("Sales Data");
                var currentRow = 1;

                // Sales Data Header
                salesSheet.Cell(currentRow, 1).Value = "Date";
                salesSheet.Cell(currentRow, 2).Value = "Prepaid Sales";
                salesSheet.Cell(currentRow, 3).Value = "Postpaid Sales";
                salesSheet.Cell(currentRow, 4).Value = "Device Sales";
                salesSheet.Cell(currentRow, 5).Value = "M-Wallet Sales";
                salesSheet.Cell(currentRow, 6).Value = "4G Sales";
                salesSheet.Cell(currentRow, 7).Value = "Rox New Sales";
                salesSheet.Cell(currentRow, 8).Value = "Rox Conversion Sales";
                salesSheet.Cell(currentRow, 9).Value = "Total Sales";

                // Add Sales Data
                foreach (var data in salesData)
                {
                    currentRow++;
                    salesSheet.Cell(currentRow, 1).Value = data.Date.ToShortDateString();
                    salesSheet.Cell(currentRow, 2).Value = data.PrepaidSales;
                    salesSheet.Cell(currentRow, 3).Value = data.PostpaidSales;
                    salesSheet.Cell(currentRow, 4).Value = data.DeviceSales;
                    salesSheet.Cell(currentRow, 5).Value = data.MWalletSales;
                    salesSheet.Cell(currentRow, 6).Value = data.FourGSales;
                    salesSheet.Cell(currentRow, 7).Value = data.RoxNewSales;
                    salesSheet.Cell(currentRow, 8).Value = data.RoxConversionSales;
                    salesSheet.Cell(currentRow, 9).Value = data.TotalSales;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var base64 = Convert.ToBase64String(content);

                    return Json(new { success = true, fileContent = base64, fileName = $"SalesData_{employeeNumber}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.xlsx" });
                }
            }
        }

        private async Task<List<SalesDataViewModel>> GetBulkSalesData(int employeeNumber, DateTime startDate, DateTime endDate)
        {
            var dailySales = await _context.EmployeeSales
                .Where(a => a.EmployeeNumber == employeeNumber && a.SalesDate >= startDate && a.SalesDate <= endDate)
                .ToListAsync();

            var salesData = dailySales.GroupBy(d => d.SalesDate.Date)
                .Select(g => new SalesDataViewModel
                {
                    Date = g.Key,
                    PrepaidSales = _context.EmployeePrepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    PostpaidSales = _context.EmployeePostpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total),
                    DeviceSales = _context.EmployeeDeviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total),
                    MWalletSales = _context.EmployeeMWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total),
                    FourGSales = _context.EmployeeFourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total),
                    RoxNewSales = _context.EmployeeRoxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total),
                    RoxConversionSales = _context.EmployeeRoxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total),
                    TotalSales = _context.EmployeePrepaidSales.Where(p => g.Select(d => d.EmployeePrepaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 _context.EmployeePostpaidSales.Where(p => g.Select(d => d.EmployeePostpaidSaleId).Contains(p.Id)).Sum(p => p.Total) +
                                 _context.EmployeeDeviceSales.Where(d => g.Select(ds => ds.EmployeeDeviceSaleId).Contains(d.Id)).Sum(d => d.Total) +
                                 _context.EmployeeMWalletSales.Where(m => g.Select(d => d.EmployeeMWalletSaleId).Contains(m.Id)).Sum(m => m.Total) +
                                 _context.EmployeeFourGSales.Where(f => g.Select(d => d.EmployeeFourGSaleId).Contains(f.Id)).Sum(f => f.Total) +
                                 _context.EmployeeRoxNewSales.Where(r => g.Select(d => d.EmployeeRoxNewSaleId).Contains(r.Id)).Sum(r => r.Total) +
                                 _context.EmployeeRoxConversionSales.Where(r => g.Select(d => d.EmployeeRoxConversionSaleId).Contains(r.Id)).Sum(r => r.Total)
                }).ToList();

            return salesData;
        }

      


    }
}
