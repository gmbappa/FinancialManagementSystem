﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateChartOfAccountCommand
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
    }
}
