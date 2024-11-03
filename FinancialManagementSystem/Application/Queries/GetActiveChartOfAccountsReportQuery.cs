using System;


namespace Application.Queries
{
    public class GetActiveChartOfAccountsReportQuery
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AccountType { get; set; } = null;

    }
}
