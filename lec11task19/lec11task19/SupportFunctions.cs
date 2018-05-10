using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec11task19
{
    class SupportFunctions
    {
        static public void CheckCorrectness(MainDriver driver)
        {
            WebDriverWait waits = new WebDriverWait(driver.Driver, TimeSpan.FromSeconds(10));
            waits.Until(ExpectedConditions.ElementIsVisible(By.XPath("//em[contains(text(),'There are no items in your cart.')]")));
            Assert.IsTrue(driver.Driver.FindElements(By.XPath("//em[contains(text(),'There are no items in your cart.')]")).Count > 0, "Error");
        }

        static public void EndDriver(MainDriver driver)
        {
            driver.Driver.Quit();
        }
    }
}
