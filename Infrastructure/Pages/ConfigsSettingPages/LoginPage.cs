using OpenQA.Selenium;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Pages
{
    public class LoginPage : BasePage
    {
        static string url = BasePage.baseUrl;

        By userTextBox => By.CssSelector("[id=\"UserName\"]");
        By passwordTextBox => By.CssSelector("[id=\"Password\"]");
        By loginBtn => By.CssSelector("[id=\"loginBtn\"]");
        

        public void NavigateTo()
        {
             driver.Navigate().GoToUrl(url);
        }

        public void Login(string user, string pass)
        {
            driver.FindElement(userTextBox).SendKeys(user);
            driver.FindElement(passwordTextBox).SendKeys(pass);
            driver.FindElement(loginBtn).Click();

        }
        public void SetConfig()
        { 

        }
    }
}
