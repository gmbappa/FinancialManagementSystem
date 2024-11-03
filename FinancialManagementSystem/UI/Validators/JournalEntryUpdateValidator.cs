using Core.Entities;
using FluentValidation;
using System;
using System.Linq;


namespace UI.Models
{

    public class JournalEntryUpdateValidator : AbstractValidator<JournalEntryDto>
    {
        public JournalEntryUpdateValidator()
        {
            RuleFor(r => r.Id)
               .Cascade(CascadeMode.Stop)
               .GreaterThan(0).WithMessage("{PropertyName} value should be greater than 0.");

            RuleFor(r => r.EntryDate)
                .NotEmpty().WithMessage("{PropertyName} must be provided.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} cannot be in the future.");
           
            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("{PropertyName} value should not be empty.");                   

            RuleFor(r => r.Lines)
                .NotEmpty().WithMessage("{PropertyName} should contain at least one line.")
                .Must(lines => lines.All(line => line != null)).WithMessage("All lines must be valid.");

            // Validate each line in the Lines collection
            RuleForEach(r => r.Lines).SetValidator(new JournalEntryLineUpdateValidator());
        }
    }

    public class JournalEntryLineUpdateValidator : AbstractValidator<JournalEntryLine>
    {
        public JournalEntryLineUpdateValidator()
        {
           
            RuleFor(line => line.AccountName)
                .NotEmpty().WithMessage("{PropertyName} value should not be empty.")
                .Length(1, 100).WithMessage("{PropertyName} length must be between {MinLength} and {MaxLength} characters.");
           
            RuleFor(line => line.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
                       
            RuleFor(line => line.IsDebit)
                .NotNull().WithMessage("{PropertyName} must be specified.");
        }
    }
}