using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetChartOfAccountByIdHandler
    {
        private readonly IChartOfAccountRepository _repository;

        public GetChartOfAccountByIdHandler(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public ChartOfAccount Handle(int id)
        {
            var chartOfAccount = _repository.GetById(id);
            if (chartOfAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            return chartOfAccount;
        }
    }

}
