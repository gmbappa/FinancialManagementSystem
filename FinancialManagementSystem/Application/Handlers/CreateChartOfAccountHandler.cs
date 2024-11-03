using Application.Commands;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateChartOfAccountHandler
    {
        private readonly IChartOfAccountRepository _repository;

        public CreateChartOfAccountHandler(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateChartOfAccountCommand command)
        {
            var chartOfAccount = new ChartOfAccount
            {
                AccountName = command.AccountName,
                AccountType = command.AccountType,
                IsActive = command.IsActive
            };

            _repository.Add(chartOfAccount);
        }
    }
}
