using System.Collections.Generic;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
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

            int size = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a")).Count;

            for (int i=0;i<size; i++)
            {
                IList<IWebElement> menu2 = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li/a"));
                menu2[i].Click();

                int in_links=0;
                try
                {
                    in_links = driver.FindElements(By.XPath("//ul[@class='docs']/li/a")).Count;
                }
                catch (NoSuchElementException)
                {}

                if (in_links>0)
                {
                    for (int k=0;k<in_links;k++)
                    {
                        IList<IWebElement> a = driver.FindElements(By.XPath("//ul[@class='docs']/li/a"));
                        if (k <= (a.Count - 1))
                        {
                            a[k].Click();
                            bool check = true;
                            try
                            {
                                driver.FindElement(By.TagName("h1"));
                            }
                            catch (NoSuchElementException)
                            {
                                check = false;
                            }
                            Assert.IsFalse(!check, driver.Title + " doesnt have h1");
                        }
                    }
                }        
            }
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
