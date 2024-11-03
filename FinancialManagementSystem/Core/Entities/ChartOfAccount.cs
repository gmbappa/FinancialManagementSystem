using System;

namespace Core.Entities
{
    public class ChartOfAccount
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
