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
    public class GetJournalEntriesByDateHandler
    {
        private readonly IJournalEntryRepository _repository;

        public GetJournalEntriesByDateHandler(IJournalEntryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<JournalEntry> Handle(GetJournalEntriesByDateQuery query)
        {
            return _repository.GetByDateRange(query.StartDate, query.EndDate);
        }
    }
}
