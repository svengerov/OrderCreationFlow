using Infra.DataModels;
using Infra.Pages;
using NUnit.Framework;
using static Infra.Pages.MerchantSetPage;
using System;

namespace Tests
{
    [TestFixture]
    public class Tests: TestBase
    {

        //setting configurations before main OrderFlow test
        
        public void Set3dSecure(string country,string merchant, string state3d)    //set automatically 3D Secure version on specific merchant and country
        { 
           Pages.loginPage.NavigateTo();
           Pages.loginPage.Login(user, pass);
           Pages.payMgtPage.NavigateTo();
           Pages.payMgtPage.SelectLeftMenu(PmtMgtLeftMenu._3DSecure);
           Pages.payMgtPage.SelectMerchant(merchant);
           Pages.payMgtPage.SelectCountry(country);
           Pages.payMgtPage.ShowResults();
          Pages.payMgtPage.changeConfig3d(state3d);                      
        }
                
          public void SetMerchantPMControllerSetting(string merchant,string controller)  //set automatically Use Payment controller v1 or v2 by merchant
        {
           Pages.loginPage.NavigateTo();
            Pages.loginPage.Login(user, pass);
            Pages.merchantSetPage.NavigateTo(); 
            Pages.merchantSetPage.SelectMerchant(merchant);
            Pages.merchantSetPage.SelectLeftMenu(MerchSetLeftMenu.Configurations); 
            Pages.merchantSetPage.changeConfigController(controller);           
        }
        
        // main order creation test using credit cards on Anna Scholz - can run only one scenario at the same time

        [Test]

        //    [TestCase("Anna Scholz (101)", "Israel", "Visa","approve", "2",true,"0",false)]                               //HypAPI 3ds2 successful Visa order with challenge, controller v1, no fallback
        //    [TestCase("Anna Scholz (101)", "Israel", "MasterCard", "approve", "2", true, "0",false)]                      //HypAPI 3ds2 successful MC order with challenge, controller v1, no fallback
        //    [TestCase("Anna Scholz (101)", "Israel", "Visa", "approve", "2", false, "0",false)]                           //HypAPI 3ds2 successful Visa order without challenge, controller v1, no fallback 
        //    [TestCase("Anna Scholz (101)", "Israel", "MasterCard", "approve", "2", false, "0",false)]                     //HypAPI 3ds2 successful MC order without challenge, controller v1, no fallback
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "0", false, "0",false)]                          //AdyenAPI no3ds successful Visa order, controller v1
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "0", false, "1", false)]                         //AdyenAPI no3ds successful Visa order, controller v2
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "1", false, "0", false)]                         //AdyenAPI 3ds1 successful Visa order, controller v1
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "1", false, "1", false)]                         //AdyenAPI 3ds1 successful Visa order, controller v2
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "2", false, "0", false)]                         //AdyenAPI 3ds2 successful Visa order, controller v1
        //    [TestCase("Anna Scholz (101", "Austria", "Visa", "approve", "2", false, "1", false)]                          //AdyenAPI 3ds2 successful Visa order, controller v2
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "2", false, "0", true)]                          //AdyenAPI 3ds2 to 3ds1 successful Visa fallback, controller v1
        //    [TestCase("Anna Scholz (101)", "Austria", "Visa", "approve", "2", false, "1", true)]                          //AdyenAPI 3ds2 to 3ds1 successful Visa fallback, controller v2
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "0", false, "0", false)]                         //WorldPayAPI no3ds successful Visa order, controller v1
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "0", false, "1", false)]                         //WorldPayAPI no3ds successful Visa order, controller v2
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "1", false, "0", false)]                         //WorldPayAPI 3ds1 successful Visa order, controller v1
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "1", false, "1", false)]                         //WorldPayAPI 3ds1 successful Visa order, controller v2
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "2", false, "0", false)]                         //WorldPayAPI 3ds2 successful Visa order, controller v1
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "2", false, "1", false)]                         //WorldPayAdyenAPI 3ds2 successful Visa order, controller v2
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "2", false, "0", true)]                          //WorldPayAPI 3ds2 to 3ds1 successful Visa fallback, controller v1
        //    [TestCase("Anna Scholz (101)", "Germany", "Visa", "approve", "2", false, "1", true)]                          //WorldPayAPI 3ds2 to 3ds1 successful Visa fallback, controller v2



        public void TestOrderCreationFlow(string merchant, string country, string paymentMethod,string orderStatus, string state3d,bool isHypChallenge, string paymentController,bool isFallback)
        {
           
            //setting configurations for test case
                 SetMerchantPMControllerSetting(merchant, paymentController);   //Use Payment Controller v1 or v2
                 Set3dSecure(country, merchant, state3d);      // Use 3D Secure version = 0 or 1 or 2
                 OrderFlowAnna(country,paymentMethod, orderStatus,state3d,isHypChallenge,isFallback);   //create order on Anna Scholz
                 
        }
        public void OrderFlowAnna(string country, string paymentMethod, string orderStatus, string state3d, bool isHypChallenge, bool isFallback)
        {
            annaSitePage.NavigateToSiteAndSetCountry(country);
            annaSitePage.ChooseProductOnMerchantSiteAndGoToCheckout();
            annaSitePage.FillCheckoutDetails(countriesDict[country]);

      
            switch (paymentMethod)
            {
                case ("Visa"):
                    {
                        if (country == "Israel")
                        {
                            annaSitePage.PaymentMethodOperationHypVisa(orderStatus, state3d, isHypChallenge, TestBase.paymentsDictCreditCards["CreditCard"]);
                            break;
                        }
                        else
                        {
                            annaSitePage.PaymentMethodOperationVisa(orderStatus, state3d, isFallback, TestBase.paymentsDictCreditCards["CreditCard"]);
                            break;
                        }
                    }
                case ("Mastercard"):
                    {
                        if (country == "Israel")
                        {
                            annaSitePage.PaymentMethodOperationHypMasterCard(orderStatus, state3d, isHypChallenge, paymentsDictCreditCards["CreditCard"]);
                            break;
                        }
                        else
                        {
                            annaSitePage.PaymentMethodOperationMc(orderStatus, state3d, isFallback, paymentsDictCreditCards["CreditCard"]);
                            break;
                        }
                    }         

                default:
                    {

                        break;
                    }
      


            }
      
        }
       
      

    }
}