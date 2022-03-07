using ArtinTestProject;
using OpenQA.Selenium;
using System;
using System.Drawing;

namespace IndraAvitech.Framework
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

        public Element FindElement(By by)
        {            
            return new Element(Driver, Current.FindElement(by), by);
        }

        public Elements FindElements(By by)
        {
            return new Elements(Driver, Current.FindElements(by), by);
        }

        public void Click()
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {                    
                    Current.Click();
                    break;
                }
                catch (WebDriverException e)
                {
                    // do nothing
                }
            }            
        }

        public void SendKeys(string text)
        {            
            Current.Clear();
            Current.SendKeys(text);
        }
    }
}
