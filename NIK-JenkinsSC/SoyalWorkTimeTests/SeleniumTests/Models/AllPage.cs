using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoyalWorkTimeTests.SeleniumTests.Models
{
    public class Allpage : PageObjectBase
    {
        public Allpage() : base() { }

        public IWebElement pagetitle = XPathTextHelper("Soyal WM");
        public IWebElement mainpagelink = XPathTextHelper("Main", "a");
        public IWebElement adminpagelink = XPathTextHelper("Administration", "a");

    }
}
