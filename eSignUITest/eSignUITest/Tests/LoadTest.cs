using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using eSignUITest.Pages;

namespace eSignUITest.Tests
{
    class LoadTest
    {
        IWebDriver driver;

        string caseURI = "http://applicintweb.com/Sammons_B2/CallCenter/Instructions?AskSSN=true&AppID=bfaf9101-b75a-44da-a4dc-86d199c37984";
        string loginPageURI = "http://applicintweb.com/sammons_b2";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // set implicit waits
        }
        [Test]
        public void TestFunction()
        {
            driver.Navigate().GoToUrl(loginPageURI); //go to login page

            var loginPage = new LoginPage(driver);
            loginPage.LogInAs("call", "111");
            driver.Navigate().GoToUrl(caseURI); //go to case

            driver.FindElement(By.Id("btnYes")).Click();
            driver.FindElement(By.Id("btnSTage1")).Click();
            driver.FindElement(By.Id("btnNext")).Click();

            var prevURL = driver.Url;
            var wdWait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            var time = new DateTime();
            var loadRequestTime = new TimeSpan();
            var minLoadRequestTime = new TimeSpan(0,1,0);
            var maxLoadRequestTime = new TimeSpan();

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
                    }
                }
                Console.WriteLine("Max request time: " + maxLoadRequestTime + " Min request time: " + minLoadRequestTime);
            }
            
            Console.WriteLine("Max request time: " + maxLoadRequestTime + " Min request time: " + minLoadRequestTime);
            //WebDriverWait wait = new WebDriverWait(driver, new TimeSpan (0,0,15)); //use wait until some condition would be done
            //wait.Until(ExpectedConditions.ElementExists(By.Id(loginCtrlId))).SendKeys("Admin");

        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
