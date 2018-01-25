using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;
using System.Drawing.Imaging;
using System.Drawing;

namespace Automation.Utilities
{

    public partial class Utilities
    {


        protected readonly Driver driver;
        protected TestMessages testMessages;
        protected string fixtureTitle;

        
        protected Utilities(Driver _driver, TestMessages _testMessages, string _fixtureTitle)
        {
            driver = _driver;
            testMessages = _testMessages;
            fixtureTitle = _fixtureTitle;
        }


    }
}


