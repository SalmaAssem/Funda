using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

using System.Threading;
using System.Diagnostics;

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
        protected void HoverElement(AutomatedElement element)
        {
            Actions builder = new OpenQA.Selenium.Interactions.Actions(driver.driver);

            IWebElement _ELmentToHover = driver.driver.FindElement(element.ByElement);

            builder.MoveToElement(_ELmentToHover).Perform();
        }

        protected void ClickElement(AutomatedElement Element, String ElementName, ElementValidation validation)
        {
            if (IsElementEnabled(Element))
            {
                try
                {
                    //Hover on the Element
                    HoverElement(Element);

                    driver.driver.FindElement(Element.ByElement).Click();
                }
                    catch
                {
                    if (validation == ElementValidation.Mandatory)
                    {

                        testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.UnClickableMsg, validation);

                        testMessages.End_Test();
                    }

                    else if (validation == ElementValidation.Non_Mandatory)
                        testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.UnClickableMsg, validation);

                    else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                        testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.UnClickableMsg, validation);

                }
            }
            else
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.NotEnabledMsg, validation);

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.NotEnabledMsg, validation);

                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.NotEnabledMsg, validation);


            }
        }

        private void ClearElement(AutomatedElement Element, String ElementName, ElementValidation Validation)
        {
            try
            {
                driver.driver.FindElement(Element.ByElement).Clear();
            }
            catch
            {
                if (Validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.CantClearMsg, Validation);

                    testMessages.End_Test();
                }

                else if (Validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.CantClearMsg, Validation);

                else if (Validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.CantClearMsg, Validation);

            }

        }

        private void SendKeystoElement(AutomatedElement Element, String _Key, String ElementName, ElementValidation Validation)
        {
            try
            {
                driver.driver.FindElement(Element.ByElement).SendKeys(_Key);
            }
            catch
            {
                if (Validation == ElementValidation.Mandatory)
                {
                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.CantSendKeysMsg, Validation);
                    testMessages.End_Test();
                }

                else if (Validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.CantSendKeysMsg, Validation);

                else if (Validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.CantSendKeysMsg, Validation);

            }

        }

        protected void EnterText_ToElement(AutomatedElement Element, string Key, String ElementName, ElementValidation validation)
        {
           
            //Clear Element
            ClearElement(Element, ElementName, validation);

            //Send Keys to element
            SendKeystoElement(Element, Key, ElementName, validation);
        }

        protected void SelectFromList(AutomatedElement Element, string Selection, ElementValidation Validation)
        {
            //Find Element
            FindVisibleElement(20, Element, "Drop down Menu", ElementValidation.Mandatory);

            //Hover On Element
            HoverElement(Element);

            try
            {
                //Select from the list
                new SelectElement(driver.driver.FindElement(Element.ByElement)).SelectByText(Selection);
            }

            catch
            {
                if (Validation == ElementValidation.Mandatory)
                {
                    testMessages.Mandatory_Message = Messages.ReturnMessage(Selection, MessageType.CantSelectFromListMsg, Validation);
                    testMessages.End_Test();
                }

                else if (Validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Selection, MessageType.CantSelectFromListMsg, Validation);

                else if (Validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Selection, MessageType.CantSelectFromListMsg, Validation);

            }
        }

        protected void SelectFromList_ByValue(AutomatedElement Element, string Selection, ElementValidation Validation)
        {
            //Find Element
            FindVisibleElement(20, Element, "Drop down Menu", ElementValidation.Mandatory);

            //Hover On Element
            HoverElement(Element);

            try
            {
                //Select from the list
                new SelectElement(driver.driver.FindElement(Element.ByElement)).SelectByValue(Selection);
            }

            catch
            {
                if (Validation == ElementValidation.Mandatory)
                {
                    testMessages.Mandatory_Message = Messages.ReturnMessage(Selection, MessageType.CantSelectFromListMsg, Validation);
                    testMessages.End_Test();
                }

                else if (Validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Selection, MessageType.CantSelectFromListMsg, Validation);

                else if (Validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Selection, MessageType.CantSelectFromListMsg, Validation);

            }
        }

        protected void SelectFromList_ByIndex(AutomatedElement Element, int Index, ElementValidation Validation)
        {
            //Find Element
            FindVisibleElement(20, Element, "Drop down Menu", ElementValidation.Mandatory);

            //Hover On Element
            HoverElement(Element);

            try
            {
                //Select from the list
                new SelectElement(driver.driver.FindElement(Element.ByElement)).SelectByIndex(Index);
            }

            catch
            {
                if (Validation == ElementValidation.Mandatory)
                {
                    testMessages.Mandatory_Message = Messages.ReturnMessage("Option " + Index, MessageType.CantSelectFromListMsg, Validation);
                    testMessages.End_Test();
                }

                else if (Validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage("Option " + Index, MessageType.CantSelectFromListMsg, Validation);

                else if (Validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage("Option " + Index, MessageType.CantSelectFromListMsg, Validation);

            }
        }

        protected void EnterText_UsingJS(AutomatedElement Element, string text, string ElementName, ElementValidation validation)
        {

            IWebElement elmnt = driver.driver.FindElement(Element.ByElement);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver.driver;

            try
            {
                js.ExecuteScript("arguments[0].value = '" + text + "';", elmnt);
            }
            
            catch
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.CantSendKeysMsg, validation);

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.CantSendKeysMsg, validation);

                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.CantSendKeysMsg, validation);
                    


            }

        }

        protected void Enter_KeyBoardTab()
        {
            Actions builder = new Actions(driver.driver);
            builder.SendKeys(Keys.Tab);
        }
    }
        
}


