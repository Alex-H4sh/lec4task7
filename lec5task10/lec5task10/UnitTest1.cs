using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec5task10
{
    [TestClass]
    public class UnitTest
    {
        static IWebDriver driver;

        public string[] color_value(IWebElement el, string xp_)
        {
            string orig_price_norm_c = el.FindElement(By.XPath(xp_)).GetCssValue("color");
            string[] delimiterChars = { "rgba(", ")", ", " };
            string[] colors = orig_price_norm_c.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
            return colors;
        }

        public string[] color_value(IWebDriver el, string xp_)
        {
            string orig_price_norm_c = el.FindElement(By.XPath(xp_)).GetCssValue("color");
            string[] delimiterChars = { "rgba(", ")", ", " };
            string[] colors = orig_price_norm_c.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
            return colors;
        }

        [TestMethod]
        public void Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            IWebElement duck = driver.FindElement(By.XPath("//div[@id='box-campaigns']//li"));
            
            // a) на главной странице и на странице товара совпадает текст названия товара
            string orig_name = duck.FindElement(By.XPath(".//div[@class='name']")).Text;
            // б) на главной странице и на странице товара совпадают цены(обычная и акционная)
            string orig_price_norm = duck.FindElement(By.XPath(".//*[@class='regular-price']")).Text;
            string orig_price_sale = duck.FindElement(By.XPath(".//*[@class='campaign-price']")).Text;
          
            // Проверка серого цвета цены
            string[] orig_pr_n_c = color_value(duck,".//*[@class='regular-price']");
            Assert.IsFalse(!((orig_pr_n_c[0].Equals(orig_pr_n_c[1]))&&(orig_pr_n_c[1].Equals(orig_pr_n_c[2]))),"Orig price is not gray!");

            // Проверка красного цвета акцонной цены
            string[] orig_pr_s_c = color_value(duck, ".//*[@class='campaign-price']");
            Assert.IsFalse(!(!(orig_pr_s_c[0].Equals("0")) && (orig_pr_s_c[1].Equals(orig_pr_s_c[2]))&& (orig_pr_s_c[1].Equals("0"))), "Sale price is not red!");

            // Проверка жирности шрифта акционной цены
            int orig_pr_s_bl = Int32.Parse(duck.FindElement(By.XPath(".//*[@class='campaign-price']")).GetCssValue("font-weight"));
            Assert.IsFalse(orig_pr_s_bl<700, "Sale price is not bold");

            // Проверка перечеркнутости цены
            string orig_pr_n_dec = duck.FindElement(By.XPath(".//*[@class='regular-price']")).GetCssValue("text-decoration-line");
            Assert.IsFalse(!(orig_pr_n_dec.Equals("line-through")), "Price is not crossed");

            // Шрифт акционной цены должен быть больше
            string orig_pr_s_fs = duck.FindElement(By.XPath(".//*[@class='campaign-price']")).GetCssValue("font-size");
            orig_pr_s_fs= orig_pr_s_fs.Substring(0,2);
            string orig_pr_n_fs = duck.FindElement(By.XPath(".//*[@class='regular-price']")).GetCssValue("font-size");
            orig_pr_n_fs = orig_pr_n_fs.Substring(0,2);
            Assert.IsFalse(Int32.Parse(orig_pr_n_fs)>=Int32.Parse(orig_pr_s_fs),"Wrong font size of proces!");

            // На новой странице
            duck.FindElement(By.XPath("./a")).Click();
            string new_name = driver.FindElement(By.XPath("//h1[@class='title']")).Text;

            string new_price_norm = driver.FindElement(By.XPath("//*[@class='regular-price']")).Text;
            string new_price_sale = driver.FindElement(By.XPath("//*[@class='campaign-price']")).Text;

            Assert.IsFalse(!new_name.Equals(orig_name),"Names are different");

            Assert.IsFalse(!new_price_norm.Equals(orig_price_norm), "Prices are different");
            Assert.IsFalse(!new_price_sale.Equals(orig_price_sale), "Sale prices are different");

            string[] new_pr_n_c = color_value(driver, "//*[@class='regular-price']");
            Assert.IsFalse(!((new_pr_n_c[0].Equals(new_pr_n_c[1])) && (new_pr_n_c[1].Equals(new_pr_n_c[2]))), "Orig price is not gray!");

            // Проверка красного цвета акцонной цены
            string[] new_pr_s_c = color_value(driver, "//*[@class='campaign-price']");
            Assert.IsFalse(!(!(new_pr_s_c[0].Equals("0")) && (new_pr_s_c[1].Equals(new_pr_s_c[2])) && (new_pr_s_c[1].Equals("0"))), "Sale price is not red!");

            // Проверка жирности шрифта акционной цены
            int new_pr_s_bl = Int32.Parse(driver.FindElement(By.XPath("//*[@class='campaign-price']")).GetCssValue("font-weight"));
            Assert.IsFalse(new_pr_s_bl < 700, "Sale price is not bold");

            // Проверка перечеркнутости цены
            string new_pr_n_dec = driver.FindElement(By.XPath("//*[@class='regular-price']")).GetCssValue("text-decoration-line");
            Assert.IsFalse(!(new_pr_n_dec.Equals("line-through")), "Price is not crossed");

            // Шрифт акционной цены должен быть больше
            string new_pr_s_fs = driver.FindElement(By.XPath("//*[@class='campaign-price']")).GetCssValue("font-size");
            new_pr_s_fs = new_pr_s_fs.Substring(0, 2);
            string new_pr_n_fs = driver.FindElement(By.XPath("//*[@class='regular-price']")).GetCssValue("font-size");
            new_pr_n_fs = new_pr_n_fs.Substring(0, 2);
            Assert.IsFalse(Int32.Parse(new_pr_n_fs) >= Int32.Parse(new_pr_s_fs), "Wrong font size of proces!");

        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}