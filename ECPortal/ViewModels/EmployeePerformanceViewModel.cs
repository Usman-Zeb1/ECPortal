using Pk.Com.Jazz.ECP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pk.Com.Jazz.ECP.ViewModels
{
    public class EmployeePerformanceViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int PrepaidSalesTarget { get; set; } = 0;
        public int TotalPrepaidSales { get; set; } = 0;
        public double PrepaidSalesPerformance { get; set; } = 0;
        public int PostpaidSalesTarget { get; set; } = 0;
        public int TotalPostpaidSales { get; set; } = 0;
        public double PostpaidSalesPerformance { get; set; } = 0;
        public int DeviceSalesTarget { get; set; } = 0;
        public int TotalDeviceSales { get; set; } = 0;
        public double DeviceSalesPerformance { get; set; } = 0;
        public int MWalletSalesTarget { get; set; } = 0;
        public int TotalMWalletSales { get; set; } = 0;
        public double MWalletSalesPerformance { get; set; } = 0;
        public int FourGSalesTarget { get; set; } = 0;
        public int TotalFourGSales { get; set; } = 0;
        public double FourGSalesPerformance { get; set; } = 0;
        public int RoxNewSalesTarget { get; set; } = 0;
        public int TotalRoxNewSales { get; set; } = 0;
        public double RoxNewSalesPerformance { get; set; } = 0;
        public int RoxConversionSalesTarget { get; set; } = 0;
        public int TotalRoxConversionSales { get; set; } = 0;
        public double RoxConversionSalesPerformance { get; set; } = 0;

        public EmployeePerformanceViewModel()
        {
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
        }

      
    }


}
