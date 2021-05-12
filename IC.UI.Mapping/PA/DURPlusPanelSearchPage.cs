using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class DURPlusPanelSearchPage : BasePage
    {

        public const string Xpath_DurPlusNumber = "//table/tbody/tr/td/input[contains(@name,'DURPlusNumber')]";
        public const string Xpath_SearchButton = "//input[contains(@value,'search') and  contains(@id,'SearchButton')  and @type='submit']";
        public const string Xpath_addButton = "//input[contains(@value,'add') and  contains(@id,'NewButton')  and @type='submit']";
        public const string Xpath_Description = "//table/tbody/tr/td/input[contains(@name,'Description')]";

        public DURPlusPanelSearchPage(ITestVisitor visitor) : base(visitor)
        {

        }
        public override string Url => "";


        public IWebElement TxtDurPlusNumber => Visitor.GetElement(By.XPath(Xpath_DurPlusNumber));

        public IWebElement ButtonSearch => Visitor.GetElement(By.XPath(Xpath_SearchButton));
        public IWebElement TxtDurPlusDescription => Visitor.GetElement(By.XPath(Xpath_Description));
        public IWebElement ButtonAdd => Visitor.GetElement(By.XPath(Xpath_addButton));

    }
}
