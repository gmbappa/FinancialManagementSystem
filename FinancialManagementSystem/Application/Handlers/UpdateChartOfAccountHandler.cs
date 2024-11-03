using Application.Commands;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateChartOfAccountHandler
    {
        private readonly IChartOfAccountRepository _repository;

        public UpdateChartOfAccountHandler(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UpdateChartOfAccountCommand command)
        {
            var chartOfAccount = _repository.GetById(command.Id);
            if (chartOfAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            // Update the properties
            chartOfAccount.AccountName = command.AccountName;
            chartOfAccount.AccountType = command.AccountType;
            chartOfAccount.IsActive = command.IsActive;

            _repository.Update(chartOfAccount);
        }
    }

}
