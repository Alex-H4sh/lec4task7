using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test3
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

        [TestInitialize]
        public void init()
        {
        }

        [TestMethod]
        public void lec2test3()
        {
            driver = new ChromeDriver();
            login(driver);
            IList<IWebElement> menu = driver.FindElements(By.CssSelector("a#span.name"));
            foreach (IWebElement el in menu)
            {
                el.Click();
                bool check = true;
                try
                {
                    driver.FindElement(By.TagName("h1"));
                }
                catch (NoSuchElementException)
                {
                    check = false;
                }
                Assert.IsFalse(!check," doesnt have h1");
                driver.Navigate().GoToUrl("https://localhost/litecart/admin");
            }
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
