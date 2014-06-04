using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoyalWorkTimeTests.SeleniumTests.Models
{
    public class Loginpage : Allpage
    {
        public Loginpage() : base() { }

        public IWebElement logintitle = SeleniumDriver.driver.FindElement(By.XPath(@"//*[contains(text(),""Log in."")]"));
        public IWebElement loginlink = SeleniumDriver.driver.FindElement(By.Id("loginLink"));
        public IWebElement usernamelement = SeleniumDriver.driver.FindElement(By.Id("UserName"));
        public IWebElement passwordelement = SeleniumDriver.driver.FindElement(By.Id("Password"));
        public IWebElement loginbutton = SeleniumDriver.driver.FindElement(By.XPath(@"//input[@value=""Log in""][@type=""submit""]"));

    }
}
