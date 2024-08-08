using Pk.Com.Jazz.ECP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pk.Com.Jazz.ECP.ViewModels
{
    public class ECPerformanceViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int PrepaidSalesTarget { get; set; }
        public int PostpaidSalesTarget { get; set; }
        public int DeviceSalesTarget { get; set; }
        public int MWalletSalesTarget { get; set; }
        public int FourGSalesTarget { get; set; }
        public int RoxNewSalesTarget { get; set; }
        public int RoxConversionSalesTarget { get; set; }
        public int TotalPrepaidSales { get; set; }
        public int TotalPostpaidSales { get; set; }
        public int TotalDeviceSales { get; set; }
        public int TotalMWalletSales { get; set; }
        public int TotalFourGSales { get; set; }
        public int TotalRoxNewSales { get; set; }
        public int TotalRoxConversionSales { get; set; }
        public double PrepaidSalesPerformance { get; set; }
        public double PostpaidSalesPerformance { get; set; }
        public double DeviceSalesPerformance { get; set; }
        public double MWalletSalesPerformance { get; set; }
        public double FourGSalesPerformance { get; set; }
        public double RoxNewSalesPerformance { get; set; }
        public double RoxConversionSalesPerformance { get; set; }
        public List<DailyPerformanceViewModel> DailyPerformances { get; set; }

        public ECPerformanceViewModel()
        {
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;
        }

      
    }

    

    public class ECDailyPerformanceViewModel
    {
        public DateTime Date { get; set; }
        public int PrepaidSales { get; set; }
        public int PostpaidSales { get; set; }
        public int DeviceSales { get; set; }
        public int MWalletSales { get; set; }
        public int FourGSales { get; set; }
        public int RoxNewSales { get; set; }
        public int RoxConversionSales { get; set; }
        public int TotalSales { get; set; }
        public double Performance { get; set; }
    }



}
