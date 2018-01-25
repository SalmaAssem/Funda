using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.PhantomJS;

namespace Automation.Utilities
{
    [Flags]
    public enum Browser { IE, Chrome, FireFox, Android,Edge };
    
    public class Driver
    {
        public  IWebDriver driver;
        private string baseURL;
        
       
        public Driver(Browser browser, string Driver_Path)
        {

            if (browser == Browser.Chrome)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("start-maximized");

                driver = new ChromeDriver(Driver_Path, options, TimeSpan.FromMinutes(7));
                
            }


            else if (browser == Browser.IE)
            {
                driver = new InternetExplorerDriver(Driver_Path);
            }


            else if (browser == Browser.FireFox)
                driver = new FirefoxDriver();

            else if (browser == Browser.Edge)
                driver = new EdgeDriver(Driver_Path);

        }

        public void ImplicitlyManageTimeout(double Time)
        {
            //Manage time out Implicitly in Seconds
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
        }

        public void FreshSetup(string URL)
        {
            baseURL = URL;
           
            driver.Navigate().GoToUrl(baseURL);
        }
        
        public void CloseBrowser()
        {
            driver.Close();
        }

        public void QuitDriver()
        {
            driver.Quit();
        }

        public void TeardownDriver()
        {
            try
            {
                CloseBrowser();
                QuitDriver();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
           
        }

        
    }
}

