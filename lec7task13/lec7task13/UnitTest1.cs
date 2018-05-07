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
                IWebElement el = driver.FindElement(By.XPath("//span[@class='quantity']"));
                int old_value = Int32.Parse(el.Text);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                if (driver.FindElements(By.XPath("//select[@name='options[Size]']")).Count>0)
                {
                    driver.FindElement(By.XPath("//select[@name='options[Size]']/option[2]")).Click();
                }
                driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(el,(old_value+1).ToString()));
            }

            driver.FindElement(By.XPath("//a[contains(text(),'Checkout')]")).Click();

            for (int i = 0; i < 3; i++)
            {
                if (driver.FindElements(By.XPath("//div[@id='checkout-cart-wrapper']//ul[@class='shortcuts']/li")).Count>0)
                {
                    driver.FindElement(By.XPath("//div[@id='checkout-cart-wrapper']//ul[@class='shortcuts']/li[1]")).Click();
                }
                driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement el = driver.FindElement(By.XPath("//table[@class='dataTable rounded-corners']//tr[2]"));
                wait.Until(ExpectedConditions.StalenessOf(el));
            }
            WebDriverWait waits = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waits.Until(ExpectedConditions.ElementIsVisible(By.XPath("//em[contains(text(),'There are no items in your cart.')]")));

            Assert.IsTrue(driver.FindElements(By.XPath("//em[contains(text(),'There are no items in your cart.')]")).Count>0,"Error");
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}