using System;
using NUnit;
using OpenQA.Selenium;
using NUnit.Framework;
using Automation.Utilities;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Funda_Assignment
{

    [TestFixture]
    public class Search
    {
        //Define Drivers
        private Driver driver = new Driver(Browser.Chrome, StaticMethods.getCorrectPath(SystemVariables.ChromeDriverPath));

        //Define Pages
        FundaHome_Page FundaHome_Page;
        SearchResults_Page SearchResult_Page;

        #region Declare TestMessages
        private TestMessages Test_Messages = new TestMessages();
        private object thread;
        #endregion

        [OneTimeSetUp]
        public void StartUp()
        {
            //Launch the driver and navigate to system URL
            driver.FreshSetup(SystemVariables.URL);

            //Declare defined pages
            FundaHome_Page = new FundaHome_Page(driver, Test_Messages, this.GetType().Name.ToString());
            SearchResult_Page = new SearchResults_Page(driver, Test_Messages, this.GetType().Name.ToString());
        }

        [SetUp]
        public void Test_SetUp()
        {
            Test_Messages.Clear_Messages();
        }

        public static IEnumerable<TestCaseData> SearchCases_DS()
        {
            yield return new TestCaseData(OfferTypes.ForSale, "Amsterda", "+ 0 km", "€ 75.000", "€ 200.000").SetName("Search In Offer Type For Sale.");

            yield return new TestCaseData(OfferTypes.ForRent, "Rotterda", "+ 0 km", "€ 100", "€ 500").SetName("Search In Offer Type For Rent using suggestion message.");

            yield return new TestCaseData(OfferTypes.NewlyBuilt, "Purmerend", "+ 0 km", null, null).SetName("Search In Offer Type Newly Built.");

            yield return new TestCaseData(OfferTypes.Recreation, "Amsterdam", "+ 0 km", null, null).SetName("Search In Offer Type Recreation.");

            yield return new TestCaseData(OfferTypes.Europe, "Zwitserland", null, null, null).SetName("Search In Offer Type In Europe.");

        }

        [Test]
        [TestCaseSource("SearchCases_DS")]
        public void Search_Tests(OfferTypes OfferType, string Address, string LocationRange, string MinPrice, string MaxPrice)
        {
            //Navigate to home page
            FundaHome_Page.Navigate_ToHomePage();

            //Select Offer Tab
            FundaHome_Page.NavigateTo_OfferType(OfferType);

            Thread.Sleep(2000);

            //Select Country if search in Europe
            if (OfferType == OfferTypes.Europe)
                FundaHome_Page.SelectFrom_CountriesList(Address);

            else
               //Enter Address
               FundaHome_Page.Enter_SearchText(Address);
            
            //Select Location Range
            if (OfferType != OfferTypes.Europe)
                FundaHome_Page.Select_LocationRange(LocationRange);

            else
                FundaHome_Page.Assert_LocationRangeList_NotDisplayed(ElementValidation.Mandatory);

            //Select Minimum Price
            if (OfferType == OfferTypes.ForSale || OfferType == OfferTypes.ForRent)
                FundaHome_Page.Select_MinimumPrice(MinPrice);

            else
                FundaHome_Page.Assert_MinimumPriceList_NotDisplayed(ElementValidation.Non_Mandatory_Till_EOT);

            //Select Maximum Price
            if (OfferType == OfferTypes.ForSale || OfferType == OfferTypes.ForRent)
                FundaHome_Page.Select_MaximumPrice(MaxPrice);

            else
                FundaHome_Page.Assert_MaximumPriceList_NotDisplayed(ElementValidation.Non_Mandatory_Till_EOT);
            
            //Click Search
            FundaHome_Page.Click_Search();

            Thread.Sleep(2000);

            //If the address should be selected from list, select the first option
            if (FundaHome_Page.Is_SuggestionMessage_Visible())
            {
                FundaHome_Page.SelectFrom_AddressList(0);

                //Click Search again
                FundaHome_Page.Click_Search();
            }
                

            //Assert the First Result Card's Address contains the address keyword
            SearchResult_Page.Assert_AddressContains_SearchAddress(Address, ElementValidation.Non_Mandatory_Till_EOT);

            //Assert the First Result Card's Minimum Price is between the entered minimum and max prices
            if(OfferType == OfferTypes.ForSale || OfferType == OfferTypes.ForRent)
                SearchResult_Page.Assert_ApartmentPriceMatchesSearchCriteria(MinPrice, MaxPrice, OfferType, ElementValidation.Non_Mandatory_Till_EOT);

            //End the test
            Test_Messages.End_Test();
        }

        [Test]
        public void View_LastSearch()
        {
            //Navigate to home page
            FundaHome_Page.Navigate_ToHomePage();

            //Click For Rent
            FundaHome_Page.Click_ForRentTab();

            Thread.Sleep(2000);

            //Enter Address
            FundaHome_Page.Enter_SearchText("Amsterdam");

            //Select Min Price
            FundaHome_Page.Select_MinimumPrice("€ 100");

            //Select Max Price
            FundaHome_Page.Select_MaximumPrice("€ 500");

            //Click Search
            FundaHome_Page.Click_Search();

            Thread.Sleep(2000);

            //If the address should be selected from list, select the first option
            if (FundaHome_Page.Is_SuggestionMessage_Visible())
            {
                FundaHome_Page.SelectFrom_AddressList(0);

                //Click Search again
                FundaHome_Page.Click_Search();
            }

            //Navigate to home page
            FundaHome_Page.Navigate_ToHomePage();

            //Ensure last search query is correct
            List<string> SearchCriteria = new List<string>();
            SearchCriteria.Add("Amsterdam");
            SearchCriteria.Add("€ 100");
            SearchCriteria.Add("€ 500");

            FundaHome_Page.Assert_LastQueryCotainsCorrectCriteria(SearchCriteria, ElementValidation.Non_Mandatory_Till_EOT);

            //Click Last Search Query
            FundaHome_Page.Click_LastSearchQuery();

            //Assert the First Result Card's Address contains the address keyword
            SearchResult_Page.Assert_AddressContains_SearchAddress("Amsterdam", ElementValidation.Non_Mandatory_Till_EOT);

            //Assert the First Result Card's Minimum Price is between the entered minimum and max prices
            SearchResult_Page.Assert_ApartmentPriceMatchesSearchCriteria("€ 100", "€ 500", OfferTypes.ForRent, ElementValidation.Non_Mandatory_Till_EOT);

            //End the test
            Test_Messages.End_Test();
        }

        [Test]
        public void Enter_OtherPrices()
        {
            //Navigate to home page
            FundaHome_Page.Navigate_ToHomePage();

            //Click For Rent
            FundaHome_Page.Click_ForRentTab();

            Thread.Sleep(2000);

            //Enter Address
            FundaHome_Page.Enter_SearchText("Amsterdam");

            //Select Min Price
            FundaHome_Page.Select_MinimumPrice("other");

            //Enter Min Price
            FundaHome_Page.Enter_CustomMinPrice("1500", OfferTypes.ForRent);

            //Select Max Price
            FundaHome_Page.Select_MaximumPrice("other");

            //Enter Max Price
            FundaHome_Page.Enter_CustomMaxPrice("5500");

            //Click Search
            FundaHome_Page.Click_Search();
            
            Thread.Sleep(2000);

            //If the address should be selected from list, select the first option
            if (FundaHome_Page.Is_SuggestionMessage_Visible())
            {
                FundaHome_Page.SelectFrom_AddressList(0);

                //Click Search again
                FundaHome_Page.Click_Search();
            }

            //Assert the First Result Card's Address contains the address keyword
            SearchResult_Page.Assert_AddressContains_SearchAddress("Amsterdam", ElementValidation.Non_Mandatory_Till_EOT);

            //Assert the First Result Card's Minimum Price is between the entered minimum and max prices
            SearchResult_Page.Assert_ApartmentPriceMatchesSearchCriteria("€ 1500", "€ 5500", OfferTypes.ForRent, ElementValidation.Non_Mandatory_Till_EOT);

            //End the test
            Test_Messages.End_Test();
        }

        [Test]
        public void Select_Invalid_Prices()
        {
            //Navigate to home page
            FundaHome_Page.Navigate_ToHomePage();

            //Click For Rent
            FundaHome_Page.Click_ForRentTab();

            Thread.Sleep(2000);

            //Enter Address
            FundaHome_Page.Enter_SearchText("Amsterdam");
            
            //Select Max Price
            FundaHome_Page.Select_MaximumPrice("€ 100");

            //Select Min Price
            FundaHome_Page.Select_MinimumPrice("€ 500");
            
            //Check red selected max price as invalid sign
            FundaHome_Page.Assert_MaxPrice_Color_IsCorrect(ElementValidation.Non_Mandatory_Till_EOT);

            //Click Search
            FundaHome_Page.Click_Search();
            
            Thread.Sleep(2000);

            //If the address should be selected from list, select the first option
            if (FundaHome_Page.Is_SuggestionMessage_Visible())
            {
                FundaHome_Page.SelectFrom_AddressList(0);

                //Click Search again
                FundaHome_Page.Click_Search();
            }

            //Assert that no result is displayed
            SearchResult_Page.Assert_NoResultMessage_Displayed(ElementValidation.Non_Mandatory_Till_EOT);
            
            //End the test
            Test_Messages.End_Test();
        }

        [TearDown]
        public void TearDownTest()
        {
            
        }
       
        
        [OneTimeTearDown]
        public void TearDownFixture()
        {
            driver.TeardownDriver();
        }
    }
}
