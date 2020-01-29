using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRenewalCalculator;


namespace InsuranceRenewalReminderDemo.Tests
{
    [TestClass]
    public class UnitTestCalculator
    {
        [TestMethod]
        public void TestCalculateInitialMonthlyPremium()
        {
            Assert.AreEqual(Helper.CalculateInitialMonthlyPayment(50.00), 4.43);
        }

        [TestMethod]
        public void TestOtherMonthlyPayment()
        {
            Assert.AreEqual(Helper.CalculateOtherMonthlyPayment(50.00), 4.37);
        }

        [TestMethod]
        public void TestCalculateCreditCharge()
        {
            Assert.AreEqual(Helper.CalculateCreditCharge(50.00), 2.5);
        }

        [TestMethod]
        public void TestTotalAnnualPremium()
        {
            Assert.AreEqual(Helper.CalculateTotalPremium(50.00), 52.50);
        }
    }
}
