using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }

        public string Reference { get; set; } 

        public virtual ICollection<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }

    public class JournalEntryLine
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebit { get; set; }

        public virtual JournalEntry JournalEntry { get; set; }
    }
}
