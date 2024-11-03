using Application.Queries;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetActiveChartOfAccountsReportHandler
    {
        private readonly IChartOfAccountRepository _repository;

        public GetActiveChartOfAccountsReportHandler(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ChartOfAccount> Handle(GetActiveChartOfAccountsReportQuery query)
        {
            return _repository.GetReports(query.StartDate,query.EndDate,query.AccountType);
        }
    }
}
