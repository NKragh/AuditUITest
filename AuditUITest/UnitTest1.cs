using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AuditUITest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                Uri url = new Uri("https://auditsystem.azurewebsites.net/");
                driver.Navigate().GoToUrl(url);
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.Name("checklist"));
                Assert.IsNotNull(elements);
            }
        }
    }
}
