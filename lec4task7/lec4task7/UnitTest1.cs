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

            IList<IWebElement> menu = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a"));

            IList<string> links = new List<string>();
            for (int i=0;i<menu.Count;i++)
            {
                links.Add(menu[i].GetAttribute("href"));
            }

            IList<string> links2 = new List<string>();
            foreach (string a in links)
            {
                driver.Navigate().GoToUrl(a);
                bool check = true;
                try
                {
                    IList<IWebElement> in_links = driver.FindElements(By.XPath("//ul[@class='docs']/li/a"));
                    foreach (IWebElement in_a in in_links)
                    {
                        links2.Add(in_a.GetAttribute("href"));
                    }
                }
                catch (NoSuchElementException)
                {
                    check = false;
                }
            }

            ((List<string>)links).AddRange(links2);

            foreach (string a in links)
            {
                driver.Navigate().GoToUrl(a);
                bool check = true;
                try
                {
                    driver.FindElement(By.TagName("h1"));
                }
                catch (NoSuchElementException)
                {
                    check = false;
                }
                Assert.IsFalse(!check,driver.Title+" doesnt have h1");
            }

        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
