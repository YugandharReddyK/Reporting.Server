namespace Sperry.MxS.Core.Common.Models.CustomerReport
{
    public static class CustomerReportDataExtension
    {
        public static void UpdateFrom(this CustomerReportData customerReport, CustomerReportData report)
        {
            customerReport.WellId = report.WellId;
            customerReport.ReportData = report.ReportData;
        }
    }
}
