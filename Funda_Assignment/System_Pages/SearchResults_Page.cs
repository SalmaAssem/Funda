using System;
using Automation.Utilities;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Globalization;

namespace Funda_Assignment
{


    public class SearchResults_Page : Utilities
    {
        public SearchResults_Page(Driver driver, TestMessages testmesssages, string ClassName)
            : base(driver, testmesssages, ClassName)
        {
            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(driver.driver, this);
        }


        #region PageObjects

        AutomatedElement span_ApartmentAddress = new AutomatedElement(LocateBy.XPath, LocatorValue: "//small[@class='search-result-subtitle']");
        AutomatedElement span_ApartmentPrice = new AutomatedElement(LocateBy.XPath, LocatorValue: "//span[@class='search-result-price']");
        AutomatedElement span_AvailableHourses = new AutomatedElement(LocateBy.XPath, LocatorValue: "//ul[@class='search-result-kenmerken with-phase']//li[2]");
        AutomatedElement lbl_NoResult = new AutomatedElement(LocateBy.XPath, LocatorValue: "//p[@class='search-no-results-body']");
        
        #endregion

        public void Assert_AddressContains_SearchAddress(string SearchAddress, ElementValidation validation)
        {
            Assert_Element_Contains_Text(span_ApartmentAddress, SearchAddress, SearchAddress, validation);
        }

        public void Assert_ApartmentPriceMatchesSearchCriteria(string MinmumPrice, string MaximumPrice, OfferTypes Offer, ElementValidation validation)
        {
            int ResultInPrice = 0;
            if (Offer == OfferTypes.ForSale)
            {
                string ApartmentPriceText = ReturnElementText(span_ApartmentPrice).Trim(',', 'k', 'k', '.', '.', '€', ' ', ' ');

                ResultInPrice = int.Parse(ApartmentPriceText, NumberStyles.Currency);
            }
                

            else if (Offer == OfferTypes.ForRent)
                ResultInPrice = int.Parse(ReturnElementText(span_ApartmentPrice).Trim('d', 'm', 'n', '/', '.', '€', ' '));


            int MinPrice = int.Parse(MinmumPrice.Trim('€', ','), NumberStyles.Currency); 
            int MaxPrice = 0;

            if (MaximumPrice == "No limit")
                Assert_Number_GreaterThanAnotherNumber(ResultInPrice, MinPrice, "Apartment Price", validation);

            else
            {
                MaxPrice = int.Parse(MaximumPrice.Trim('€', ','), NumberStyles.Currency);
                Assert_Number_BetweenTwoNumbers(ResultInPrice, MinPrice, MaxPrice, "Apartment Price", validation);
            }
                
        }

        public void Assert_NoResultMessage_Displayed(ElementValidation validation)
        {
            FindVisibleElement(20, lbl_NoResult, "No Search Result Message", validation);
        }

    }
}
