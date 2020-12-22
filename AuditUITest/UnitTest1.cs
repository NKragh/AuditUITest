using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Fiddler;
using Proxy = OpenQA.Selenium.Proxy;

namespace AuditUITest
{
    [TestClass]
    public class UnitTest1
    {
        //private string URL = "https://auditsystem.azurewebsites.net/";
        private string URL = "http://127.0.0.1:5500/";

        [TestMethod]
        public void HeadersPresent()
        {
            using (IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                Uri url = new Uri(URL);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);

                driver.FindElement(By.TagName("a")).Click();

                IWebElement element = wait.Until(e => e.FindElement(By.Name("checklist")));

                Assert.IsNotNull(element);
            }
        }

        [TestMethod]
        public void QuestionsLoaded()
        {
            using (IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                Uri url = new Uri(URL);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);

                driver.FindElement(By.TagName("a")).Click();

                IWebElement element = wait.Until(e => e.FindElement(By.Name("checklist")));

                element.Click();

                IWebElement container = wait.Until(e => e.FindElement(By.Id("checklistcontainer")));
                IWebElement question = container.FindElement(By.CssSelector(".row"));
                Assert.IsTrue(question.Displayed);
            }

        }

        [TestMethod]
        public void AnswersClicked()
        {
            using (IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                Uri url = new Uri(URL + "audit.html");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);

                IWebElement element = wait.Until(e => e.FindElement(By.Name("checklist")));

                element.Click();

                IWebElement container = wait.Until(e => e.FindElement(By.Id("checklistcontainer")));

                IWebElement question = container.FindElement(By.CssSelector(".row"));

                IWebElement radiobutton = question.FindElement(By.Name("main1"));

                Assert.IsTrue(radiobutton.Enabled);
                radiobutton.Click();
                Assert.IsTrue(radiobutton.Selected);

                //GetCssValue("value") == "OK"
            }
        }

        [TestMethod]
        public void SaveClicked()
        {
            using (IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                Uri url = new Uri(URL + "audit.html");
                driver.Navigate().GoToUrl(url);

                IWebElement element = wait.Until(e => e.FindElement(By.Name("checklist")));

                element.Click();

                IWebElement container = wait.Until(e => e.FindElement(By.Id("checklistcontainer")));

                IWebElement question = container.FindElement(By.CssSelector(".row"));

                IWebElement radiobutton = question.FindElement(By.Name("main1"));

                radiobutton.Click();

                IWebElement savebutton = driver.FindElement(By.Id("savebtn"));

                savebutton.Click();

                string response = wait.Until(e => e.FindElement(By.Id("response")).Text);

                Assert.AreEqual("Svar gemt succesfuldt.", response);
            }
        }

        [TestMethod]
        public void CompleteClicked()
        {
            using (IWebDriver driver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                Uri url = new Uri(URL + "audit.html");
                driver.Navigate().GoToUrl(url);

                IWebElement completeButton = driver.FindElement(By.Id("completebtn"));

                completeButton.Click();

                string response = wait.Until(e => e.FindElement(By.Id("response")).Text);

                Assert.AreEqual("Rapport afsluttet.", response);
            }
        }
    }
}
