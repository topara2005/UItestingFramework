using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace TestLibrary
{
    public static class WebDriverExtensions
    {
        public static bool PageSourceContains(this IWebDriver driver, string content, int waitTime)
        {
            return PageContains(driver, waitTime, () => driver.PageSource.Contains(content));
        }

        /// <summary>
        /// Take a screenshot of the current page
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="FileLocationName">Full directory plus the file name and format, e.g C:\Temp\Image.jpeg</param>
        /// <param name="imageFormat">System image format, I tend to stick to jpeg</param>
        public static void TakeScreenshot(this IWebDriver driver, string FileLocationName, ScreenshotImageFormat imageFormat)
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(FileLocationName, imageFormat);
        }
    

    public static bool PageSourceDoesNotContain(this IWebDriver driver, string content, int waitTime)
        {
            return PageContains(driver, waitTime, () => !driver.PageSource.Contains(content));
        }

        private static bool PageContains(this IWebDriver driver, int waitTime, Func<bool> eventOccurs)
        {
            if (waitTime > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
                return wait.Until(drv => eventOccurs());
            }
            return eventOccurs();
        }

        public static IWebElement WaitForElementToAppear(this IWebDriver driver, int waitTime, By waitingElement)
        {
          // IWebElement wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime)).Until(WebDriverExtensions.ElementExists(driver,waitingElement));
            Helper helper = new Helper();

            return helper.WaitForElement(driver, waitingElement, TimeSpan.FromSeconds(waitTime)); 
        }
       /* public static bool ElementExists(IWebDriver driver, By waitingElement)
        {
            try
            {
               
               
                Wait.Until(c => c.FindElement(By.Id("content-section")));
                var elementToBeDisplayed = driver.FindElement(waitingElement);
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }*/
    }

}
