using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.VisualStudio;
using NUnit.VisualStudio.TestAdapter;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SoyalWorkTimeTests.SeleniumTests.Models;
using System.Threading;

namespace SoyalWorkTimeTests
{
    public partial class SoyalWorkTimeTest
    {
        [TestFixture]
        public class Tests : TestBase
        {
            [TestCase]
            public void LoginTest()
            {
                Assert.True(SeleniumDriver.driver.Title.Contains("WorkTime Manager"), "Title contains WorkTime Manager");
                Assert.True(SeleniumDriver.driver.Title.Contains("Log in"), "Title contains Log in");
                Assert.True(SeleniumDriver.driver.Url.Contains("Login"), "Url contains Login");

                Loginpage page = new Loginpage();

                page.usernamelement.Clear();
                page.usernamelement.SendKeys("root");

                page.passwordelement.Clear();
                page.passwordelement.SendKeys("123456");
                //page.loginbutton.Click();

            }

            [TestCase]
            public void LoginTest10Times()
            {
                for (var i = 0; i < 10; i++)
                {
                    reseturl();
                    LoginTest();
                }
            }
            
        }
    }
}
