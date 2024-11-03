using Application.Commands;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;

public class CreateJournalEntryHandler
{
    private readonly IJournalEntryRepository _repository;

    public CreateJournalEntryHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public void Handle(CreateJournalEntryCommand command)
    {      
        var journalEntry = new JournalEntry
        {
            EntryDate = command.EntryDate,
            Description = command.Description,
            Reference = command.Reference,
            Lines = command.Lines
        };      
        
        _repository.Add(journalEntry);
    }
}
