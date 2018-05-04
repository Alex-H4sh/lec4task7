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
        public void Test()
        {
            driver = new ChromeDriver();
            login(driver);

            // Step 1-1a
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            IList<IWebElement> rows = driver.FindElements(By.XPath("//table[@class='dataTable']//tr[@class='row']"));
            List<string> x = new List<string>();
            List<string> multi_tz_links = new List<string>();
            foreach (IWebElement el in rows)
            {
                IWebElement td = el.FindElement(By.XPath("./td[5]//a"));
                x.Add(td.Text);
                string s = el.FindElement(By.XPath("./td[6]")).Text;
                if (s != "0")
                {
                    multi_tz_links.Add(td.GetAttribute("href"));
                }
            }
            List<string> y = new List<string>(x);
            x.Sort();
            Assert.IsFalse(!(y.SequenceEqual(x)), "Wrong order!");

            // Step 1b
            foreach (string href in multi_tz_links)
            {
                driver.Navigate().GoToUrl(href);
                List<string> u = new List<string>();
                IList<IWebElement> in_rows = driver.FindElements(By.XPath("//table[@class='dataTable']//tr//td[3]//input"));
                foreach (IWebElement ch in in_rows)
                {
                   u.Add(ch.Text);
                }

                List<string> z = new List<string>(u);
                u.Sort();
                Assert.IsFalse(!(z.SequenceEqual(u)), "Wrong order!");
            }
            
            // Step 2
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            IList<IWebElement> countries = driver.FindElements(By.XPath("//table[@class='dataTable']//tr//td[3]/a"));
            List<string> link = new List<string>();

            foreach (IWebElement con in countries)
            {
                link.Add(con.GetAttribute("href"));
            }

            foreach (string href in link)
            {
                driver.Navigate().GoToUrl(href);
                List<string> u = new List<string>();
                IList<IWebElement> in_rows = driver.FindElements(By.XPath("//table[@id='table-zones']//tr//td[3]//select//option[@selected='selected']"));
                foreach (IWebElement ch in in_rows)
                {
                    u.Add(ch.Text);
                    System.Console.WriteLine(ch.Text);
                }

                List<string> z = new List<string>(u);
                u.Sort();
                Assert.IsFalse(!(z.SequenceEqual(u)), "Wrong order!");
            }
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
