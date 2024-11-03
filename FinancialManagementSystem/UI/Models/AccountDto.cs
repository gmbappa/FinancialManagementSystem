
namespace UI.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsActive { get; set; }
    }

    public enum AccountType
    {
        Asset,
        Liability,
        Equity,
        Revenue,
        Expense
    }
}