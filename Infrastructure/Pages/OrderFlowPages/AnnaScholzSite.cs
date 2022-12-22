using Infra.DataModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Pages.OrderFlowPages
{
    public class AnnaScholzSite : BasePage
    {


        public AnnaScholzSite()
        {
            url = "http://globale:globalelocal@anna.bglobale.de/online-store/tops/chiara-sweatshirt-botanical.html";
            checkoutUrl = "https://globale:globalelocal@anna.bglobale.de/onestepcheckout/";
            cartUrl = "http://anna.bglobale.de/checkout/cart/";
            merchant = "Anna Scholz (101)";
        }

        public void ChooseProductOnMerchantSiteAndGoToCheckout()
        {
            IWebElement product = driver.FindElement(By.Id("attribute492"));
            product.Click();
            product.SendKeys(Keys.ArrowDown + Keys.Enter);
            driver.FindElement(By.CssSelector("#product-addtocart-button > span > span")).Click();
            driver.Navigate().GoToUrl(cartUrl);  

            IWebElement quantity = driver.FindElement(By.CssSelector("#shopping-cart-table > tbody > tr > td:nth-child(6) > input"));
            quantity.Clear();
            quantity.SendKeys("2");     
            driver.FindElement(By.Name("update_cart_action")).Click();        

            driver.FindElement(By.CssSelector("body > div.wrapper > div > div.main-container.col1-layout > div > div > div > div.cart-collaterals > div > div.col-2 > div > ul > li > button")).Click();      
            driver.Navigate().GoToUrl(checkoutUrl);        
        }

        public void NavigateToSiteAndSetCountry(string country)
        {
            driver.Navigate().GoToUrl(url);
            locatorService.waitForElement((By.Id("globale_popup")));
            IWebElement annaPopup = driver.FindElement(By.Id("globale_popup"));            
            annaPopup.FindElement(By.CssSelector("#globale_popup > div > div > div.glControls > a")).Click();
            IWebElement countries_dropdown = driver.FindElement(By.Id("gle_selectedCountry"));
            locatorService.waitForElement(By.Id("gle_selectedCountry"));
            countries_dropdown.SendKeys(country);
            driver.FindElement(By.CssSelector("#globale_csc_popup > div > div > div > div > div.glControls > input")).Click();         
        }

        public void FillCheckoutDetails(CheckoutEntry country)
        {
            driver.SwitchTo().Frame("Intrnl_CO_Container");
            driver.FindElement(By.CssSelector("#BillingSalutationID")).SendKeys(Keys.Down + Keys.Enter);
            driver.FindElement(By.Id("CheckoutData_BillingFirstName")).SendKeys(country.First);
            driver.FindElement(By.Id("shippingDefault")).Click();
            driver.FindElement(By.Id("CheckoutData_BillingLastName")).SendKeys(country.Last);
            driver.FindElement(By.Id("CheckoutData_Email")).SendKeys(country.email);
            driver.FindElement(By.Id("CheckoutData_BillingAddress1")).SendKeys(country.streetAddress);
            driver.FindElement(By.Id("BillingCity")).SendKeys(country.city);
            driver.FindElement(By.Id("BillingZIP")).SendKeys(country.postIndex);
            driver.FindElement(By.Id("CheckoutData_BillingPhone")).SendKeys(country.phone);
        }

        public void PaymentMethodOperationHypVisa( string orderStatus, string state3d, bool isChallenge, PaymentCreditCardsEntry paymentCredit)
        {
            
            
            By cardNum = By.CssSelector("#cardNum");
            By expMonth = By.Id("cardExpiryMonth");
            By expYear = By.Id("cardExpiryYear");
            By CVV = By.Id("cvdNumber");
            By Pay = By.CssSelector("#btnPay");

            BasePage.wait();
            driver.FindElement(By.CssSelector("#pmContainer > div.pm_box.active > span")).Click();
            BasePage.wait();
            driver.SwitchTo().Frame("secureWindow");   //entering credit card payment details
            BasePage.wait();


            switch (orderStatus, isChallenge, state3d)
            {

                case ("approve", true, "2"):
                    {
                        driver.FindElement(cardNum).SendKeys(paymentCredit.numberVisa3dsAcceptHyp);
                        driver.FindElement(expMonth).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                        driver.FindElement(expYear).SendKeys(paymentCredit.expireYear + Keys.Enter);
                        driver.FindElement(CVV).SendKeys(paymentCredit.cvvChallenge);
                        driver.SwitchTo().ParentFrame();
                        driver.FindElement(Pay).Click();
                        BasePage.wait();
                        BasePage.wait();
                        

                        driver.SwitchTo().Frame("ifrm");  //3DS Hyp authorization window
                        driver.FindElement(By.Id("btn1")).Click();  //approval
                        BasePage.wait();
                        BasePage.wait();
                        driver.FindElement(By.Id("btn5")).Click();  //complete order
                        break;
                    }
               
                             
                case ("approve", false, "2"):
                    {
                        driver.FindElement(cardNum).SendKeys(paymentCredit.numberVisa3dsAcceptHyp);                        
                        driver.FindElement(expMonth).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                        driver.FindElement(expYear).SendKeys(paymentCredit.expireYear + Keys.Enter);
                        driver.FindElement(CVV).SendKeys(paymentCredit.cvvHypFrictionless);
                        driver.SwitchTo().ParentFrame();
                        driver.FindElement(Pay).Click();
                        BasePage.wait();
                        BasePage.wait();
                        
                        break;
                    }
               
                case ("decline", true, "2"):
                case ("decline", false, "2"):
                    {
                        driver.FindElement(cardNum).SendKeys(paymentCredit.numberVisa3dsAcceptHyp);                        
                        driver.FindElement(expMonth).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                        driver.FindElement(expYear).SendKeys(paymentCredit.expireYear + Keys.Enter);
                        driver.FindElement(CVV).SendKeys(paymentCredit.cvvDecline);
                        driver.SwitchTo().ParentFrame();
                        driver.FindElement(Pay).Click();
                        BasePage.wait();
                        break;
                    }
               
                default:
                    {
                        break;
                    }
            }
        }

   

        public void PaymentMethodOperationHypMasterCard(string orderStatus, string state3d, bool isHypChallenge, PaymentCreditCardsEntry paymentCreditMc)
        {
            {
                By cardNum = By.CssSelector("#cardNum");
                By expMonth = By.Id("cardExpiryMonth");
                By expYear = By.Id("cardExpiryYear");
                By CVV = By.Id("cvdNumber");
                By Pay = By.CssSelector("#btnPay");

                BasePage.wait();
                driver.FindElement(By.CssSelector("#pmContainer > div.pm_box.active > span")).Click();
                BasePage.wait();
                driver.SwitchTo().Frame("secureWindow");   //entering credit card payment details
                BasePage.wait();


                switch (orderStatus, isHypChallenge, state3d)
                {

               
                    case ("approve", true, "2"):
                        {
                            driver.FindElement(cardNum).SendKeys(paymentCreditMc.numberMc3dsHyp);
                            driver.FindElement(expMonth).SendKeys(paymentCreditMc.expireMonth + Keys.Enter);
                            driver.FindElement(expYear).SendKeys(paymentCreditMc.expireYear + Keys.Enter);
                            driver.FindElement(CVV).SendKeys(paymentCreditMc.cvvChallenge);
                            driver.SwitchTo().ParentFrame();
                            driver.FindElement(Pay).Click();
                            BasePage.wait();
                            BasePage.wait();
                            BasePage.wait();
                            BasePage.wait();
                            driver.SwitchTo().Frame("ifrm");  //3DS Hyp authorization window
                            driver.FindElement(By.Id("btn1")).Click();
                            BasePage.wait();
                            BasePage.wait();
                            driver.FindElement(By.Id("btn5")).Click();
                            break;
                        }                    
                    
                    case ("approve", false, "2"):
                        {

                            driver.FindElement(cardNum).SendKeys(paymentCreditMc.numberMc3dsHyp);
                            BasePage.wait();
                            driver.FindElement(expMonth).SendKeys(paymentCreditMc.expireMonth + Keys.Enter);
                            driver.FindElement(expYear).SendKeys(paymentCreditMc.expireYear + Keys.Enter);
                            driver.FindElement(CVV).SendKeys(paymentCreditMc.cvvHypFrictionless);
                            driver.SwitchTo().ParentFrame();
                            driver.FindElement(Pay).Click();
                            BasePage.wait();
                            BasePage.wait();
                            BasePage.wait();
                            break;
                        }

                    case ("decline", true, "2"):
                    case ("decline", false, "2"):
                        {
                            driver.FindElement(cardNum).SendKeys(paymentCreditMc.numberMc3dsHyp);
                            BasePage.wait();
                            driver.FindElement(expMonth).SendKeys(paymentCreditMc.expireMonth + Keys.Enter);
                            driver.FindElement(expYear).SendKeys(paymentCreditMc.expireYear + Keys.Enter);
                            driver.FindElement(CVV).SendKeys(paymentCreditMc.cvvDecline);
                            driver.SwitchTo().ParentFrame();
                            driver.FindElement(Pay).Click();
                            BasePage.wait();
                            break;
                        }
                    
                    default:
                        {
                            break;
                        }
                }
            }
        }

      public void PaymentMethodOperationMc(string orderstatus, string state3d, bool isFallback, PaymentCreditCardsEntry paymentCreditMc)
        {
            {

                driver.FindElement(By.Id("cardNum")).SendKeys(paymentCreditMc.numberMcNo3ds);
            }

        }

        public void PaymentMethodOperationVisa(string orderstatus, string state3d, bool isFallback, PaymentCreditCardsEntry paymentCredit)
        {
            if (state3d == "0")
            {
                driver.FindElement(By.CssSelector("#pmContainer > div.pm_box.active > span")).Click();
                BasePage.wait();
                driver.SwitchTo().Frame("secureWindow");
                BasePage.wait();
                driver.FindElement(By.CssSelector("#cardNum")).SendKeys(paymentCredit.numberVisaNo3ds);
                BasePage.wait();
                driver.FindElement(By.Id("cardExpiryMonth")).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                driver.FindElement(By.Id("cardExpiryYear")).SendKeys(paymentCredit.expireYear + Keys.Enter);
                if (orderstatus == "approve")
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvAccept);
                    BasePage.wait();
                }
                else
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvDecline);
                    BasePage.wait();
                }
                driver.SwitchTo().ParentFrame();
                driver.FindElement(By.CssSelector("#btnPay")).Click();

            }




            else if (state3d == "1")                           //VISA 3DS1 case with popup
            {
                driver.FindElement(By.CssSelector("#pmContainer > div.pm_box.active > span")).Click();
                BasePage.wait();
                driver.SwitchTo().Frame("secureWindow");
                BasePage.wait();
                driver.FindElement(By.CssSelector("#cardNum")).SendKeys(paymentCredit.number3dsAcceptTwo);
                BasePage.wait();
                driver.FindElement(By.Id("cardExpiryMonth")).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                driver.FindElement(By.Id("cardExpiryYear")).SendKeys(paymentCredit.expireYear + Keys.Enter);
                if (orderstatus == "approve")
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvAccept);
                    BasePage.wait();
                }
                else
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvDecline);
                    BasePage.wait();
                }
                driver.SwitchTo().ParentFrame();
                driver.FindElement(By.CssSelector("#btnPay")).Click();
                if (orderstatus == "approve")    //continue to popup
                {
                    BasePage.wait();
                    driver.SwitchTo().Frame("tdsFrame");
                    BasePage.wait();
                    driver.SwitchTo().Frame("threeDSIframe");

                    IWebElement PassField = driver.FindElement(By.CssSelector("body>div.container>form>label>div:nth-child(2)>input.input-field"));
                    BasePage.wait();
                    PassField.SendKeys("password");
                    driver.FindElement(By.Id("buttonSubmit")).Click();
                    BasePage.wait();
                }
            }

            else if ((state3d == "2") && (isFallback == false))                             //VISA 3DS2 case with full redirect, no fallback to 3DS1
            {
                driver.FindElement(By.CssSelector("#pmContainer > div.pm_box.active > span")).Click();         
                driver.SwitchTo().Frame("secureWindow");       
                driver.FindElement(By.CssSelector("#cardNum")).SendKeys(paymentCredit.number3dsAcceptTwo);       
                driver.FindElement(By.Id("cardExpiryMonth")).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                driver.FindElement(By.Id("cardExpiryYear")).SendKeys(paymentCredit.expireYear + Keys.Enter);
                if (orderstatus == "approve")
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvAccept);
                   
                }
                else
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvDecline);
                  
                }
                driver.SwitchTo().ParentFrame();
                driver.FindElement(By.CssSelector("#btnPay")).Click();
                if (orderstatus == "approve")    //continue to full redirect
                {
                    BasePage.wait();    //Waiting for frame to load
                    driver.SwitchTo().Frame("threeDSIframe");

                    driver.FindElement(By.CssSelector("body>div.container>form>label>div:nth-child(2)>input.input-field")).SendKeys("password");
                    BasePage.wait();
                    driver.FindElement(By.Id("buttonSubmit")).Click();
                    BasePage.wait();

                }
            }

            else if ((state3d == "2") && (isFallback == true))                             //VISA 3DS2 case with fallback to 3DS1
            {
                driver.FindElement(By.CssSelector("#pmContainer > div.pm_box.active > span")).Click();
                BasePage.wait();
                driver.SwitchTo().Frame("secureWindow");
                BasePage.wait();
                driver.FindElement(By.CssSelector("#cardNum")).SendKeys(paymentCredit.numberVisa3dsFallback);
                BasePage.wait();
                driver.FindElement(By.Id("cardExpiryMonth")).SendKeys(paymentCredit.expireMonth + Keys.Enter);
                driver.FindElement(By.Id("cardExpiryYear")).SendKeys(paymentCredit.expireYear + Keys.Enter);
                if (orderstatus == "approve")
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvAccept);
                    BasePage.wait();
                }
                else
                {
                    driver.FindElement(By.Id("cvdNumber")).SendKeys(paymentCredit.cvvDecline);
                    BasePage.wait();
                }
                driver.SwitchTo().ParentFrame();
                driver.FindElement(By.CssSelector("#btnPay")).Click();
                if (orderstatus == "approve")    //continue to fallback
                {
                    driver.SwitchTo().ParentFrame();
                    BasePage.wait();
                    driver.FindElement(By.Id("username")).SendKeys("user");
                    driver.FindElement(By.Id("password")).SendKeys("password");
                    driver.FindElement(By.CssSelector("#subcontent>form>table>tbody>tr:nth-child(7)>td>input")).Click();
                    BasePage.wait();
                }
            }

        }
    }
}
    
