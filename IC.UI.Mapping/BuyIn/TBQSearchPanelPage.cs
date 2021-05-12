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
    public class TBQSearchPanelPage:BasePage
    {
        public const string Xpath_MedicarIdText = "//table/tbody/tr/td/input[contains(@name,'MedicareID')]";
        public const string Xpath_SearchButton = "//input[contains(@value,'search') and  contains(@id,'SearchButton')  and @type='submit']";

        public TBQSearchPanelPage(ITestVisitor visitor) : base(visitor)
        {

        }

        public override string Url => "";

        public IWebElement TxtMedicareId => Visitor.GetElement(By.XPath(Xpath_MedicarIdText));

        public IWebElement ButtonSearch => Visitor.GetElement(By.XPath(Xpath_SearchButton));
    }
}
