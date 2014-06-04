using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoyalWorkTimeTests.SeleniumTests.Models
{
    public abstract class PageObjectBase
    {
        public static IWebElement XPathTextHelper(string textToFind)
        {
            return SeleniumDriver.driver.FindElement(By.XPath(@"//*[text()=""" + textToFind + @"""]"));
        }
        public static IWebElement XPathTextHelper(string textToFind, string element)
        {
            return SeleniumDriver.driver.FindElement(By.XPath(@"//" + element + @"[text()=""" + textToFind + @"""]"));
        }
    }
}
