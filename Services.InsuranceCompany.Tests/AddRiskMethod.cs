using DataAccess.Models;
using DataAccess.Repository.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass]
    public class AddRiskMethod
    {
        Mock<IPolicyRepository> policyRepositoryMock;
        Mock<IRiskRepository> riskRepositoryMock;
        InsuranceCompany insuranceCompany;

        [TestInitialize]
        public void TestInit()
        {        
            policyRepositoryMock = new Mock<IPolicyRepository>();
            riskRepositoryMock = new Mock<IRiskRepository>();

            //policyRepositoryMock
            //    .Setup(z => z.Add(It.IsAny<Policy>()))
            //    .Callback<>

            insuranceCompany = new InsuranceCompany(policyRepositoryMock.Object, riskRepositoryMock.Object);
        }

        [TestMethod]
        public void Add_CheckCallAdd__ShouldBeCalled()
        {
            //Arrange
            var nameOfInsuredObject = "Example";
            var risk = new Risk { Name = "Risk ABC", YearlyPrice = 20000};
            var validFrom = DateTime.Now.AddYears(1);
            policyRepositoryMock
                .Setup(x => x.Get(It.IsAny<string>()))
                .Returns(Task.FromResult(new Policy {
                    NameOfInsuredObject = "Example A",
                    Premium = 100,
                    ValidFrom = validFrom,
                    ValidTill = validFrom.AddMonths(6),
                    InsuredRisks = new List<Risk> { new Risk {Name = "dasdas", YearlyPrice = 200} },
                    AttachedRisks = new Dictionary<string, ActiveState> { }
                }));
                

            //Act
            insuranceCompany.AddRisk(nameOfInsuredObject, risk, validFrom);

            //Assert
            policyRepositoryMock.Verify(x => x.Get());
        }


    }
}
