using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec4task8
{
    [TestClass]
    public class UnitTest1
    {
        static public IWebDriver driver;

        [TestInitialize]
        public void init()
        {
            driver = new ChromeDriver();
        }

        [TestMethod]
        public void Test()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            IList<IWebElement> ducks = driver.FindElements(By.XPath());
        }

        [TestCleanup]
        public void end_test()
        {
            driver.Quit();
        }
    }
}
