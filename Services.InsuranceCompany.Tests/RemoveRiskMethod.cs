using DataAccess.Models;
using DataAccess.Repository.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class RemoveRiskMethod
    {
        Mock<IPolicyRepository> policyRepositoryMock;
        Mock<IRiskRepository> riskRepositoryMock;
        InsuranceCompany insuranceCompany;
        string nameOfInsuredObject;
        Risk risk;
        DateTime validTill;

        [TestInitialize]
        public void TestInit()
        {
            policyRepositoryMock = new Mock<IPolicyRepository>();
            riskRepositoryMock = new Mock<IRiskRepository>();

            insuranceCompany = new InsuranceCompany(policyRepositoryMock.Object, riskRepositoryMock.Object);
            validTill = DateTime.Now.AddYears(1);
            nameOfInsuredObject = "Example";
            risk = new Risk { Name = "Risk ABC", YearlyPrice = 20000 };
            //var validFrom = DateTime.Now.AddYears(1);
            policyRepositoryMock
                .Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Policy
                {
                    NameOfInsuredObject = nameOfInsuredObject,
                    Premium = 100,
                    ValidFrom = DateTime.Now,
                    ValidTill = DateTime.Now.AddMonths(6),
                    InsuredRisks = new List<Risk> { new Risk { Name = "Risk ABC", YearlyPrice = 20000 } },
                    AttachedRisks = new Dictionary<string, ActiveState>() { { risk.Name,
                        new ActiveState
                        { YearlyPrice = risk.YearlyPrice,
                            RiskFrom = DateTime.Now,
                            RiskTill = DateTime.Now.AddMonths(6),
                            IsActive = true
                        } } }
                }));
                
        }

        [TestMethod]
        public void Remove_CheckCallGetUpdate__ShouldBeCalled()
        {
            //Arrange

            //Act
            insuranceCompany.RemoveRisk(nameOfInsuredObject, risk, validTill);

            //Assert
            policyRepositoryMock.Verify(x => x.Get(It.IsAny<string>()));
            policyRepositoryMock.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<Policy>()));
        }

        [TestMethod]
        public void Remove_CheckPremium()
        {
            //Arrange
            double expectedPremium = 19945;
            double delta = 1;

            //Act
            insuranceCompany.RemoveRisk(nameOfInsuredObject, risk, validTill);

            //Assert
            Assert.AreEqual(expectedPremium, (double) policyRepositoryMock.Object.Get("Example").Result.Premium, delta);
        }

        [TestMethod]
        public void Remove_CheckProperties()
        {
            //Arrange

            //Act
            insuranceCompany.RemoveRisk(nameOfInsuredObject, risk, validTill);

            //Assert
            Assert.AreEqual(validTill, policyRepositoryMock.Object.Get("Example").Result.AttachedRisks[risk.Name].RiskTill);
        }
    }
}
