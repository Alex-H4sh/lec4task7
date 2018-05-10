using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lec11task19
{
    [TestClass]
    public class UnitTest
    {
        MainDriver driver;

        [TestInitialize]
        public void init() {
            IWebDriver ch_driver = new ChromeDriver();
            driver = MainDriver.getInstance(ch_driver); // Singleton
        }

        [TestMethod]
        public void Test()
        {
            MainPage m_page = new MainPage(driver);
            ProductPage pr_page = new ProductPage(driver);
            Checkout checkout = new Checkout(driver);

            for (int i = 0; i < 3; i++) {
                m_page.ChooseFirstProduct();
                pr_page.AddProduct();
            }

            checkout.GoToBin();
            for (int i = 0; i < 3; i++)
                checkout.DeleteFirstProduct();

            SupportFunctions.CheckCorrectness(driver);
        }

        [TestCleanup]
        public void end()
        {
            SupportFunctions.EndDriver(driver);
        }
    }
}