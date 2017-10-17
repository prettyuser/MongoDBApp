//using DataAccess.Models;
//using DataAccess.Repository.Base;
//using DataAccess.Repository.Stubs;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Services.BusinessLogic;
//using System;
//using System.Collections.Generic;
//using System.Linq;


//namespace Services.Tests
//{
//    [TestClass]
//    public class InsuranceCompanyTDD
//    {
//        private IRiskRepository _riskRepo = null;

//        [TestInitialize]
//        public void TestInit()
//        {
//            _riskRepo = new StubRiskDataObject();
//        }

//        [TestMethod]
//        public void ItemsNotNull_AvailableRisks()
//        {
//            var someList = _riskRepo.Get().Result.ToList();

//            CollectionAssert.AllItemsAreNotNull(someList);
//        }

//        [TestMethod]
//        public void ItemsAreUnique_AvailableRisks()
//        {
//            var someList = _riskRepo.Get().Result.ToList();

//            CollectionAssert.AllItemsAreUnique(someList);
//        }

//        [TestMethod]
//        public void TypeOfRisk_AvailableRisks()
//        {
//            var someList = _riskRepo.Get().Result.ToList();

//            CollectionAssert.AllItemsAreInstancesOfType(someList, typeof(Risk));
//        }

//        [TestMethod]
//        public void CreatePolicy_PolicyCreated_PolicyShouldBeSaved()
//        {
//            //Arrange
//            string nameOfInsuredObject = "SampleInsuredObject";
//            DateTime validFrom = DateTime.Now.AddMonths(1);
//            short validMonths = 6;

//            List<Risk> selectedRisks = new List<Risk>();
//            var _stubListRisks = _riskRepo.Get().Result.Where(x => x.YearlyPrice > 600);
//            foreach(var item in _stubListRisks)
//            {
//                selectedRisks.Add(item);
//            }

//            Mock<IPolicyRepository> policyRepositoryMock = new Mock<IPolicyRepository>();
//            Mock<IRiskRepository> riskRepositoryMock = new Mock<IRiskRepository>();

                           

//            InsuranceCompany insuranceCompany = new InsuranceCompany(policyRepositoryMock.Object, riskRepositoryMock.Object);

//            policyRepositoryMock
//               .Setup(x => x.Add(It.IsAny<Policy>()));

//            //Act
//            var policy = insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);

//            //Assert
//            policyRepositoryMock.Verify(x => x.Add(It.IsAny<Policy>()));

//            Assert.AreEqual(nameOfInsuredObject, policy.NameOfInsuredObject);
//            Assert.AreEqual(validFrom, policy.ValidFrom);
//            Assert.AreEqual(validFrom.AddMonths(validMonths), policy.ValidTill);
//            CollectionAssert.AreEquivalent(selectedRisks, policy.InsuredRisks.ToList());
            
//        }


//    }
//}
