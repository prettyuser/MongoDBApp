using DataAccess.Models;
using DataAccess.Repository.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class AddRiskMethod
    {
        Mock<IPolicyRepository> policyRepositoryMock;
        Mock<IRiskRepository> riskRepositoryMock;
        InsuranceCompany insuranceCompany;
        string nameOfInsuredObject;
        Risk risk;
        DateTime validFrom;

        [TestInitialize]
        public void TestInit()
        {        
            policyRepositoryMock = new Mock<IPolicyRepository>();
            riskRepositoryMock = new Mock<IRiskRepository>();

            insuranceCompany = new InsuranceCompany(policyRepositoryMock.Object, riskRepositoryMock.Object);

            nameOfInsuredObject = "Example";
            risk = new Risk { Name = "Risk ABC", YearlyPrice = 20000 };
            validFrom = DateTime.Now.AddYears(1);
            policyRepositoryMock
                .Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Policy
                {
                    NameOfInsuredObject = "Example",
                    Premium = 100,
                    ValidFrom = validFrom,
                    ValidTill = validFrom.AddMonths(6),
                    InsuredRisks = new List<Risk>(),
                    AttachedRisks = new Dictionary<string, ActiveState>()
                }));
        }

        [TestMethod]
        public void Add_CheckCallGetUpdate__ShouldBeCalled()
        {
            //Arrange

            //Act
            insuranceCompany.AddRisk(nameOfInsuredObject, risk, validFrom);

            //Assert
            policyRepositoryMock.Verify(x => x.Get(It.IsAny<string>()));
            policyRepositoryMock.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Policy>()));
        }

        [TestMethod]
        public void Add_CheckProperties()
        {
            //Arrange

            //Act
            insuranceCompany.AddRisk(nameOfInsuredObject, risk, validFrom);

            //Assert
            policyRepositoryMock.Verify(x => x.Get(
                It.Is<string>(val => val.Equals(nameOfInsuredObject))
                ));
            CollectionAssert.Contains(policyRepositoryMock.Object.Get("Example").Result.InsuredRisks.ToList(), risk);
            Assert.IsTrue(policyRepositoryMock.Object.Get("Example").Result.AttachedRisks.Keys.Contains(risk.Name));
        }

        [TestMethod]
        public void Add_CheckPremium()
        {
            //Arrange
            double expectedPremium = 9972;
            double delta = 1;

            //Act
            insuranceCompany.AddRisk(nameOfInsuredObject, risk, validFrom);

            //Assert
            Assert.AreEqual(expectedPremium, (double) policyRepositoryMock.Object.Get("Example").Result.Premium, delta);
        }

    }
}
