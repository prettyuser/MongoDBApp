using DataAccess.Models;
using DataAccess.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BusinessLogic.Base
{
    public interface IInsuranceCompany
    {
        /// <summary>
        /// Name of Insurance company
        /// </summary>
        string Name { get; }
        /// <summary>
        /// List of the risks that can be insured. List can be updated at any time
        /// </summary>
        IList<Risk> AvailableRisks { get; set; }
        /// <summary>
        /// Sell the policy.
        /// </summary>
        /// <param name="nameOfInsuredObject">Name of the insured object. Must be unique in the given period.</param>
        /// <param name="validFrom">Date and time when policy starts. Can not be in the past</param>
        /// <param name="validMonthes">Policy period in months</param>
        /// <param name="selectedRisks">List of risks that must be included in the policy</param>
        /// <returns>Information about policy</returns>
        IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks);
        /// <summary>
        /// Add risk to the policy of insured object
        /// </summary>
        /// <param name="nameOfInsuredObject">Name of insured object</param>
        /// <param name="risk">Risk tham must be added</param>
        /// <param name="validFrom">Date when risk becomes active. Can not be in the past</param>
        void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom);
        /// <summary>
        /// Remove risk from the policy of insured object
        /// </summary>
        /// <param name="nameOfInsuredObject">Name of insured object</param>
        /// <param name="risk">Risk tham must be removed</param>
        /// <param name="validTill">Date when risk become inactive. Must be equal to or greater than date when risk become active</param>
        void RemoveRisk(string nameOfInsuredObject, Risk risk, DateTime validTill);
        /// <summary>
        /// Get policy state at the given moment of time.
        /// </summary>
        /// <param name="nameOfInsuredObject">Name of insured object</param>
        /// <param name="effectiveDate">Moment of date and time</param>
        /// <returns></returns>
        IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate);
    }
}
