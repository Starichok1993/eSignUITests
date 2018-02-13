using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using eSignUITest.Pages;

namespace eSignUITest.Tests
{
    class WizardLoadTestClass
    {
        IWebDriver driver;

        string caseURI = "http://applicintweb.com/NWM_ExamOne_B2/App/ESignConfirmationPage?AskSSN=true&AppID=7f6c5933-632e-4284-b233-bb08172d125c";
        //"http://applicintweb.com/Sammons_B2/CallCenter/Instructions?AskSSN=true&AppID=bfaf9101-b75a-44da-a4dc-86d199c37984";

        string loginPageURI = "http://applicintweb.com/NWM_ExamOne_B2";
            //"http://applicintweb.com/sammons_b2";

        TimeSpan minLoadRequestTime = new TimeSpan(0, 1, 0);
        TimeSpan maxLoadRequestTime = new TimeSpan();
        DateTime maxLoadRequestDateTime = new DateTime();

        public async Task<bool> RunTestAsync()
        {
            Initialize();
            try
            {
                TestFunction();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                EndTest();
            }
        }        

        public void RunTest()
        {
            Initialize();
            try
            {
                TestFunction();
                //return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //return false;
            }
            finally
            {
                EndTest();
            }
        }
        
        protected void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // set implicit waits
        }

        protected void TestFunction()
        {
            try
            {
                driver.Navigate().GoToUrl(loginPageURI); //go to login page

                var loginPage = new LoginPage(driver);
                //loginPage.LogInAs("call", "111");
                loginPage.LogInAs("examiner", "test");
                driver.Navigate().GoToUrl(caseURI); //go to case

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
                    driver.FindElement(By.XPath("/html/body/div[1]/div[3]/div/button[1]/span")).Click();
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine(e.Message);
                }
                
                var prevURL = driver.Url;
                var wdWait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
                var time = new DateTime();
                var loadRequestTime = new TimeSpan();

                for (var i = 0; i < 20; i++)
                {
                    for (var j = 0; j < 7; j++)
                    {
                        driver.FindElement(By.Id("buttonNext")).Click();
                        time = DateTime.Now;
                        wdWait.Until(o => !prevURL.Equals(driver.Url));
                        loadRequestTime = DateTime.Now.Subtract(time);
                        prevURL = driver.Url;

                        if (minLoadRequestTime > loadRequestTime)
                        {
                            minLoadRequestTime = loadRequestTime;
                        }
                        if (maxLoadRequestTime < loadRequestTime)
                        {
                            maxLoadRequestTime = loadRequestTime;
                        }
                    }

                    for (var j = 0; j < 7; j++)
                    {
                        driver.FindElement(By.Id("buttonPrev")).Click();
                        time = DateTime.Now;
                        wdWait.Until(o => !prevURL.Equals(driver.Url));
                        loadRequestTime = DateTime.Now.Subtract(time);
                        prevURL = driver.Url;

                        if (minLoadRequestTime > loadRequestTime)
                        {
                            minLoadRequestTime = loadRequestTime;
                        }
                        if (maxLoadRequestTime < loadRequestTime)
                        {
                            maxLoadRequestTime = loadRequestTime;
                            maxLoadRequestDateTime = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(String.Format("Min request time: {0}, Max request time: {1}, At: {2}", minLoadRequestTime, maxLoadRequestTime, maxLoadRequestDateTime));
            //WebDriverWait wait = new WebDriverWait(driver, new TimeSpan (0,0,15)); //use wait until some condition would be done
            //wait.Until(ExpectedConditions.ElementExists(By.Id(loginCtrlId))).SendKeys("Admin");
        }

        protected void EndTest()
        {
            driver.Close();
        }
    }
}
