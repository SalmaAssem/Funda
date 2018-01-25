using System;
using Automation.Utilities;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;

namespace Funda_Assignment
{
    public enum OfferTypes
    {
        ForSale, ForRent, NewlyBuilt, Recreation, Europe
    };

    public class FundaHome_Page : Utilities
    {
        public FundaHome_Page(Driver driver, TestMessages testmesssages, string ClassName)
            : base(driver, testmesssages, ClassName)
        {
            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(driver.driver, this);
        }


        #region PageObjects

        AutomatedElement img_Logo = new AutomatedElement(LocateBy.XPath, LocatorValue: "//img[@alt='funda']");
        AutomatedElement tab_ForSale = new AutomatedElement(LocateBy.XPath, LocatorValue: "//a[contains(@href,'/koop/') and contains(@class, 'search')]");
        AutomatedElement tab_ForRent = new AutomatedElement(LocateBy.XPath, LocatorValue: "//a[contains(@href,'/huur/') and contains(@class, 'search')]");
        AutomatedElement tab_NewlyBuilt = new AutomatedElement(LocateBy.XPath, LocatorValue: "//a[contains(@href,'/nieuwbouw/') and contains(@class, 'search')]");
        AutomatedElement tab_Recreation = new AutomatedElement(LocateBy.XPath, LocatorValue: "//a[contains(@href,'/recreatie/') and contains(@class, 'search')]");
        AutomatedElement tab_Europe = new AutomatedElement(LocateBy.XPath, LocatorValue: "//a[contains(@href,'/europe/') and contains(@class, 'search')]");
        AutomatedElement txt_Address = new AutomatedElement(LocateBy.Id, LocatorValue: "autocomplete-input");
        AutomatedElement lst_Address = new AutomatedElement(LocateBy.Id, LocatorValue: "autocomplete-list");
        AutomatedElement select_LocationRange = new AutomatedElement(LocateBy.Id, LocatorValue: "Straal");
        AutomatedElement select_MinPrice = new AutomatedElement(LocateBy.XPath, LocatorValue: "//select[contains(@id, 'jsvan')]");
        AutomatedElement select_MaxPrice = new AutomatedElement(LocateBy.XPath, LocatorValue: "//select[contains(@id, 'jstot')]");
        AutomatedElement button_Search = new AutomatedElement(LocateBy.XPath, LocatorValue: "//button[@class = 'button-primary-alternative' and @type = 'submit']");
        AutomatedElement lnk_LastSearch = new AutomatedElement(LocateBy.XPath, LocatorValue: "//p[@class = 'search-block__last-query']/a");
        AutomatedElement lst_Countries = new AutomatedElement(LocateBy.XPath, LocatorValue: "//div[@class = 'search-dropdown custom-select']");
        AutomatedElement lbl_SuggestionMessage = new AutomatedElement(LocateBy.XPath, LocatorValue: "//h4[@class='autocomplete-suggestion-message']");
        AutomatedElement span_FirstOption = new AutomatedElement(LocateBy.Id, LocatorValue: "autocomplete-list-option0");
        AutomatedElement lnk_LastQuery = new AutomatedElement(LocateBy.XPath, LocatorValue: "//p[@class='search-block__last-query']/a");
        AutomatedElement txt_MinPrice = new AutomatedElement(LocateBy.XPath, LocatorValue: "//input[contains(@name, 'prijsVan')]");
        AutomatedElement txt_MaxPrice = new AutomatedElement(LocateBy.XPath, LocatorValue: "//input[contains(@name, 'prijsTot')]");
        
        #endregion

        public async Task Navigate_ToHomePage()
        {
            FindVisibleElement(20, img_Logo, "Home Logo", ElementValidation.Mandatory);

            ClickElement(img_Logo, "Home Logo", ElementValidation.Mandatory);
        }

        public void Click_ForSaleTab()
        {
            FindVisibleElement(20, tab_ForSale, "For Sale Tab", ElementValidation.Mandatory);

            ClickElement(tab_ForSale, "For Sale Tab", ElementValidation.Mandatory);
        }

        public void Click_ForRentTab()
        {
            FindVisibleElement(20, tab_ForRent, "For Rent Tab", ElementValidation.Mandatory);

            ClickElement(tab_ForRent, "For Rent Tab", ElementValidation.Mandatory);
        }

        public void Click_NewlyBuiltTab()
        {
            FindVisibleElement(20, tab_NewlyBuilt, "NewlyBuilt Tab", ElementValidation.Mandatory);

            ClickElement(tab_NewlyBuilt, "NewlyBuilt Tab", ElementValidation.Mandatory);
        }

        public void Click_RecreatieTab()
        {
            FindVisibleElement(20, tab_Recreation, "Recreation Tab", ElementValidation.Mandatory);

            ClickElement(tab_Recreation, "Recreation Tab", ElementValidation.Mandatory);
        }

        public void Click_EuropeTab()
        {
            FindVisibleElement(20, tab_Europe, "Europe Tab", ElementValidation.Mandatory);

            ClickElement(tab_Europe, "Europe Tab", ElementValidation.Mandatory);
        }

