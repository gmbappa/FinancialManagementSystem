using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetJournalEntryByIdHandler
    {
        private readonly IJournalEntryRepository _repository;

        public GetJournalEntryByIdHandler(IJournalEntryRepository repository)
        {
            _repository = repository;
        }

        public JournalEntry Handle(int id)
        {
            var journalEntry = _repository.GetById(id);
            if (journalEntry == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            return journalEntry;
        }
    }
}
