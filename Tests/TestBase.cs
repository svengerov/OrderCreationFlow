using Infra.DataModels;
using Infra.Helpers;
using Infra.Pages;
using Infra.Pages.OrderFlowPages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TestBase
    {
        protected static string baseUrl = @"https://qa.bglobale.com/GlobalEAdmin";
        protected string user = "userGlobalE@global-e.com";
        protected string pass = "Admin password";
        protected static Dictionary<string, CheckoutEntry> countriesDict;
        protected static Dictionary<string, PaymentCreditCardsEntry> paymentsDictCreditCards;
        protected static Dictionary<string, PaymentAlternativesEntry> paymentsDictAlternatives;

        protected AnnaScholzSite annaSitePage => Pages.AnnaSitePage;
      
     
        
        [OneTimeSetUp]
        public static void oneTimeSetUp()
        {
            BasePage.InitDriver(baseUrl);
            string dataFile = @"pathToAddressData.json"; 
            countriesDict = SerializerHelper.DeserializeJsonFile<Dictionary<string, CheckoutEntry>>(dataFile);
            string dataFilePaymentsCreditCards = @"pathToPaymentDataCreditCards.json";
            paymentsDictCreditCards = SerializerHelper.DeserializeJsonFile<Dictionary<string, PaymentCreditCardsEntry>>(dataFilePaymentsCreditCards);
            string dataFilePaymentsAlternatives = @"pathToPaymentDataAlternatives.json";
             paymentsDictAlternatives = SerializerHelper.DeserializeJsonFile<Dictionary<string, PaymentAlternativesEntry>>(dataFilePaymentsAlternatives);
         

        }

        [OneTimeTearDown]
        public static void oneTimeTearDown()
        {
                    BasePage.CloseDriver();
        }
        [SetUp]
        public void setup()
        {

        }

        [TearDown]
        public void teardown()
        {

        }
    }
}
