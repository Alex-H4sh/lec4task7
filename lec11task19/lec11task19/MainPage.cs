using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace lec11task19
{
    class MainPage
    {
        public MainPage(IWebDriver driver_)
        {
            driver = driver_;
        }

        public void ChooseFirstProduct()
        {
            driver.Navigate().GoToUrl("https://localhost/litecart/");
            driver.FindElement(By.XPath("//div[@id='box-most-popular']//ul/li[1]")).Click();
        }

        private IWebDriver driver;
    }
}
