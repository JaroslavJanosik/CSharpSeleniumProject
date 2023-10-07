using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SendEmailProject.Framework
{
    public class Elements : ReadOnlyCollection<Element>
    {
        private readonly Driver _driver;
        private readonly ReadOnlyCollection<IWebElement> _elements;

        public Elements(Driver driver, IList<IWebElement> list, By foundBy)
            : base(list.Select(el => new Element(driver, el, foundBy)).ToList())
        {
            _driver = driver;
            _elements = new ReadOnlyCollection<IWebElement>(list);
            FoundBy = foundBy;
        }

        public By FoundBy { get; }
        public bool IsEmpty => Count == 0;
        public Driver Driver => _driver ?? throw new NullReferenceException("_driver is null");
        public ReadOnlyCollection<IWebElement> Current => _elements ?? throw new NullReferenceException("_elements is null");
    }
}
