using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ActiveState
    {
        public DateTime RiskFrom { get; set; }

        public DateTime RiskTill { get; set; }

        public bool IsActive { get; set; }

        public decimal YearlyPrice { get; set; }
    }
}
