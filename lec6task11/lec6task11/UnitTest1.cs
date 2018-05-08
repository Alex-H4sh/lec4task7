using System;
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

        string gen_email(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghiklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray())+"@gmail.com";
        }

        [TestMethod]
        public void Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost/litecart/");
            IWebElement reg = driver.FindElement(By.XPath("//form[@name='login_form']//tr[5]/td/a"));
            reg.Click();

            // Fill registration form
            SelectElement country_select = new SelectElement(driver.FindElement(By.XPath("//select[@name='country_code']")));
            country_select.SelectByText("United States");

            driver.FindElement(By.XPath("//button[@name='create_account']")).Submit();

            driver.FindElement(By.XPath("//input[@name='firstname']")).SendKeys("Jho");
            driver.FindElement(By.XPath("//input[@name='lastname']")).SendKeys("Smith");
            driver.FindElement(By.XPath("//input[@name='address1']")).SendKeys("Times Garden");
            driver.FindElement(By.XPath("//input[@name='postcode']")).SendKeys("55555");

            string login = gen_email(10);
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(login);
            driver.FindElement(By.XPath("//input[@name='phone']")).SendKeys("+18934284903");

            string password = "123456";
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//input[@name='confirmed_password']")).SendKeys(password);

            driver.FindElement(By.XPath("//input[@name='city']")).SendKeys("California");
            driver.FindElement(By.XPath("//button[@name='create_account']")).Click();

            //Logout
            driver.FindElement(By.XPath("//div[@class='content']//a[contains(text(),'Logout')]")).Click();

            // Login
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(login);
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@name='login']")).Click();

            // Logout again
            driver.FindElement(By.XPath("//div[@class='content']//a[contains(text(),'Logout')]")).Click();
        }

        [TestCleanup]
        public void end()
        {
            driver.Quit();
        }
    }
}
