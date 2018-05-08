using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec11task19
{
    class SupportFunctions
    {
        static public void CheckCorrectness(IWebDriver driver)
        {
            WebDriverWait waits = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waits.Until(ExpectedConditions.ElementIsVisible(By.XPath("//em[contains(text(),'There are no items in your cart.')]")));
            Assert.IsTrue(driver.FindElements(By.XPath("//em[contains(text(),'There are no items in your cart.')]")).Count > 0, "Error");
        }
    }
}
