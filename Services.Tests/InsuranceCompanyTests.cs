//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Services.BusinessLogic;
//using DataAccess.Repository.Base;
//using System.Diagnostics;
//using System.Collections.Generic;
//using DataAccess.Models;
//using DataAccess.Repository;
//using DataAccess.DbModels;
//using System;

//namespace Services.Tests
//{
//    [TestClass]
//    public class InsuranceCompanyTests
//    {
//        private InsuranceCompany _insCompany;
//        private IPolicyRepository _policyRep;
//        private IRiskRepository _riskRep;

//        static List<Risk> _listOfRisks;

//        public TestContext _TestContext { get; set; }

//        [TestInitialize]
//        public void TestInitialize()
//        {
//            Console.WriteLine("ads");
//            Trace.WriteLine("Bububu");
//            _insCompany = new InsuranceCompany(_policyRep, _riskRep);   
//        }

//        [ClassInitialize]
//        public static void InitializeListOfRisks(TestContext testContext)
//        {
//            _listOfRisks = new List<Risk> {
//                new Risk {Name="Risk A", YearlyPrice = 100},
//                new Risk {Name="Risk B", YearlyPrice = 200},
//                new Risk {Name="Risk C", YearlyPrice = 250},
//                new Risk {Name="Risk D", YearlyPrice = 300},
//                new Risk {Name="Risk E", YearlyPrice = 350},
//                new Risk {Name="Risk F", YearlyPrice = 400},
//                new Risk {Name="Risk G", YearlyPrice = 450},
//                new Risk {Name="Risk H", YearlyPrice = 500},
//                new Risk {Name="Risk I", YearlyPrice = 550}
//            };

//        }

//        [TestMethod]
//        public void AreEqual_AvailableRisks()
//        {
//            _insCompany.AvailableRisks = _listOfRisks;
//            CollectionAssert.AreEqual(_listOfRisks, (List<Risk>)_insCompany.AvailableRisks);
//        }

//        //[TestMethod]
//        //public void UniqueItems_AvailableRisks_ListRisksreturn()
//        //{
//        //    _insCompany.AvailableRisks = _listOfRisks;
//        //    CollectionAssert.AllItemsAreUnique((List<Risk>) _insCompany.AvailableRisks, "Uniqueness failed!");
//        //}

//        //[TestMethod]
//        //public void ItemsNotNull_AvailableRisks_ListRisksreturn()
//        //{
//        //    _insCompany.AvailableRisks = _listOfRisks;
//        //    CollectionAssert.AllItemsAreNotNull((List<Risk>) _insCompany.AvailableRisks, "Not null failed!");
//        //}

//        [TestMethod]
//        public void Get_NameCompany_IFreturn()
//        {
//            //Arrange
//            string expected = "IF";
            
//            //Act
//            string actual = _insCompany.Name;

//            //Assert
//            Assert.AreEqual(expected, actual, $"Name {actual} should have been {expected}");
//        }

//        [TestMethod]
//        public void Edit_ChangeCompanyName_ChangedNamereturn()
//        {
//            //Arrange
//            string expected = "IFplus";
            
//            //Act
//            _insCompany.ChangeCompanyName(expected);
//            string actual = _insCompany.Name;

//            //Assert
//            Assert.AreEqual(expected, actual, $"Name {actual} should have been {expected}");
//        }
//    }
//}
