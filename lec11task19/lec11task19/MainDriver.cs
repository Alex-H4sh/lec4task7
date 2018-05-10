using OpenQA.Selenium;

namespace lec11task19
{
    class MainDriver
    {
        private static MainDriver instance;
        static MainDriver() { }
        private MainDriver() { }
        public IWebDriver Driver {get; private set;}
        protected MainDriver(IWebDriver driver)
        {
            this.Driver = driver;
        }
        public static MainDriver getInstance(IWebDriver driver)
        {
            if (instance == null)
                instance = new MainDriver(driver);
            return instance;
        }
    }
}
