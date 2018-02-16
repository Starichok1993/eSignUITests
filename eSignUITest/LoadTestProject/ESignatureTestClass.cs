using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using eSignUITest.Pages;
using LoadTestProject;

namespace LoadTestProject
{
    public class ESignatureTestClass : BaseTestClass
    {
        private string CaseURI
        {
            get
            {
                return StartPageUrl + StartInterviewURI + CaseGUID;
            }
            set
            { }
        }

        public string StartInterviewURI { set; get; }
        public string CaseGUID { set; get; }
        public string StartSignatureUrl { set; get; }



        TimeSpan testDuration = new TimeSpan(0, 30, 0);

        protected override void TestFunction()
        {
            try
            {
                DateTime startDate = DateTime.Now;

                driver.Navigate().GoToUrl(StartPageUrl); //go to login page

                var loginPage = new LoginPage(driver);

                //loginPage.LogInAs("call", "111");
                loginPage.LogInAs("examiner", "test"); //login as examiner

                driver.Navigate().GoToUrl(CaseURI); //go to case

                /*Script for call Sammons*/
                //driver.FindElement(By.Id("btnYes")).Click();
                //try
                //{
                //    driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/button[1]/span")).Click();
                //}
                //catch (NoSuchElementException e)
                //{
                //    Console.WriteLine(e.Message);
                //}

                //driver.FindElement(By.Id("btnSTage1")).Click();
                //driver.FindElement(By.Id("btnNext")).Click();

                /*Script for EC NWM*/
                driver.FindElement(By.Id("YesBtn")).Click();
                driver.FindElement(By.Id("AcceptBtn")).Click();
                driver.FindElement(By.Id("chkExaminerAgree")).Click();
                driver.FindElement(By.Id("NextBtn")).Click();
                driver.FindElement(By.Id("SignSSN")).SendKeys("3333");
                driver.FindElement(By.Id("NextBtn")).Click();
                try
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/button[1]/span")).Click(); //TODO: Remove try catch. check and close "lock by other user" popup
                }
                catch (NoSuchElementException e)
                {
                }


                var buttonLocatorList = new List<By> { By.Name("InsuredSign"), By.Id("btnAgree"), By.Name("divPartB"), By.Id("btnAgree"),
                        By.Name("divHiv"), By.Id("btnAgree"), By.Id("btnNext"), By.Name("CancelESign"), By.XPath("/html/body/div[1]/div[3]/div/button[1]"),
                    };
                var requestDuration = new TimeSpan();
                do
                {
                    //for (var j = 0; j < 7; j++)
                    //{
                    //    var requestDuration = PressElementAndWait(By.Id("buttonNext"));
                    //    logs.Add(String.Format("Time: {0}, Request Duration: {1}", DateTime.Now, requestDuration));
                    //}

                    //driver.FindElement(By.Id("buttonPrint")).Click();
                    //IJavaScriptExecutor jsExecuter = (driver as IJavaScriptExecutor);
                    //wdWait.Until(o => float.Parse(jsExecuter.ExecuteScript("return jQuery.active;").ToString()) == 0.0);
                    //driver.FindElement(By.ClassName("ui-dialog-titlebar-close")).Click();
                    ////class="ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only ui-dialog-titlebar-close"

                    //for (var j = 0; j < 7; j++)
                    //{
                    //    var requestDuration = PressElementAndWait(By.Id("buttonPrev"));
                    //    logs.Add(String.Format("Time: {0}, Request Duration: {1}", DateTime.Now, requestDuration));
                    //}
                    

                    driver.Navigate().GoToUrl(StartPageUrl + "/ESignature/Step1?AppID=" + CaseGUID + "&SignID=Insured");
                    requestDuration = PressElementAndWait(By.Name("CancelESign"));
                    driver.Navigate().GoToUrl(StartPageUrl + "/ESignature/Step1?AppID=" + CaseGUID + "&SignID=Insured");

                    foreach (var buttonLocator in buttonLocatorList)
                    {
                        //Console.WriteLine(buttonLocator.ToString() + "Try press");
                        requestDuration = PressElementAndWait(buttonLocator);
                        logs.Add(String.Format("Time: {0}, Request Duration: {1}", DateTime.Now, requestDuration));                        
                    }

                    
                    
                } while (DateTime.Now.Subtract(startDate) < testDuration); //do until test time doesn't expired
            }
            catch (Exception e)
            {
                var msg = String.Format("Error until execute {0}, ID: {1}, Message: {2}", testName, testId, e.Message);
                //Console.WriteLine(msg);
                logs.Add(msg);
            }

            //wait.Until(ExpectedConditions.ElementExists(By.Id(loginCtrlId))).SendKeys("Admin");
        }

        private TimeSpan PressElementAndWait(By elementSelector)
        {
           // var prevUrl = driver.Url;
            var requestStartTime = DateTime.Now;
            driver.FindElement(elementSelector).Click();

            IJavaScriptExecutor jsExecuter = (driver as IJavaScriptExecutor);
            wdWait.Until(o => float.Parse(jsExecuter.ExecuteScript("return jQuery.active;").ToString()) == 0.0);
            // wdWait.Until(o => !prevUrl.Equals(driver.Url)); //TODO: need use other way for do this
            return DateTime.Now.Subtract(requestStartTime);
        }
    }
}
