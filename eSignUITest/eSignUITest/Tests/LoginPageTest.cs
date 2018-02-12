using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using eSignUITest.Pages;
//using System.Threading;

namespace eSignUITest
{
    public class LoginPageTest
    {
        IWebDriver driver;

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
            loginPage.LogInExpectingFailure("blabla", "asdfasd");
            Assert.AreEqual("Not Valid User.", loginPage.GetValidationMessage());
            loginPage.LogInExpectingFailure(" Admin ", "111");
            Assert.AreEqual("Not Valid User.", loginPage.GetValidationMessage());
            loginPage.LogInExpectingFailure("", "");
            Assert.AreEqual("Not Valid User.", loginPage.GetValidationMessage());

            loginPage.LogInAs("Admin", "111");
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
