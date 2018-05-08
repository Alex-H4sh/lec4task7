using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec10task17
{
    [TestClass]
    public class UnitTest1
    {
        static IWebDriver driver;
        private void login(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        [TestMethod]
        public void Test()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            driver = new ChromeDriver(options);
            login(driver);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            IList<IWebElement> goods = driver.FindElements(By.XPath("//table[@class='dataTable']//img/../a"));
            List<string> links = new List<string>();
            foreach (IWebElement el in goods)
            {
                links.Add(el.GetAttribute("href"));
            }

            // Для каждой ссылки смотрим наличие логов
            foreach (string href in links)
            { 
                driver.Navigate().GoToUrl(href);
                ILogs logs = driver.Manage().Logs;
                foreach (string type in logs.AvailableLogTypes)
                {
                    var browserLogs = logs.GetLog(type);
                    if (browserLogs.Count > 0)
                    {
                        Assert.IsTrue(browserLogs.Count > 0, "New logs");
                        foreach (var log in browserLogs)
                        {
                            System.Console.WriteLine(log.ToString());
                        }

                    }
                }
            }
            driver.Quit();                           
        }
    }
}
