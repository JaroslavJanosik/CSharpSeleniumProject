using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Drawing;
using System.Threading;

namespace SendEmailProject.Framework
{
    public class Element
    {
        private readonly Driver _driver;
        private readonly IWebElement _element;

        public By FoundBy { get; }

        public Element(Driver driver, IWebElement element, By foundBy)
        {
            _driver = driver;
            _element = element;
            FoundBy = foundBy;
        }

        public Driver Driver => _driver ?? throw new NullReferenceException("_driver is null");

        public IWebElement Current => _element;
        public string TagName => Current.TagName;
        public string Text => Current.Text;
        public bool Displayed => Current.Displayed;
        public bool Selected => Current.Selected;
        public bool Enabled => Current.Enabled;
        public Point Location => Current.Location;
        public Size Size => Current.Size;
        public string GetAttribute(string attributeName) => Current.GetAttribute(attributeName);

        public Element FindElement(By by)
        {
            return new Element(_driver, _element.FindElement(by), by);
        }

        public void Click(ISearchContext shadowRoot = null)
        {
            try
            {
                WaitToBeClickable(_element);
                ExtentReporting.LogInfo($"Click on element found {FoundBy}");
                Current.Click();
            }
            catch (WebDriverException exc)
            {
                ExtentReporting.LogInfo($"Exception caught while clicking on element:\n{exc.Message}");
                if (shadowRoot != null)
                {
                    ExtentReporting.LogInfo($"Click on element found {FoundBy}");
                    shadowRoot.FindElement(FoundBy).Click();
                } else
                {
                    ExtentReporting.LogInfo($"Click on element found {FoundBy}");
                    _driver.Current.FindElement(FoundBy).Click();
                }
            }
        }

        public void SendKeys(string text)
        {
            Thread.Sleep(500);
            Current.Clear();
            ExtentReporting.LogInfo($"Enter text into the element found {FoundBy}");
            Current.SendKeys(text);
        }

        public void WaitToBeClickable(IWebElement element, int timeouInSecs = 20)
        {
            WebDriverWait wait = new (_driver.Current, TimeSpan.FromSeconds(timeouInSecs));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
    }
}
