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
    public class GetActiveChartOfAccountsHandler
    {
        private readonly IChartOfAccountRepository _repository;

        public GetActiveChartOfAccountsHandler(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ChartOfAccount> Handle(GetActiveChartOfAccountsQuery query)
        {
            return _repository.GetActiveAccounts();
        }
    }
}
