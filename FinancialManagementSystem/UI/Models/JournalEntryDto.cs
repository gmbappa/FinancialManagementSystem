using Core.Entities;
using System;
using System.Collections.Generic;

namespace UI.Models
{
    public class JournalEntryDto
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }

        // Change ICollection to List
        public virtual List<JournalEntryLine> Lines { get; set; } = new List<JournalEntryLine>();
    }
}