        public void Enter_SearchText(string KeyWord)
        {
            FindVisibleElement(20, txt_Address, "Search text area", ElementValidation.Mandatory);

            EnterText_ToElement(txt_Address, KeyWord, "Search text area", ElementValidation.Mandatory);

            Thread.Sleep(2000);

            Enter_KeyBoardTab();

            Thread.Sleep(1000);
        }

        public bool Is_SuggestionMessage_Visible()
        {
            if (IsElementVisible(10, lbl_SuggestionMessage))
                return true;

            else
                return false;
        }

        public void SelectFrom_AddressList(int OptionNumber)
        {
            AutomatedElement span_Option = new AutomatedElement(LocateBy.Id, LocatorValue: "autocomplete-list-option" + OptionNumber);
            ClickElement(span_Option, " Option of address list number " + OptionNumber, ElementValidation.Mandatory);
        }

        public void SelectFrom_CountriesList(string Country)
        {
            FindVisibleElement(20, lst_Countries, "Countries List", ElementValidation.Mandatory);

            ClickElement(lst_Countries, "Countries List", ElementValidation.Mandatory);

            AutomatedElement span_country = new AutomatedElement(LocateBy.XPath, LocatorValue: "//span[text() = '" + Country + "']");

            ClickElement(span_country, "Country " + Country, ElementValidation.Mandatory);
        }

        public void Select_LocationRange(string Range)
        {
            FindVisibleElement(20, select_LocationRange, "Location Range List", ElementValidation.Mandatory);

            SelectFromList(select_LocationRange, Range, ElementValidation.Mandatory);
        }

        public void Assert_LocationRangeList_NotDisplayed(ElementValidation validation)
        {
            Assert_Element_NotFound(2, select_LocationRange, "Location Range List", validation, validation);
        }

        public void Assert_MinimumPriceList_NotDisplayed(ElementValidation validation)
        {
            Assert_Element_NotFound(2, select_MinPrice, "Minimum Price List", validation, validation);
        }

        public void Assert_MaximumPriceList_NotDisplayed(ElementValidation validation)
        {
            Assert_Element_NotFound(2, select_MaxPrice, "Maximum Price List", validation, validation);
        }

        public void Select_MinimumPrice(string MinimumPrice)
        {
            FindVisibleElement(20, select_MinPrice, "Minimum Price List", ElementValidation.Mandatory);

            if (MinimumPrice == "other")
                SelectFromList_ByIndex(select_MinPrice, 0, ElementValidation.Mandatory);

            else
                SelectFromList(select_MinPrice, MinimumPrice, ElementValidation.Mandatory);
        }
        
        public void Select_MaximumPrice(string MaximumPrice)
        {
            FindVisibleElement(20, select_MaxPrice, "Maximum Price List", ElementValidation.Mandatory);

            if (MaximumPrice == "other")
                SelectFromList_ByIndex(select_MaxPrice, 0, ElementValidation.Mandatory);

            else
                SelectFromList(select_MaxPrice, MaximumPrice, ElementValidation.Mandatory);
        }

        public void Enter_CustomMinPrice(string Price, OfferTypes OfferType)
        {
            
            //Enter Price
            EnterText_UsingJS(txt_MinPrice, Price, "Min Price text area", ElementValidation.Mandatory);
        }

        public void Enter_CustomMaxPrice(string Price)
        {

            //Enter Price
            EnterText_UsingJS(txt_MaxPrice, Price, "Max Price text area", ElementValidation.Mandatory);
        }


        public void Click_Search()
        {
            FindVisibleElement(20, button_Search, "Search Button", ElementValidation.Mandatory);

            ClickElement(button_Search, "Search Button", ElementValidation.Mandatory);
        }

        public void Assert_LastQueryCotainsCorrectCriteria(List<string> CriteriaList, ElementValidation validation)
        {
            for (int i = 0; i < CriteriaList.Count; i++)
                Assert_Element_Contains_Text(lnk_LastQuery, CriteriaList[i], "Last Search Query", validation);
        }

        public void Click_LastSearchQuery()
        {
            FindVisibleElement(20, lnk_LastQuery, "Last Search Query", ElementValidation.Mandatory);

            ClickElement(lnk_LastQuery, "Last Search Query", ElementValidation.Mandatory);
        }

        public void Assert_MaxPrice_Color_IsCorrect(ElementValidation validation)
        {
            ColorObject MaxPriceColor = new ColorObject(240, 60, 48);
            ElementColor_IsCorrect(select_MaxPrice, MaxPriceColor.r, MaxPriceColor.g, MaxPriceColor.b, "Max Price Color", validation);
        }

        public void NavigateTo_OfferType(OfferTypes OfferType)
        {
            if (OfferType == OfferTypes.ForSale)
                Click_ForSaleTab();

            else if (OfferType == OfferTypes.ForRent)
                Click_ForRentTab();

            else if (OfferType == OfferTypes.NewlyBuilt)
                Click_NewlyBuiltTab();

            else if (OfferType == OfferTypes.Recreation)
                Click_RecreatieTab();

            else if (OfferType == OfferTypes.Europe)
                Click_EuropeTab();
            
        }
    }
}
