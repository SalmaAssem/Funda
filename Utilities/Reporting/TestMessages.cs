using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;

using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Utilities
{
   
    public class TestMessages
    {
        public string Mandatory_Message;
        public string Non_Mandatory_Message;
        public string Non_Mandatory_Till_EOT_Message;
        public string Success_Message;

        public TestMessages()
        {
            Mandatory_Message = "";
            Non_Mandatory_Message = "";
            Non_Mandatory_Till_EOT_Message = "";
            Success_Message = "";
        }
        
        public void Clear_Messages()
        {
            Mandatory_Message = "";
            Non_Mandatory_Message = "";
            Non_Mandatory_Till_EOT_Message = "";
            Success_Message = "";

            
        }

        public string Pass_Test(string SuccessMessage)
        {
            return Non_Mandatory_Message + "\n" + SuccessMessage;
        }

        //public void End_Test(string TestName, string SuccessMessage)
        //{
        //    Report.test = Report.extent.CreateTest(TestContext.CurrentContext.Test.Name);

        //    if (Non_Mandatory_Till_EOT_Message != "" || Mandatory_Message != "")
        //    {
        //        Report.Log_TestResult(NUnit.Framework.Interfaces.TestStatus.Failed, Non_Mandatory_Till_EOT_Message + "\n" + Non_Mandatory_Message + "\n" + Mandatory_Message);

        //        Assert.Fail(Non_Mandatory_Till_EOT_Message + "\n" + Non_Mandatory_Message + "\n" + Mandatory_Message);

        //    }
                

        //    else if(Non_Mandatory_Message != "")
        //    {
        //        Report.Log_TestResult(NUnit.Framework.Interfaces.TestStatus.Passed, SuccessMessage + " but " + Non_Mandatory_Message);

        //        Success_Message = SuccessMessage;

        //        Assert.Pass(SuccessMessage + " but " + Non_Mandatory_Message);

        //    }
                

        //    else
        //    {
        //        Report.Log_TestResult(NUnit.Framework.Interfaces.TestStatus.Passed, SuccessMessage);

        //        Success_Message = SuccessMessage;

        //        Assert.Pass(SuccessMessage);

        //    }
              
        //}

        public void End_Test()
        {
            if (Non_Mandatory_Till_EOT_Message != "" || Mandatory_Message != "")
            {
                Assert.Fail(Non_Mandatory_Till_EOT_Message + "\n" + Non_Mandatory_Message + "\n" + Mandatory_Message);
                
            }


            else if (Non_Mandatory_Message != "")
            {
                Assert.Pass("Test has succeeded but " + Non_Mandatory_Message);

            }


            else
            {
                Assert.Pass("Test has succeeded.");

            }

        }
    }

    
}
