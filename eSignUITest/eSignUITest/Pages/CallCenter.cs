using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace eSignUITest.Pages
{
    class CallCenter : BasePage
    {
        By trackingIdFilterLocator = By.Id("CaseNumber");
        By aplicantNameFilterLocator = By.Id("AppName");
        By searchBtnLocator = By.Name("Search");

        public CallCenter(IWebDriver driver) : base(driver, "Call Center")
        {

        }
    }
}
