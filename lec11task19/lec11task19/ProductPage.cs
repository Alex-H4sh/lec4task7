﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace lec11task19
{
    class ProductPage
    {
        public ProductPage(IWebDriver driver_)
        {
            driver = driver_;
        }

        public void AddProduct()
        {
            int old_value = OldQuantity();
            SetProductSize();
            WaitUntilChange(old_value);
        }

        private void SetProductSize()
        {
            if (driver.FindElements(By.XPath("//select[@name='options[Size]']")).Count > 0)
            {
                driver.FindElement(By.XPath("//select[@name='options[Size]']/option[2]")).Click();
            }
        }

        private int OldQuantity()
        {
            IWebElement el = driver.FindElement(By.XPath("//span[@class='quantity']"));
            return Int32.Parse(el.Text);
        }

        private void WaitUntilChange(int old_value)
        {
            driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.XPath("//span[@class='quantity']")), (old_value + 1).ToString()));
        }

        private IWebDriver driver;
    }
}
