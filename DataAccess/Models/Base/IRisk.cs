using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models.Base
{
    public interface IRisk
    {
        string Id { get; set; }
        /// <summary>
        /// Unique name of the risk
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Risk yearly price
        /// </summary>
        decimal YearlyPrice { get; set; }

        DateTime RiskFrom { get; set; }

        DateTime RiskTill { get; set; }

    }
}
