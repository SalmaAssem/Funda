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
        private bool IsElementEnabled(AutomatedElement Element)
        {
            IWebElement element = driver.driver.FindElement(Element.ByElement);
            if (element.Enabled)
            {
                if (element.GetAttribute("disabled") == "true")
                {
                    return false;
                }

                return true;

            }
            else
            {
                return false;
            }
        }

        private bool IsElementVisible(AutomatedElement Element)
        {
            
            try
            {
                if (driver.driver.FindElement(Element.ByElement).Displayed)

                    return true;
                else
                {


                    return false;
                }
            }

            catch
            {

                return false;
            }
        }

        protected bool IsElementVisible(double Time, AutomatedElement Element)
        {
            WebDriverWait wait = new WebDriverWait(driver.driver, TimeSpan.FromSeconds(Time));

            try
            {

                wait.Until(ExpectedConditions.ElementIsVisible(Element.ByElement));

                return true;
            }

            catch
            {

                return false;
            }
        }

        protected bool IsElementPresent(AutomatedElement Element)
        {
            try
            {
                driver.driver.FindElement(Element.ByElement);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void FindVisibleElement(double Time, AutomatedElement Element, String ElementName, ElementValidation validation)
        {
            WebDriverWait wait = new WebDriverWait(driver.driver, TimeSpan.FromSeconds(Time));
            try
            {

                wait.Until(ExpectedConditions.ElementExists(Element.ByElement));
            }
            catch
            {

                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.NotFoundMsg, validation);
                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.NotFoundMsg, validation);

                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.NotFoundMsg, validation);
                   
            }

            //Ensure Element is visible
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(Element.ByElement));
            }
            catch
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.NotVisibleMsg, validation);

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.NotVisibleMsg, validation);

                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.NotVisibleMsg, validation);
                   
            }

        }

        protected void Assert_Element_NotFound(double Time, AutomatedElement Element, String ElementName, ElementValidation ExistanceValidation, ElementValidation VisibilityValidation)
        {
          
            Thread.Sleep(TimeSpan.FromSeconds(Time));

            if (IsElementPresent(Element))
            {
                if (ExistanceValidation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.FoundInInvalidPlaceMsg, ExistanceValidation);
                    testMessages.End_Test();
                }

                else if (ExistanceValidation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.FoundInInvalidPlaceMsg, ExistanceValidation);

                else if (ExistanceValidation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.FoundInInvalidPlaceMsg, ExistanceValidation);
                   

                
                if (IsElementVisible(Element))
                {
                    if (VisibilityValidation == ElementValidation.Mandatory)
                    {
                        testMessages.Mandatory_Message = Messages.ReturnMessage(ElementName, MessageType.VisibleInInvalidPlaceMsg, VisibilityValidation);
                        testMessages.End_Test();
                    }

                    else if (VisibilityValidation == ElementValidation.Non_Mandatory)
                        testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.VisibleInInvalidPlaceMsg, VisibilityValidation);

                    else if (VisibilityValidation == ElementValidation.Non_Mandatory_Till_EOT)
                        testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(ElementName, MessageType.VisibleInInvalidPlaceMsg, VisibilityValidation);
                        
                }

            }
        }

        protected void Assert_ElementTxt_IsCorrect(AutomatedElement Element, string Expected_Text, string Element_Tag, ElementValidation validation)
        {
            //Find Element
            FindVisibleElement(30, Element, Element_Tag, validation);

            //Get Element text
            string ElementInnerText = driver.driver.FindElement(Element.ByElement).Text;

            //Check if Element test = Expected text
            if (ElementInnerText.ToLower() == Expected_Text.ToLower()) { }

            else
            {
                if (validation == ElementValidation.Mandatory)
                {
                    testMessages.Mandatory_Message = Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    testMessages.Mandatory_Message += "\n The actual is '" + ElementInnerText + "' but expected '" + Expected_Text + "'";

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                {
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    testMessages.Non_Mandatory_Message += "\n The actual is '" + ElementInnerText + "' but expected '" + Expected_Text + "'";
                }

                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                {
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n The actual is '" + ElementInnerText + "' but expected '" + Expected_Text + "'";
                   
                }


            }



        }

        protected void Assert_Element_Contains_Text(AutomatedElement Element, string Expected_Text, string Element_Tag, ElementValidation validation)
        {
            //Find Element
            FindVisibleElement(30, Element, Element_Tag, validation);

            //Get Element Text
            string ElementInnerText = driver.driver.FindElement(Element.ByElement).Text;

            //Check if Element text contains Expected text
            if (driver.driver.FindElement(Element.ByElement).Text.ToLower().Contains(Expected_Text.ToLower())) { }

            else
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    testMessages.Mandatory_Message += "\n The actual is '" + ElementInnerText + "' but expected '" + Expected_Text + "'";

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                {
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    testMessages.Non_Mandatory_Message += "\n The actual is '" + ElementInnerText + "' but expected '" + Expected_Text + "'";
                }


                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                {
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n The actual is '" + ElementInnerText + "' but expected '" + Expected_Text + "'";
                    
                }


            }
        }

        protected void Assert_Number_BetweenTwoNumbers(int NumberToCheck, int Min, int Max, string Element_Tag, ElementValidation validation)
        {
           
            //Check if NumberToCheck is between Min and Max
            if (NumberToCheck >= Min && NumberToCheck <= Max) { }

            else
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    
                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    


                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);
                    


            }
        }

        protected void Assert_Number_GreaterThanAnotherNumber(int NumberToCheck, int Min, string Element_Tag, ElementValidation validation)
        {

            //Check if NumberToCheck is greater than Min
            if (NumberToCheck >= Min) { }

            else
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);



                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);



            }
        }

        public string ReturnElementText(AutomatedElement Element)
        {
            //Return its text
            return driver.driver.FindElement(Element.ByElement).Text;
        }

        public static string ToHex(Color color)
        {
            return String.Format("#{0}{1}{2}{3}"
                , color.A.ToString("X").Length == 1 ? String.Format("0{0}", color.A.ToString("X")) : color.A.ToString("X")
                , color.R.ToString("X").Length == 1 ? String.Format("0{0}", color.R.ToString("X")) : color.R.ToString("X")
                , color.G.ToString("X").Length == 1 ? String.Format("0{0}", color.G.ToString("X")) : color.G.ToString("X")
                , color.B.ToString("X").Length == 1 ? String.Format("0{0}", color.B.ToString("X")) : color.B.ToString("X"));
        }

        public void ElementColor_IsCorrect(AutomatedElement Element, int r, int g, int b, string Element_Tag, ElementValidation validation)
        {
            
           string ElementColor = driver.driver.FindElement(Element.ByElement).GetCssValue("color");

            ElementColor =  ElementColor.Trim('r', 'g', 'b', 'a', '(', ')');

            string[] a = ElementColor.Split(',');
            
            if (r == int.Parse(a[0]) && g == int.Parse(a[1]) && b == int.Parse(a[2])) { }

            else
            {
                if (validation == ElementValidation.Mandatory)
                {

                    testMessages.Mandatory_Message = Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);

                    testMessages.End_Test();
                }

                else if (validation == ElementValidation.Non_Mandatory)
                    testMessages.Non_Mandatory_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);



                else if (validation == ElementValidation.Non_Mandatory_Till_EOT)
                    testMessages.Non_Mandatory_Till_EOT_Message += "\n" + Messages.ReturnMessage(Element_Tag, MessageType.NotCorrect, validation);



            }

        }
        
    }
}

 

