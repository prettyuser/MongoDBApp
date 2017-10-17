using DataAccess.Models;
using DataAccess.Repository.Base;
using DataAccess.Repository.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tests
{
    [TestClass]
    public class SellPolicyMethod
    {
        private IRiskRepository _riskRepo = null;
        Mock<IPolicyRepository> policyRepositoryMock;
        Mock<IRiskRepository> riskRepositoryMock;
        InsuranceCompany insuranceCompany;

        string nameOfInsuredObject;
        DateTime validFrom;
        short validMonths;
        List<Risk> selectedRisks;

        [TestInitialize]
        public void TestInit()
        {
            _riskRepo = new StubRiskDataObject();

            nameOfInsuredObject = "SampleInsuredObject";
            validFrom = DateTime.Now.AddMonths(1);
            validMonths = 6;

            selectedRisks = new List<Risk>();
            var _stubListRisks = _riskRepo.Get().Result.Where(x => x.YearlyPrice > 600);
            foreach (var item in _stubListRisks)
            {
                selectedRisks.Add(item);
            }

            policyRepositoryMock = new Mock<IPolicyRepository>();
            riskRepositoryMock = new Mock<IRiskRepository>();

            insuranceCompany = new InsuranceCompany(policyRepositoryMock.Object, riskRepositoryMock.Object);
        }

        [TestMethod]
        public void Create_CheckCallAdd_ShouldBeCalled()
        {
            //Arrange
            policyRepositoryMock
               .Setup(x => x.Add(It.IsAny<Policy>()));

            //Act
            var policy = insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);

            //Assert
            policyRepositoryMock.Verify(x => x.Add(It.IsAny<Policy>()));
        }

        [TestMethod]
        public void Create_CheckPropertiesObject_FromInitial()
        {
            //Arrange

            //Act
            var policy = insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);

            //Assert
            Assert.AreEqual(nameOfInsuredObject, policy.NameOfInsuredObject);
            Assert.AreEqual(validFrom, policy.ValidFrom);
            Assert.AreEqual(validFrom.AddMonths(validMonths), policy.ValidTill);
            CollectionAssert.AreEquivalent(selectedRisks, policy.InsuredRisks.ToList());
        }

        [TestMethod]
        public void Create_CheckPremium()
        {
            //Arrange
            double expected = 3450;
            double delta = 1;

            //Act
            var policy = insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);

            //Assert
            Assert.AreEqual(expected, (double)policy.Premium, delta);
        }

        [TestMethod]
        public void Create_CheckAttachedDictionary()
        {
            //Arrange

            //Act
            Policy policy = (Policy)insuranceCompany.SellPolicy(nameOfInsuredObject, validFrom, validMonths, selectedRisks);

            //Assert
            Assert.IsNotNull(policy.AttachedRisks);
            CollectionAssert.AllItemsAreNotNull(policy.AttachedRisks);
            Assert.AreEqual(selectedRisks.Count, policy.AttachedRisks.Count);
        }
    }
}
