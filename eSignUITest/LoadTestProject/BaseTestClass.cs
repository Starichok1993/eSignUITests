using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using eSignUITest.Pages;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;

namespace LoadTestProject
{
    public class BaseTestClass
    {
        protected IWebDriver driver;
        //protected AppiumDriver<AppiumWebElement> driver;
        protected WebDriverWait wdWait;

        protected Guid testId = Guid.NewGuid();
        protected String testName = "Base test";
        protected List<string> logs = new List<string>();

        public string StartPageUrl { set; get; }

        public async Task<bool> RunTestAsync() //TODO: do async method
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);                
            }
            finally
            {
                EndTest();
            }
        }

        protected virtual void Initialize()
        {
            //DesiredCapabilities capabilities = new DesiredCapabilities(); //TODO: need investigate how i can use Appium

            //capabilities.SetCapability("deviceName", "Xiaomi A1");
            //capabilities.SetCapability("platformName", "Android 7.0");            
            //driver = new AndroidDriver<AppiumWebElement>(
            //               new Uri("http://127.0.0.1:4723/wd/hub"),
            //                   capabilities);



            var options = new ChromeOptions();
            options.AddArgument("headless");
            driver = new ChromeDriver(options);


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // set implicit waits
            wdWait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            Counter.Increment();
        }

        protected virtual void TestFunction()
        {
        }

        protected void EndTest()
        {
            Counter.Decrement();
            driver.Close();
            
        }
    }
}
