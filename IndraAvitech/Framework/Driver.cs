using IndraAvitech.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace ArtinTestProject
{
    public class Driver
    {
        private TestConfiguration _testConfiguration;
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public Driver(TestConfiguration driverConfiguration)
        {
            _testConfiguration = driverConfiguration;
            InitDriver(_testConfiguration.BrowserType);            
        }        

        private void InitDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    _driver = new ChromeDriver(_testConfiguration.ChromeDriverPath);
                    break;
                case "firefox":
                    _driver = new FirefoxDriver(_testConfiguration.FirefoxDriverPath);
                    break;
                default:
                    throw new WebDriverException($"Problem with initializing driver for: {browser} browser");
            }

            _wait = new WebDriverWait(Current, TimeSpan.FromSeconds(_testConfiguration.DefaultWaitTime));            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_testConfiguration.DefaultWaitTime);
            MaximizeBrowser();           
        }

        public IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is Null");
        public WebDriverWait Wait => _wait ?? throw new NullReferenceException("_wait is Null");

        public string Url => Current.Url;
        public string Title => Current.Title;
        
        public void GoTo(string url)
        {
            Current.Navigate().GoToUrl(url);
        }

        public void RefreshPage()
        {
            Current.Navigate().Refresh();
        }

        public void MaximizeBrowser()
        {
            Current.Manage().Window.Maximize();
        }

        public void Destroy()
        {
            Current.Quit();
        }
        
        public Element FindElement(By by)
        {           
            return new Element(this, Current.FindElement(by), by);
        }

        public Elements FindElements(By by)
        {
            return new Elements(this, Current.FindElements(by), by);
        }
    }
}