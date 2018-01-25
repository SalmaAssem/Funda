using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Automation.Utilities
{
    [Flags]
    public  enum ElementValidation {Mandatory, Non_Mandatory, Non_Mandatory_Till_EOT };
    public enum MessageType {NotFoundMsg,  UnClickableMsg, NotVisibleMsg, NotEnabledMsg, CantSendKeysMsg, CantClearMsg,
                              NoSuchFrameMsg, FoundInInvalidPlaceMsg, VisibleInInvalidPlaceMsg, EnabledInInvalidPlaceMsg, CantSelectFromListMsg, NotSelectedMsg,
                              NotCorrect, WrongColor};

    public static class Messages
    {
        public static string ReturnMessage(String ElementName, MessageType messageType, ElementValidation validation)
        {
            switch (messageType)
            {
                case MessageType.NotFoundMsg:
                    return "Unable to locate element [" + ElementName + "] _ [" + validation + "]";
                    break;

                case MessageType.UnClickableMsg:
                    return "Unable to click on element [" + ElementName + "] _ [" + validation + "]";
                    break;

                case MessageType.NotVisibleMsg:
                    return "This Element isn't visible [" + ElementName + "] _ [" + validation + "]";
                    break;

                case MessageType.NotEnabledMsg:
                    return "This Element is not enabled  [ " + ElementName + " ] _ [ " + validation + " ]";
                    break;

                case MessageType.CantSendKeysMsg:
                    return "Unable to enter text to element " + ElementName + "] _ [" + validation + "]";
                    break;

                case MessageType.CantClearMsg:
                    return "Unable to clear element " + ElementName + "] _ [" + validation + "]";
                    break;

                case MessageType.NoSuchFrameMsg:
                    return "Unable to find Frame " + ElementName + "] _ [" + validation + "]";
                    break;

                case MessageType.FoundInInvalidPlaceMsg:
                    return "Element " + ElementName + "] is found in invalid place _ [" + validation + "]";
                    break;

                case MessageType.VisibleInInvalidPlaceMsg:
                    return "Element " + ElementName + "] is visible in invalid place _ [" + validation + "]";
                    break;

                case MessageType.EnabledInInvalidPlaceMsg:
                    return "Element " + ElementName + "] is enabled in invalid place _ [" + validation + "]";
                    break;

                case MessageType.CantSelectFromListMsg:
                    return "Can't Select  " + ElementName + "] From List _ [" + validation + "]";
                    break;

                case MessageType.NotSelectedMsg:
                    return "Element " + ElementName + "] isn't selected [" + validation + "]";
                    break;

                case MessageType.NotCorrect:
                    return "Element " + ElementName + "] text isn't correct [" + validation + "]";
                    break;

                case MessageType.WrongColor:
                    return "Element " + ElementName + "] color isn't correct [" + validation + "]";
                    break;

                default:
                    return "Invalid Operation with" + ElementName;
                    break;

                        

            }
                
                    
        }
        
    }
}
