using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SendEmailProject.Framework
{
    public class Driver
    {
        private readonly TestConfiguration _testConfiguration;
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
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new ChromeDriver();
                    break;
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    _driver = new FirefoxDriver();
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    _driver = new EdgeDriver();
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
            IWebElement nativeElement = _wait.Until(ExpectedConditions.ElementExists(by));
            return new Element(this, nativeElement, by);
        }

        public Elements FindElements(By by)
        {
            IList<IWebElement> nativeElements = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            return new Elements(this, nativeElements, by);
        }

        public Element FindShadowDomElement(ISearchContext shadowRoot, By by)
        {
            return new Element(this, shadowRoot.FindElement(by), by);
        }

        public Elements FindShadowDomElements(ISearchContext shadowRoot, By by)
        {     
            IList<IWebElement> nativeElements = shadowRoot.FindElements(by);
            return new Elements(this, nativeElements, by);
        }
    }
}