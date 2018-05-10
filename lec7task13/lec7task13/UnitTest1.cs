using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test13
{
    [TestClass]
    public class UnitTest
    {
        static IWebDriver driver;

        [TestMethod]
        public void Test()
        {
            driver = new ChromeDriver();
            
            for (int i=0;i<3;i++)
            {
                driver.Navigate().GoToUrl("https://localhost/litecart/");
                driver.FindElement(By.XPath("//div[@id='box-most-popular']//ul/li[1]")).Click();
                By by_el = By.XPath("//span[@class='quantity']");
                IWebElement el = driver.FindElement(by_el);
                int old_value = Int32.Parse(el.Text);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                if (driver.FindElements(By.XPath("//select[@name='options[Size]']")).Count>0)
                {
                    driver.FindElement(By.XPath("//select[@name='options[Size]']/option[2]")).Click();
                }
                driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
                wait.Until(d => d.FindElement(by_el).Text == ((old_value + 1).ToString()));
            }

            driver.FindElement(By.XPath("//a[contains(text(),'Checkout')]")).Click();

            for (int i = 0; i < 3; i++)
            {
                if (driver.FindElements(By.XPath("//div[@id='checkout-cart-wrapper']//ul[@class='shortcuts']/li")).Count>0)
                {
                    driver.FindElement(By.XPath("//div[@id='checkout-cart-wrapper']//ul[@class='shortcuts']/li[1]")).Click();
                }
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                By el_by = By.XPath("//table[@class='dataTable rounded-corners']//tr[2]");
                string el = driver.FindElement(el_by).Text;
                driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();
                if (i!=2)
                    wait.Until(d => d.FindElement(el_by).Text != el);
            }
            WebDriverWait waits = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waits.Until(d => driver.FindElement(By.XPath("//em")).Displayed);
            Assert.IsTrue(driver.FindElements(By.XPath("//em[contains(text(),'There are no items in your cart.')]")).Count>0,"Error");
        }

        [TestCleanup]
        public void end()
        {
            //driver.Quit();
        }
    }
}