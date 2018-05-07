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

        private void test_order(List<string> new_order)
        {
            List<string> old_order = new List<string>(new_order);
            new_order.Sort();
            Assert.IsTrue(old_order.SequenceEqual(new_order), "Wrong order!");
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
            List<string> main_countries = new List<string>();
            List<string> multi_tz_links = new List<string>();
            foreach (IWebElement el in rows)
            {
                IWebElement td = el.FindElement(By.XPath("./td[5]//a"));
                main_countries.Add(td.Text);
                string s = el.FindElement(By.XPath("./td[6]")).Text;
                if (s != "0")
                {
                    multi_tz_links.Add(td.GetAttribute("href"));
                }
            }
            test_order(main_countries);

            // Step 1b
            foreach (string href in multi_tz_links)
            {
                driver.Navigate().GoToUrl(href);
                List<string> sub_countries = new List<string>();
                IList<IWebElement> in_rows = driver.FindElements(By.XPath("//table[@class='dataTable']//td[3]//input[@type='hidden']"));
                foreach (IWebElement ch in in_rows)
                {
                    sub_countries.Add(ch.Text);
                }
                test_order(sub_countries);
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
                List<string> geo_zones = new List<string>();
                IList<IWebElement> in_rows = driver.FindElements(By.XPath("//select[contains(@name,'zone_code')]//option[@selected='selected']"));
                foreach (IWebElement ch in in_rows)
                {
                    geo_zones.Add(ch.Text);
                    System.Console.WriteLine(ch.Text);
                }
                test_order(geo_zones);
            }
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
