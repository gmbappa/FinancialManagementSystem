using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteChartOfAccountHandler
    {
        private readonly IChartOfAccountRepository _repository;

        public DeleteChartOfAccountHandler(IChartOfAccountRepository repository)
        {
            _repository = repository;
        }

        public void Handle(int id)
        {
            var chartOfAccount = _repository.GetById(id);
            if (chartOfAccount == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            _repository.Delete(id);
        }
    }


}
