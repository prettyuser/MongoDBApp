using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.BusinessLogic;
using DataAccess.Repository.Base;
using Moq;
using DataAccess.Repository.Stubs;
using System.Linq;
using DataAccess.Models;

namespace Services.Tests
{
    [TestClass]
    public class InsuranceCompanyTDD
    {
        private IRiskRepository _riskRepo = null;

        [TestInitialize]
        public void TestInit()
        {
            _riskRepo = new StubRiskDataObject();
        }

        [TestMethod]
        public void ItemsNotNull_AvailableRisks()
        {
            var someList = _riskRepo.Get().Result.ToList();

            CollectionAssert.AllItemsAreNotNull(someList);
        }

        [TestMethod]
        public void ItemsAreUnique_AvailableRisks()
        {
            var someList = _riskRepo.Get().Result.ToList();

            CollectionAssert.AllItemsAreUnique(someList);
        }

        [TestMethod]
        public void TypeOfRisk_AvailableRisks()
        {
            var someList = _riskRepo.Get().Result.ToList();

            CollectionAssert.AllItemsAreInstancesOfType(someList, typeof(Risk));
        }



    }
}
