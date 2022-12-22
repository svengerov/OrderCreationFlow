using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Pages
{
    public class MerchantSetPage : BasePage
    {
        string url = @"https://qa.bglobale.com/GlobalEAdmin/Merchants/Management";
        By MerchantSetConfig = By.XPath("//div[contains(text(),'Configurations')]");
        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(url);
        }
        public void SelectLeftMenu(MerchSetLeftMenu menu)
        {
            switch (menu)
            {
                case MerchSetLeftMenu.Configurations:
                    {

                        driver.FindElements(MerchantSetConfig).First().Click();
                        break;
                    }
                   default:
                    break;
            }
        }

      

    public enum MerchSetLeftMenu
    {
        Summary,
        General_Info,
        Contact_Details,
        Configurations,
        Countries,
        Logistics,
        Shipping,
        Flat_Shipping
    }

        public void SelectMerchant(string merchant)
        {
            driver.FindElement(By.CssSelector("#SelectedMerchantId_chosen > a > span")).Click();
            driver.FindElement(By.CssSelector("#SelectedMerchantId_chosen > div > div > input[type=text]")).SendKeys(merchant+ Keys.Enter);
        }

        public void changeConfigController(string controller)
        {
            locatorService.waitForElement(By.CssSelector("#UsePaymentsV2Controller")).Click();
         
            IWebElement controllerField = driver.FindElement(By.CssSelector("#UsePaymentsV2Controller"));
          var currentValue =   controllerField.GetAttribute("value");
            
 
            var selectPMControllerConfig = new SelectElement(controllerField);
            
            if ((controller == "1")&&(currentValue == "false"))    //set property to true when current property is false
            {
                selectPMControllerConfig.SelectByValue("true");
                SaveConfiguration();
            }
            else if ((controller == "0")&&(currentValue == "true"))  //set property to false when current property is true
            {
                selectPMControllerConfig.SelectByValue("false");
                SaveConfiguration();
            }
            BasePage.wait();
           
        }

        public void SaveConfiguration()
        {
            driver.FindElement(By.CssSelector("#btnSaveChanges")).Click();
            BasePage.wait();
            driver.FindElement(By.CssSelector("#btnApply")).Click();
        }
    }

    }

