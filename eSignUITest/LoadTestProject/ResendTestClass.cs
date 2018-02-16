using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using eSignUITest.Pages;

namespace LoadTestProject
{
    public class ResendTestClass : BaseTestClass
    {
        protected override void Initialize()
        {
            base.Initialize();
            wdWait.Timeout = new TimeSpan(0, 0, 300);
        }
        protected override void TestFunction()
        {
            try
            {              
                DateTime startDate = DateTime.Now;

                driver.Navigate().GoToUrl(StartPageUrl); //go to login page

                var loginPage = new LoginPage(driver);
                //loginPage.LogInAs("call", "111");
                loginPage.LogInAs("examiner", "test");
                driver.Navigate().GoToUrl(StartPageUrl + "/SubmittedApp"); //go to Submitted list
                
                //driver.FindElements(By.LinkText("Resend")).Select(o => { o.Click(); return o; });
                var resendBtns = driver.FindElements(By.LinkText("Resend"));

                foreach (var resendBtn in resendBtns)
                {
                    resendBtn.Click();
                }
                IJavaScriptExecutor jsExecuter = (driver as IJavaScriptExecutor);
                wdWait.Until(o => float.Parse(jsExecuter.ExecuteScript("return jQuery.active;").ToString()) == 0.0);
            }
            catch (Exception e)
            {
                var msg = String.Format("Error until execute {0}, ID: {1}, Message: {2}",testName, testId, e.Message);
                Console.WriteLine(msg);
                logs.Add(msg);
            }
        }
    }
}
