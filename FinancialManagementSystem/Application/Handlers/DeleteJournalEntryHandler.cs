using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteJournalEntryHandler
    {
        private readonly IJournalEntryRepository _repository;

        public DeleteJournalEntryHandler(IJournalEntryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(int id)
        {
            var journalEntry = _repository.GetById(id);
            if (journalEntry == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            _repository.Delete(id);
        }
    }
}
