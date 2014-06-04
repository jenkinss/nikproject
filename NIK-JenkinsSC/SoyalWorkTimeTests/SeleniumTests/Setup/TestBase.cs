using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SoyalWorkTimeTests
{
    
    public static class SeleniumDriver {
        public static IWebDriver driver;
    }

    public class TestBase
    {
        [SetUp]
        public void SetupSteps()
        {

            //Webserver initialize
            int portnumber = 5042;
            WebServerSetup.sitePath = Environment.GetEnvironmentVariable("soyalpublishfolder");
            WebServerSetup.portNumber = portnumber;
            WebServerSetup.StartIIS();


            //Selenium initialize
            SeleniumDriver.driver = new FirefoxDriver();
            SeleniumDriver.driver.Navigate().GoToUrl("localhost" + ":" + portnumber);
            starturl = SeleniumDriver.driver.Url;

        }

        [TearDown]
        public void DeSetupSteps()
        {
            SeleniumDriver.driver.Quit();
            WebServerSetup.StopIIS();
        }

        private string starturl;

        public void reseturl() { SeleniumDriver.driver.Navigate().GoToUrl(starturl); }

    }
}
