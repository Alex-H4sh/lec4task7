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
            IList<IWebElement> ducks = driver.FindElements(By.XPath("//ul//a[@class='link']"));
            foreach (IWebElement el in ducks)
            {
                bool check = true; ;
                try
                {
                    check = el.FindElement(By.XPath("//div[contains(@class,'sticker')]")).Displayed;
                }
                catch (NoSuchElementException)
                {
                    check = false;
                }
                Assert.IsFalse(!check, el.Text + " no stickers");
            }
        }

        [TestCleanup]
        public void end_test()
        {
            driver.Quit();
        }
    }
}
