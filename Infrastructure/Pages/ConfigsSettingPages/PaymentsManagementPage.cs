using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace Infra.Pages
{
    public class PaymentsManagementPage: BasePage
    {
        string url = @"https://qa.bglobale.com/GlobalEAdmin/PaymentsAdmin/PaymentsManagement";

        By pmtGatewaysLoc = By.XPath("//div[contains(text(),'Payment Gateways')]");
        By _3dSec = By.XPath("//div[contains(text(),'3D Secure')]");
        By selectors = By.CssSelector("[class=\"searchCol searchField\"] div button span");
        public IWebElement controller;

        By showBtn => By.CssSelector("[id=\"btnShowIdConfiguration\"]");

        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(url);
        }
        public void SelectLeftMenu(PmtMgtLeftMenu menu)
        {
            switch (menu)
            {
                case PmtMgtLeftMenu.PaymentGateways:
                    driver.FindElements(pmtGatewaysLoc).First().Click();
                    break;
                case PmtMgtLeftMenu._3DSecure:
                    driver.FindElements(_3dSec).First().Click();
                    
                    break;
                case PmtMgtLeftMenu.MCC:
                    break;
                case PmtMgtLeftMenu.Klarna:
                    break;
                default:
                    break;
            }
        }

        public void SelectCountryBySearch(string v)
        {
            
        }

        public void UnselectAllMerchants()
        {
            
            while(true)
            {
                try
                {
                    locatorService.waitForElement(selectors).Click();
                    break;
                }
                catch (Exception)
                {
                }

            }
   
            driver.FindElement(By.CssSelector("[name=\"selectAllChosenMerchantIds\"]")).Click();
        }

        public void UnselectAllCountries()
        {

            while (true)
            {
                try
                {
                    driver.FindElements(selectors).Skip ( 1).First().Click() ;

                    break;
                }
                catch (Exception)
                {
                }

            }

            driver.FindElement(By.CssSelector("[name=\"selectAllChosenCountryIds\"]")).Click();
        }
    
        public void ShowResults()
        {
            driver.FindElement(By.CssSelector("#btnShowTdConfiguration")).Click();

        }

        public void changeConfig3d(string state3d_needed)
        {
            locatorService.waitForElement(By.CssSelector("#tdTableData > div.row.tdTableRow > div:nth-child(6) > div.btnEdit"));
            By editButton = By.CssSelector("#tdTableData > div.row.tdTableRow > div:nth-child(6) > div.btnEdit");
            By threshold = By.CssSelector("#tdTableData > div.row.tdTableRow > div:nth-child(4) > input");

            driver.FindElement(editButton).Click();
            controller = driver.FindElement(By.ClassName("tdSecuredVersionDropdown"));
            var select3dConfig = new SelectElement(controller);
            var currentState = controller.GetAttribute("value");
            
            if ((currentState != state3d_needed)&&((currentState=="1")||(currentState=="2")))     //first case of change configuration
            {
                if (state3d_needed == "1") { select3dConfig.SelectByText("3DS1"); }
                else if (state3d_needed == "2") { select3dConfig.SelectByText("3DS2"); }
                else
                {
                    driver.FindElement(threshold).Clear();
                }
                    SaveConfiguration();
            }
            else if(currentState != state3d_needed)   //second case of change configuration
            {
                
                if (state3d_needed == "1") 
                {
                    driver.FindElement(threshold).SendKeys("0");
                    select3dConfig.SelectByText("3DS1");
                    SaveConfiguration();
                }
                else if (state3d_needed == "2") 
                {
                    driver.FindElement(threshold).SendKeys("0");
                    select3dConfig.SelectByText("3DS2");
                    SaveConfiguration();
                }

            }
            
           
        }

        public void SaveConfiguration()
        {
            
            {
                driver.FindElement(By.Id("btnSaveTdConfiguration")).Click();
                
            }
        }

        public void SelectMerchant(string merchant)
        {
            UnselectAllMerchants();
           var label = locatorService.findByTagContainingText("label",merchant);
            label.Click();
            
            
        }

        public void SelectCountry(string country)
        {
            UnselectAllCountries();
            var matchingLabels = driver.FindElements(By.XPath($"//label[contains(text(), '{country}')]"));
            matchingLabels.FirstOrDefault(l => l.Text == country).Click();


        }

    }

    public enum PmtMgtLeftMenu
    {
        PaymentGateways,
        _3DSecure,
        MCC,
        Klarna
    }

  
}
