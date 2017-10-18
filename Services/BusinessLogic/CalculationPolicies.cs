using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BusinessLogic
{
    public static class CalculationPolicies
    {
        /// <summary>
        /// Calc method for overall policy's premium
        /// </summary>
        /// <param name="listrisk"></param>
        /// <returns>Total price for policy within policy's period</returns>
        public static decimal CalcPolicyPremium(Dictionary<string, ActiveState> listrisk)
        {
            if (listrisk.Count < 1 || listrisk == null)
            {
                throw new ArgumentNullException("listRisk is empty or NULL");
            }

            decimal total_price = 0;

            foreach (var item in listrisk)
            {
                    var days_in_risk = (int) (item.Value.RiskTill - item.Value.RiskFrom).TotalDays;
                    var daily_price = item.Value.YearlyPrice / 365;
                    var realprice = days_in_risk * daily_price;
                    total_price += realprice;
            }

            return total_price;
        }
    }
}
