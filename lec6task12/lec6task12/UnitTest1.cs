using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test11
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

        [TestMethod]
        public void Test()
        {
            driver = new ChromeDriver();
            login(driver);
            driver.FindElement(By.XPath("//span[contains(text(),'Catalog')]")).Click();

            // If there are other "Notebook"s - delete them
            IList<IWebElement> goods = driver.FindElements(By.XPath("//table[@class='dataTable']//td[3]//a[contains(text(),'Notebook')]"));
            if (goods.Count>0)
            {
                foreach (IWebElement el in goods)
                {
                    System.Console.WriteLine(el.Text);
                    el.FindElement(By.XPath("./../../td[1]/input")).Click();
                }
                driver.FindElement(By.XPath("//span//button[@name='delete']")).Click();
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            
            driver.FindElement(By.XPath("//a[contains(text(),'Add New Product')]")).Click();

            // Fill General tab
            driver.FindElement(By.XPath("//input[@type='radio'and@value='1']")).Click();
            driver.FindElement(By.XPath("//input[@name='name[en]']")).SendKeys("Notebook");
            driver.FindElement(By.XPath("//input[@name='code']")).SendKeys("5455");
            driver.FindElement(By.XPath("//input[@type='checkbox' and @value='1-3']")).Click();
            driver.FindElement(By.XPath("//input[@name='quantity']")).SendKeys("10");
            string abs = Path.GetFullPath("image.jpg");
            driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(abs);
            driver.FindElement(By.XPath("//input[@name='date_valid_from']")).SendKeys("05/07/2018");
            driver.FindElement(By.XPath("//input[@name='date_valid_to']")).SendKeys("05/07/2020");

            // Fill Informmation tab
            driver.FindElement(By.XPath("//a[contains(text(),'Information')]")).Click();
            SelectElement manuf_select = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturer_id']")));
            manuf_select.SelectByValue("1");
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys("notebook");
            driver.FindElement(By.XPath("//input[@name='short_description[en]']")).SendKeys("Notebook Acer");
            driver.FindElement(By.XPath("//div[@class='trumbowyg-editor']")).SendKeys("From everyday computing to a tough\n" 
                +"professional workload, experience a new level of design and performance options.");
            driver.FindElement(By.XPath("//input[@name='head_title[en]']")).SendKeys("Notebook Acer");
            driver.FindElement(By.XPath("//input[@name='meta_description[en]']")).SendKeys("notebook");

            // Fill Prices tab
            driver.FindElement(By.XPath("//a[contains(text(),'Prices')]")).Click();
            SelectElement price_select = new SelectElement(driver.FindElement(By.XPath("//select[@name='purchase_price_currency_code']")));
            price_select.SelectByText("US Dollars");
            driver.FindElement(By.XPath("//input[@name='purchase_price']")).Clear();
            driver.FindElement(By.XPath("//input[@name='purchase_price']")).SendKeys("1000");

            driver.FindElement(By.XPath("//input[@name='prices[USD]']")).Clear();
            driver.FindElement(By.XPath("//input[@name='prices[USD]']")).SendKeys("1100");
            driver.FindElement(By.XPath("//input[@name='gross_prices[USD]']")).Clear();
            driver.FindElement(By.XPath("//input[@name='gross_prices[USD]']")).SendKeys("1200");
            driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).Clear();
            driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).SendKeys("900");
            driver.FindElement(By.XPath("//input[@name='gross_prices[EUR]']")).Clear();
            driver.FindElement(By.XPath("//input[@name='gross_prices[EUR]']")).SendKeys("970");

            // Save
            driver.FindElement(By.XPath("//button[@name='save']")).Click();
            // Check
            Assert.IsTrue(driver.FindElements(By.XPath("//table//a[contains(text(),'Notebook')]")).Count>0, "No item in catalog");
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}