using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace lec11task19
{
    class Checkout
    {
        public Checkout(MainDriver driver_)
        {
            driver = driver_;
        }

        public void GoToBin()
        {
            driver.Driver.FindElement(By.XPath("//a[contains(text(),'Checkout')]")).Click();
        }

        public void DeleteFirstProduct()
        {
            ClickOnFirstProduct();
            RemoveItem();
        }

        private void ClickOnFirstProduct()
        {
            if (driver.Driver.FindElements(By.XPath("//div[@id='checkout-cart-wrapper']//ul[@class='shortcuts']/li")).Count > 0)
            {
                driver.Driver.FindElement(By.XPath("//div[@id='checkout-cart-wrapper']//ul[@class='shortcuts']/li[1]")).Click();
            }
        }

        private void RemoveItem()
        {
            driver.Driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();
            WebDriverWait wait = new WebDriverWait(driver.Driver, TimeSpan.FromSeconds(5));
            IWebElement el = driver.Driver.FindElement(By.XPath("//table[@class='dataTable rounded-corners']//tr[2]"));
            wait.Until(ExpectedConditions.StalenessOf(el));
        }

        private MainDriver driver;
    }
}
