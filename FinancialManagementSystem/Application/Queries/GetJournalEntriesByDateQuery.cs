using System;
namespace Application.Queries
{
 public   class GetJournalEntriesByDateQuery
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
