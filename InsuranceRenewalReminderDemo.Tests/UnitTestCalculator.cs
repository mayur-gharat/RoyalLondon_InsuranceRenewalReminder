using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRenewalCalculator;


namespace InsuranceRenewalReminderDemo.Tests
{
    /// <summary>
    /// Basic test written to test the calculator class
    /// Can enhance tests so as to meet the business requirements as and when needed.
    /// </summary>
    [TestClass]
    public class UnitTestCalculator
    {
        [TestMethod]
        public void TestCalculateInitialMonthlyPremium()
        {
            Assert.AreEqual(Calculator.CalculateInitialMonthlyPayment(50.00), 4.43);
        }

        [TestMethod]
        public void TestOtherMonthlyPayment()
        {
            Assert.AreEqual(Calculator.CalculateOtherMonthlyPayment(50.00), 4.37);
        }

        [TestMethod]
        public void TestCalculateCreditCharge()
        {
            Assert.AreEqual(Calculator.CalculateCreditCharge(50.00), 2.5);
        }

        [TestMethod]
        public void TestTotalAnnualPremium()
        {
            Assert.AreEqual(Calculator.CalculateTotalPremium(50.00), 52.50);
        }
    }
}
