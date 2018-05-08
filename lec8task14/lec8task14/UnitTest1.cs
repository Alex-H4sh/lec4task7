using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec8task4
{
    [TestClass]
    public class UnitTest
    {
        static IWebDriver driver;
        private void login(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        public static Func<IWebDriver, String> ThereIsWindowOtherThan(ICollection<String> oldWindows)
        {
            return driver =>
            {
                ICollection<String> handles = driver.WindowHandles;
                return (handles.Count > oldWindows.Count) ? handles.Last().ToString() : null;
            };
        }

        [TestMethod]
        public void Test()
        {
            driver = new ChromeDriver();
            login(driver);
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            driver.FindElement(By.XPath("//a[@class='button']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IList<IWebElement> links = driver.FindElements(By.XPath("//a/i[@class='fa fa-external-link']"));

            foreach (IWebElement exter_link in links)
            {
                string mainWindow = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;
                exter_link.Click();
                string newWindow = wait.Until(ThereIsWindowOtherThan(oldWindows));
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}