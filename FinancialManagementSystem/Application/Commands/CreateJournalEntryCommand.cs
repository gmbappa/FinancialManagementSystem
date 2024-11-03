using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateJournalEntryCommand
    {
        public DateTime EntryDate { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; } 
        public List<JournalEntryLine> Lines { get; set; }
    }
}
