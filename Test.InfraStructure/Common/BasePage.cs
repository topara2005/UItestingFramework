using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;

namespace Test.InfraStructure.Common
{
    public abstract class BasePage
    {
        private readonly ITestVisitor visitor;
        protected string XPath_RadNavigationBar =
            "//div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rmItem')]//a//span[text()='{0}']//ancestor::a";


        public BasePage(ITestVisitor visitor)
        {
            this.visitor = visitor;



        }

        public IDictionary<string,IWebElement> GetSubMenuItems(string subMenu)
        {
            var elements2 = new Dictionary<string,IWebElement>();
            //string itemsQuery =
            //    $"//div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rmItem')]//span[text()='{subMenu}']//ancestor::li//div//ul//li[contains(@class, 'rmItem')]//div//span[text()='{0}']//ancestor::li[1]";
            string menu =
                   $"//div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rmItem')]//span[text()='{subMenu}']//ancestor::li//div//ul//li[contains(@class, 'rmItem')]//div//span";
            var elements=Visitor.GetElements(By.XPath(menu)).ToDictionary(d => d.GetAttribute("innerHTML"), d => d);
            foreach (var key in elements.Keys)
            {
              var query= 
                    $"//div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rmItem')]//span[text()='{subMenu}']//ancestor::li//div//ul//li[contains(@class, 'rmItem')]//div//span[text()='{key}']//ancestor::li[1]";
                elements2.Add(key, Visitor.GetElement(By.XPath(query)));
            }
            return elements2;
        }
   
        public virtual string OracleDBConnectionString => ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        protected ITestVisitor Visitor => visitor;
        public abstract string Url { get; }
      
            
      
        public virtual void Open(string part = "")
        {
            visitor.Driver.Navigate().GoToUrl(string.Concat(this.Url, part));
        }

     

}

   
}
