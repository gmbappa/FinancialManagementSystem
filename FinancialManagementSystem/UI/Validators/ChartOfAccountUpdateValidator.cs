using Core.Entities;
using FluentValidation;

namespace UI.Models
{
    public class ChartOfAccountUpdateValidator : AbstractValidator<ChartOfAccount>
    {
        public ChartOfAccountUpdateValidator()
        {
            this.RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} value should be greater than 0.");

            this.RuleFor(r => r.AccountName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} value should not be empty");

            this.RuleFor(r => r.AccountType.ToString())
                .Cascade(CascadeMode.Stop)              
                .NotEmpty().WithMessage("{PropertyName} value should not be empty");           
        }
    }

    
}