using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test11
{
    [TestClass]
    public class UnitTest
    {
        static IWebDriver driver;

        string gen_email(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghiklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray())+"@gmail.com";
        }

        public void unhide(IWebDriver driver, IWebElement element)
        {
            String script = "arguments[0].style.opacity=1;"
              + "arguments[0].style['transform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['MozTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['WebkitTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['msTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['OTransform']='translate(0px, 0px) scale(1)';"
              + "return true;";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script, element);
        }

        [TestMethod]
        public void Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            IWebElement reg = driver.FindElement(By.XPath("//form[@name='login_form']//tr[5]/td/a"));
            reg.Click();

            IWebElement list = driver.FindElement(By.XPath("//select[@name='country_code']"));
            unhide(driver, list);
            IWebElement usa = list.FindElement(By.XPath(".//option[contains(text(),'United States')]"));
            usa.Click();

            driver.FindElement(By.XPath("//button[@name='create_account']")).Submit();

            driver.FindElement(By.XPath("//input[@name='firstname']")).SendKeys("Jho");
            driver.FindElement(By.XPath("//input[@name='lastname']")).SendKeys("Smith");
            driver.FindElement(By.XPath("//input[@name='address1']")).SendKeys("Times Garden");
            driver.FindElement(By.XPath("//input[@name='postcode']")).SendKeys("55555");
            
            /*IWebElement state = driver.FindElement(By.XPath("//select[@name='zone_code']"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].removeAttribute('disabled');", state);*/

            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(gen_email(10));
            driver.FindElement(By.XPath("//input[@name='phone']")).SendKeys("+18934284903");

            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("root");
            driver.FindElement(By.XPath("//input[@name='confirmed_password']")).SendKeys("root");

            driver.FindElement(By.XPath("//input[@name='city']")).SendKeys("California");
            driver.FindElement(By.XPath("//button[@name='create_account']")).Submit();
        }

        [TestCleanup]
        public void end()
        {
            //driver.Quit();
        }
    }
}
