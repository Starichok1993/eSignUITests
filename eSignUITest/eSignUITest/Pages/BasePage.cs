using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace eSignUITest.Pages
{
    public class BasePage
    {
        protected IWebDriver _driver;
        protected string _title;

        public BasePage(IWebDriver driver, string title)
        {
            _driver = driver;
            _title = title;

            if (!_title.Equals(driver.Title))
            {
                //throw new Exception("This is not the " + _title); //Exception in constructor????
            }
        }
    }
}
