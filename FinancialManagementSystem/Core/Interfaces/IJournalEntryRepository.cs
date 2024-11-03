using Core.Entities;
using System;
using System.Collections.Generic;


namespace Core.Interfaces
{
    public interface IJournalEntryRepository : IRepository<JournalEntry>
    {  
        void GetActiveJournalAccounts(out IEnumerable<JournalEntriesWithLines> dataCollection);
        IEnumerable<JournalEntry> GetByDateRange(DateTime? startDate, DateTime? endDate);
    }
}
