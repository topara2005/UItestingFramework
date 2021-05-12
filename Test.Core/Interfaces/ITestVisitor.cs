using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Test.Core.Interfaces
{
    public interface ITestVisitor
    {
        IWebElement GetElement(By by);
        IEnumerable<IWebElement> GetElements(By by);
        WebDriverWait Wait { get; set; }
        IWebDriver Driver { get; }
    }
}
