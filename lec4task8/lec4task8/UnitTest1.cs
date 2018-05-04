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
                bool check = false;
                /*
                int count_stickers = 0;
                try
                {
                    count_stickers = el.FindElements(By.XPath(".//div[contains(@class,'sticker')]")).Count;
                }
                catch (NoSuchElementException)
                {
                    count_stickers = 0;
                }

                if (count_stickers==1)
                    check = true;
                */

                if (el.FindElements(By.XPath(".//div[contains(@class,'sticker')]")).Count == 1)
                    check = true;
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
