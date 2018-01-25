using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Threading;

namespace eSignUITest
{
        public class LoginPage
    {
        IWebDriver driver;

        string loginPageURI = "http://localhost/app_mvc";
        string loginCtrlId = "login-input";
        string passwordCtrlId = "password-input";
        string submitButton = "BSign";

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void TestFunction()
        {
            driver.Navigate().GoToUrl(loginPageURI); //go to login page
           
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan (0,0,15)); //use wait until some condition would be done
            wait.Until(o => driver.FindElement(By.Id(loginCtrlId)).Displayed);
           
            driver.FindElement(By.Id(loginCtrlId)).SendKeys("Admin");
            driver.FindElement(By.Id(passwordCtrlId)).SendKeys("111");
            driver.FindElement(By.Id(submitButton)).Submit();

        }
        [TearDown]
        public void EndTest()
        {
            driver.Close();
        }
    }
}
