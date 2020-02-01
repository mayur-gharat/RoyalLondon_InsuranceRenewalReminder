using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRenewalReminder;

namespace InsuranceRenewalReminderDemo.Tests
{
    [TestClass]
    public class UnitTestRenewalReminder
    {
        [TestMethod]
        public void TestReadTemplateFile()
        {
            TemplateReader TemplateReading = TemplateReader.GetInstance();
            Assert.IsNotNull(TemplateReading);
        }
    }
}
