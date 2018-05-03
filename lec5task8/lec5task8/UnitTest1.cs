using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
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

            // Step 1-2
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            IList<IWebElement> rows = driver.FindElements(By.XPath("//table[@class='dataTable']//tr[@class='row']"));
            List<string> x = new List<string>();
            foreach (IWebElement el in rows)
            {
                IWebElement td = el.FindElement(By.XPath("./td[5]//a"));
                x.Add(td.Text);
            }
            List<string> y = new List<string>(x);
            x.Sort();

            if (y.SequenceEqual(x))
            {
                System.Console.WriteLine("Right order");
            }

            // Step 3
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
