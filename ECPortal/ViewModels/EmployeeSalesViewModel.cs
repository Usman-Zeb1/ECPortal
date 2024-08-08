using Pk.Com.Jazz.ECP.Models;

public class EmployeeSalesViewModel
{
    public List<EmployeePrepaidSale> PrepaidSale { get; set; }
    public List<EmployeePostpaidSale> PostpaidSale { get; set; }
    public List<EmployeeDeviceSale> DeviceSale { get; set; }
    public List<EmployeeMWalletSale> MWalletSale { get; set; }
    public List<EmployeeFourGSale> FourGSale { get; set; }
    public List<EmployeeRoxNewSale> RoxNewSale { get; set; }
    public List<EmployeeRoxConversionSale> RoxConversionSale { get; set; }
}

public class SalesDataViewModel
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
}
