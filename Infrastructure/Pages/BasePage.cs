using Infra.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Pages
{
    public abstract class BasePage
    {
        protected static IWebDriver driver;
        protected static string baseUrl = @"https://qa.bglobale.com/GlobalEAdmin";
        protected static LocatorService locatorService;
        protected const int SECOND = 1000;
        protected const int MINUTE = 60 * 1000;
        protected const int HOUR = 60 * MINUTE;
        protected string url;
        protected string checkoutUrl;
        protected string cartUrl;
        protected string merchant;
        public static void InitDriver(string _baseUrl)
        {
            baseUrl = _baseUrl;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--metrics-recording-only");
            options.AddArgument("--force-fieldtrials=SiteIsolationExtensions/Control");
            options.AddArgument("--disable-web-resources");
            options.AddArgument("--no-sandbox");
            options.AddArgument("no-sandbox");
       
            driver = new ChromeDriver(@"E:\chromedriver_win32", options);
           
            locatorService = new LocatorService(driver) ;
        }

        public static void CloseDriver()
        {
            driver.Close();
            driver.Dispose();
        }

        public static void navigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            
        }

        public static void wait(int waitMiliSec = 7000)
        {
            Thread.Sleep(waitMiliSec);
        }

    }
}
