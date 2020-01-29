using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRenewalReminderDemo;

namespace InsuranceRenewalReminderDemo.Tests
{
    [TestClass]
    public class UnitTestRenewalReminder
    {
        [TestMethod]
        public void TestReadTemplateFile()
        {
            Assert.IsNotNull(InsuranceRenewalReminder.UIHelper.ReadTemplateFile());
        }
    }
}
