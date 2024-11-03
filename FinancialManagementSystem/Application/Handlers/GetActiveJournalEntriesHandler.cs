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
    public class GetActiveJournalEntriesHandler
    {
        private readonly IJournalEntryRepository _repository;

        public GetActiveJournalEntriesHandler(IJournalEntryRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<JournalEntriesWithLines> Handle(GetActiveJournalEntriesQuery query)
        {
            // Define the output collection
            IEnumerable<JournalEntriesWithLines> dataCollection;

            // Call the repository method
            _repository.GetActiveJournalAccounts(out dataCollection);

            // Return the populated collection
            return dataCollection;
        }

    }
}
