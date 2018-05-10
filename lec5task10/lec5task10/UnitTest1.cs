using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec5task10
{
    [TestClass]
    public class UnitTest
    {
        static IWebDriver driver;
        static IWebDriver f_driver;

        public string[] color_value(IWebElement el, string xp_)
        {
            string orig_price_norm_c = el.FindElement(By.XPath(xp_)).GetCssValue("color");
            List<string> colors = new List<string>();
            foreach (Match m in Regex.Matches(orig_price_norm_c, @"\d{1,3}"))
            {
                colors.Add(m.Value);
            }
            return colors.ToArray();
        }

        public void checkRedColor(string[] colors)
        {
            Assert.IsTrue((Int32.Parse(colors[0]) > Int32.Parse(colors[1])) && (Int32.Parse(colors[0]) > Int32.Parse(colors[2])), "Price is not red!");
        }

        public  void checkGrayColor(string[] colors)
        {
            Assert.IsTrue(colors[0].Equals(colors[1]) && colors[1].Equals(colors[2]), "Price is not gray!");
        }

        public void checkBold(int bold_value)
        {
            Assert.IsTrue(bold_value >= 700, "Price is not bold");
        }

        public void checkCross(string atribute)
        {
            Assert.IsTrue(atribute.Equals("line-through"), "Price is not crossed");
        }

        public void compareFontSizes(IWebElement element)
        {
            string new_pr_s_fs = element.FindElement(By.XPath(".//*[@class='campaign-price']")).GetCssValue("font-size");
            new_pr_s_fs = new_pr_s_fs.Substring(0, 2);
            string new_pr_n_fs = element.FindElement(By.XPath(".//*[@class='regular-price']")).GetCssValue("font-size");
            new_pr_n_fs = new_pr_n_fs.Substring(0, 2);
            Assert.IsFalse(Int32.Parse(new_pr_n_fs) >= Int32.Parse(new_pr_s_fs), "Wrong font size of proces!");
        }
        public void test_prices(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            IWebElement duck = driver.FindElement(By.XPath("//div[@id='box-campaigns']//li"));

            string orig_name = duck.FindElement(By.XPath(".//div[@class='name']")).Text;
            string orig_price_norm = duck.FindElement(By.XPath(".//*[@class='regular-price']")).Text;
            string orig_price_sale = duck.FindElement(By.XPath(".//*[@class='campaign-price']")).Text;

            string[] orig_pr_n_c = color_value(duck, ".//*[@class='regular-price']");
            string[] orig_pr_s_c = color_value(duck, ".//*[@class='campaign-price']");
            int orig_pr_s_bl = Int32.Parse(duck.FindElement(By.XPath(".//*[@class='campaign-price']")).GetCssValue("font-weight"));
            string orig_pr_n_dec = duck.FindElement(By.XPath(".//*[@class='regular-price']")).GetCssValue("text-decoration-line");

            checkGrayColor(orig_pr_n_c);  
            checkRedColor(orig_pr_s_c);
            checkBold(orig_pr_s_bl);
            checkCross(orig_pr_n_dec);
            compareFontSizes(duck);

            // На новой странице
            duck.FindElement(By.XPath("./a")).Click();
            IWebElement new_duck = driver.FindElement(By.XPath("//div[@id='box-product']"));

            string new_name = new_duck.FindElement(By.XPath(".//h1[@class='title']")).Text;
            string new_price_norm = new_duck.FindElement(By.XPath(".//*[@class='regular-price']")).Text;
            string new_price_sale = new_duck.FindElement(By.XPath(".//*[@class='campaign-price']")).Text;

            string[] new_pr_n_c = color_value(new_duck, ".//*[@class='regular-price']");
            string[] new_pr_s_c = color_value(new_duck, ".//*[@class='campaign-price']");
            int new_pr_s_bl = Int32.Parse(new_duck.FindElement(By.XPath(".//*[@class='campaign-price']")).GetCssValue("font-weight"));
            string new_pr_n_dec = new_duck.FindElement(By.XPath(".//*[@class='regular-price']")).GetCssValue("text-decoration-line");

            Assert.IsTrue(new_name.Equals(orig_name), "Names are different");
            Assert.IsTrue(new_price_norm.Equals(orig_price_norm), "Prices are different");
            Assert.IsTrue(new_price_sale.Equals(orig_price_sale), "Sale prices are different");

            checkGrayColor(new_pr_n_c);            
            checkRedColor(new_pr_s_c);
            checkBold(new_pr_s_bl);
            checkCross(new_pr_n_dec);
            compareFontSizes(new_duck);

            driver.Quit();
        }

        [TestMethod]
        public void Test()
        {
            IWebDriver driver = new ChromeDriver();
            test_prices(driver);
            
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;
            options.BrowserExecutableLocation = @"C:\Program Files\Firefox Nightly\firefox.exe";
            IWebDriver f_driver = new FirefoxDriver(options);
            test_prices(f_driver);
        }

        [TestCleanup]
        public void end()
        {
        }
    }
}