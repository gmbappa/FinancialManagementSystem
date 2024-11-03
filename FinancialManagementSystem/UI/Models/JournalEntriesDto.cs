using System;

namespace UI.Models
{
    public class JournalEntriesDto
    {
        public int JournalEntryId { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public int LineId { get; set; }
        public string AccountName { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}