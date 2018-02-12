using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSignUITest.Pages
{
    class LoginPage : BasePage
    {
        By loginLocator = By.Id("login-input");
        By passwordLocator = By.Id("password-input");
        By submitButtonLocator = By.Id("BSign");
        By validationMessageLocator = By.Id("SysMessageLabel");

        public LoginPage(IWebDriver driver) : base (driver, "Login Page")
        {
        }

        public LoginPage TypeLogin(string login)
        {
            _driver.FindElement(loginLocator).Clear();
            _driver.FindElement(loginLocator).SendKeys(login);
            return this;
        }

        public LoginPage TypePassword(string password)
        {
            _driver.FindElement(passwordLocator).Clear();
            _driver.FindElement(passwordLocator).SendKeys(password);
            return this;
        }

        // TODO: Base Page should be replaced to Call Centre page
        public BasePage SubmitForm()
        {
            _driver.FindElement(submitButtonLocator).Submit();
            return new BasePage(_driver, "");
        }

        public LoginPage SubmitFormExpectedFailure()
        {
           _driver.FindElement(submitButtonLocator).Submit();
            return this;
        }

        public BasePage LogInAs(string login, string password)
        {
            return (BasePage) this.TypeLogin(login).TypePassword(password).SubmitForm();
        }
        public LoginPage LogInExpectingFailure(string login, string password)
        {
            return this.TypeLogin(login).TypePassword(password).SubmitFormExpectedFailure();
        }

        public string GetValidationMessage()
        {
            var messageItem = _driver.FindElement(validationMessageLocator);
            return messageItem.Displayed ? messageItem.Text : ""; 
        }
    }
}
