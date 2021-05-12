using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.Common
{
    public class DriverFactory
    {
        /// <summary>
        /// Gets a driver of perticular type.
        /// </summary>
       
        /// <returns></returns>
      
        public   IWebDriver GetDriver()
        {
            switch (ConfigurationManager.AppSettings["TargetDriver"])
            {
                case "Edge":
                    {
                        IWebDriver driver = new  EdgeDriver();
                        InitializeDriverOperations(driver);
                        return driver;
                    }

                case "IE":
                    {
                        var options = new InternetExplorerOptions();
                        options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        options.IgnoreZoomLevel = true;
                        IWebDriver driver = new InternetExplorerDriver(options);
                        InitializeDriverOperations(driver);


                        return driver;
                    }
                case "Firefox":
                    {

                        IWebDriver driver = new FirefoxDriver();
                        InitializeDriverOperations(driver);


                        return driver;
                    }
                case "Chrome":
                    {
                        IWebDriver driver = new ChromeDriver();
                      //  System.setProperty("webdriver.chrome.driver", "path/to/downloaded/driver");
                        ChromeOptions chromeOptions = new ChromeOptions();
                        InitializeDriverOperations(driver);
                      /*  driver.clearPreferences();
                        chromeOptions.c*/


                        return driver;

                    }
                default:
                    return null;


            }
        }

        public void CloseDriver(IWebDriver webDriver)
        {
            if (webDriver != null)
                webDriver.Quit();
        }

        /// <summary>
        /// Initializes the options for the driver.
        /// </summary>
        /// <param name="driver">driver instance.</param>
      
        private  void InitializeDriverOperations(IWebDriver driver)
        {

            driver.Manage().Window.Maximize();
            //https://code.google.com/p/chromedriver/issues/detail?id=109
            if (!ConfigurationManager.AppSettings["TargetDriver"].Equals("Chrome",StringComparison.InvariantCultureIgnoreCase))
            {
              //  driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationManager.AppSettings["DriverPageLoadTimeout"])));
            }
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationManager.AppSettings["DriverWebElementTimeout"])));




        }
    }

}
