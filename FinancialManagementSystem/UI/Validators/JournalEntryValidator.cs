using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{

    public class JournalEntryValidator : AbstractValidator<JournalEntryDto>
    {
        public JournalEntryValidator()
        {           
            RuleFor(r => r.EntryDate)
                .NotEmpty().WithMessage("{PropertyName} must be provided.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} cannot be in the future.");
           
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("{PropertyName} value should not be empty."); 
           
            RuleFor(r => r.Lines)
                .NotEmpty().WithMessage("{PropertyName} should contain at least one line.")
                .Must(lines => lines.All(line => line != null)).WithMessage("All lines must be valid.");

            // Validate each line in the Lines collection
            RuleForEach(r => r.Lines).SetValidator(new JournalEntryLineValidator());
        }
    }

    public class JournalEntryLineValidator : AbstractValidator<JournalEntryLine>
    {
        public JournalEntryLineValidator()
        {
            RuleFor(line => line.AccountName)
                .NotEmpty().WithMessage("{PropertyName} value should not be empty.");               
          
            RuleFor(line => line.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
            
            RuleFor(line => line.IsDebit)
                .NotNull().WithMessage("{PropertyName} must be specified.");
        }
    }
}