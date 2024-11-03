using Application.Commands;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateJournalEntryHandler
    {
        private readonly IJournalEntryRepository _repository;

        public UpdateJournalEntryHandler(IJournalEntryRepository repository)
        {
            _repository = repository;
        }

        public void Handle(UpdateJournalEntryCommand command)
        {
            var JournalEntry = _repository.GetById(command.Id);
            if (JournalEntry == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            // Update the properties
            JournalEntry.EntryDate = command.EntryDate;
            JournalEntry.Description = command.Description;
            JournalEntry.Reference = command.Reference;

            JournalEntry.Lines.Clear();
            foreach (var line in command.Lines)
            {
                JournalEntry.Lines.Add(line);
            } 
            _repository.Update(JournalEntry);
        }
    }
}
