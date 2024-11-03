using Core.Entities;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IChartOfAccountRepository : IRepository<ChartOfAccount>
    {
        IEnumerable<ChartOfAccount> GetActiveAccounts();

        IEnumerable<ChartOfAccount> GetReports(DateTime? startDate, DateTime? endDate, string accountType = null);
    }
}
