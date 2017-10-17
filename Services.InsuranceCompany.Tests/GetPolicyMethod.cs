using AutoMapper;
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
    public class GetPolicyMethod
    {
        Mock<IPolicyRepository> policyRepositoryMock;
        Mock<IRiskRepository> riskRepositoryMock;
        InsuranceCompany insuranceCompany;
        string nameOfInsuredObject;
        Risk risk;
        DateTime effectiveTime;


        [TestInitialize]
        public void TestInit()
        {
            policyRepositoryMock = new Mock<IPolicyRepository>();
            riskRepositoryMock = new Mock<IRiskRepository>();

            insuranceCompany = new InsuranceCompany(policyRepositoryMock.Object, riskRepositoryMock.Object);
            effectiveTime = DateTime.Now.AddMonths(6);
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

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Policy, Policy>();
            });

        }

        [TestMethod]
        public void GetState_CheckCallGet__ShouldBeCalled()
        {
            //Arrange

            //Act
            insuranceCompany.GetPolicy(nameOfInsuredObject, effectiveTime);

            //Assert
            policyRepositoryMock.Verify(x => x.Get(It.IsAny<string>()));
        }

        [TestMethod]
        public void GetState_CheckRiskFlag__ShouldBeCalled()
        {
            //Arrange

            //Act
            Policy policyState = (Policy)insuranceCompany.GetPolicy(nameOfInsuredObject, effectiveTime);

            //Assert
            Assert.AreEqual(true, policyState.AttachedRisks[risk.Name].IsActive);
        }
    }
}
