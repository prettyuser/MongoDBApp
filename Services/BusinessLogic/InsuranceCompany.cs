using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace Services.BusinessLogic
{
    public class InsuranceCompany 
    {
        public string Name => "IF...";

        public IList<Risk> AvailableRisks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public void AddRisk(string nameOfInsuredObject, Risk risk, DateTime validFrom)
        //{
        //    throw new NotImplementedException();
        //}

        //public IPolicy GetPolicy(string nameOfInsuredObject, DateTime effectiveDate)
        //{
        //    throw new NotImplementedException();
        //}

        //public void RemoveRisk(string nameOfInsuredObject, Risk risk, DateTime validTill)
        //{
        //    throw new NotImplementedException();
        //}

        //public IPolicy SellPolicy(string nameOfInsuredObject, DateTime validFrom, short validMonths, IList<Risk> selectedRisks)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
