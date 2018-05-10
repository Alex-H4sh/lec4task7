using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace lec11task19
{
    class MainPage
    {
        public MainPage(MainDriver driver_)
        {
            driver = driver_;
        }

        public void ChooseFirstProduct()
        {
            driver.Driver.Navigate().GoToUrl("https://localhost/litecart/");
            driver.Driver.FindElement(By.XPath("//div[@id='box-most-popular']//ul/li[1]")).Click();
        }

        private MainDriver driver;
    }
}
