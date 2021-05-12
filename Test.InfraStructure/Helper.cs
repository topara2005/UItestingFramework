using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary.Global;

namespace TestLibrary
{
   public  class Helper
    {

        public Helper()
        {

        }
      
        public bool NavigateToHomePage(IWebDriver webDriver)
        {
            webDriver.Navigate().GoToUrl(GlobalValues.baseURL);
            return webDriver.Title.Contains("Url Upload");
        }

        public bool IsTextPresent(IWebDriver webDriver, string text)
        {
            return webDriver.FindElement(By.TagName("body")).Text.Contains(text);

        }

        public string CallScript(IWebDriver driver, string script)
        {
            var result = ((IJavaScriptExecutor)driver).ExecuteScript(script);
            return result != null ? result.ToString() : null;
        }

        public void TakeScreenshot(IWebDriver webDriver, string fileName)
        {
            try
            {
                if (webDriver != null)
                {
                    string filePath = ConfigurationManager.AppSettings["ScreenshotsPath"].ToString();

                    // Take the screenshot            
                    Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                    string screenshot = ss.AsBase64EncodedString;
                    byte[] screenshotAsByteArray = ss.AsByteArray;

                    // Save the screenshot
                    ss.SaveAsFile(filePath + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt") + "_" + fileName + ".Jpeg", OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
                    ss.ToString();
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        public bool IsElementPresent(IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.FindElement(by);
                IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(by));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public IWebElement WaitForElement(IWebDriver webDriver, By by, TimeSpan t)
        {
            /*  try
              {*/
          

                IWait<IWebDriver> wait = new WebDriverWait(webDriver, t);
                return wait.Until(d => d.FindElement(by));

           /* }
            catch (NoSuchElementException)
            {
                return null;
            }*/
        }

        public ReadOnlyCollection<IWebElement> WaitForElements(IWebDriver webDriver, By by, TimeSpan t)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(webDriver, t);
                return wait.Until(d => d.FindElements(by));

            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }


        public bool WaitForTextOnPage(IWebDriver webDriver, String text, TimeSpan t)
        {
            try
            {
                //driver.FindElement(by);
                WebDriverWait wait = new WebDriverWait(webDriver, t);
                return wait.Until(d => IsTextPresent(d, text));

            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void StartIISExpress(string webSite)
        { 
            const string iisexpress = "iisexpress";
            Process iis = null;
            var iises = Process.GetProcessesByName(iisexpress);
                while (iises.Length > 0)
                { 
                    iis = iises[0];
                    iis.Kill();
                    iis.WaitForExit();
                    iises = Process.GetProcessesByName(iisexpress);
                }
            iis = new Process();
            try
            {

                iis.StartInfo.FileName = @"C:\Program Files\IIS Express\iisexpress.exe";
                iis.StartInfo.Arguments = webSite; // "/site:Website1";
                iis.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }







    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
}
