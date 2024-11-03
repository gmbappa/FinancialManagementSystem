using FluentValidation;

namespace UI.Models
{
    public class ChartOfAccountValidator : AbstractValidator<AccountDto>
    {
        public ChartOfAccountValidator()
        {
            this.RuleFor(r => r.AccountName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} value should not be empty");

            this.RuleFor(r => r.AccountType.ToString())
                .Cascade(CascadeMode.Stop)              
                .NotEmpty().WithMessage("{PropertyName} value should not be empty");           
        }
    }

    
}