using System;
using System.Reflection.Metadata;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (IWebDriver driver = new ChromeDriver())
            {
                Uri url = new Uri("https://auditsystem.azurewebsites.net/");
                driver.Navigate().GoToUrl(url);
                driver.FindElements(By.Name("checklist"));
                
            }
        }
    }
}
