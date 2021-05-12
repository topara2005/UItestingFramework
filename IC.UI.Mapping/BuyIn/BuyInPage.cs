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
   public class BuyInPage : BasePage
    {
        public const string xpath_OpenTabMenu = "//*/a/span[contains(text(),'Open Tab')]//ancestor::a";
        public const string xpath_OpenTabMenu_RelatedData = "//*/a/span[contains(text(),'Open Tab')]/ancestor::li/div/ul/li[contains(@class,'rmItem')]/a/span[text()='Related Data']/ancestor::a";
        public const string xpath_OpenTabMenu_RelatedData_BendexSearch = "//*/li[contains(@class,'rmItem')]/*/*[contains(@id,'n12')]/ancestor::div[1]/span[text()='BENDEX Search']//preceding::input[1]";
    
        public BuyInPage(ITestVisitor visitor) : base(visitor)
        {

        }

        public override string Url => "";


        public IWebElement TabMenu => Visitor.GetElement(By.XPath(xpath_OpenTabMenu));

        public IWebElement TabMenuRelatedData => Visitor.GetElement(By.XPath(xpath_OpenTabMenu_RelatedData));
        public IWebElement TabMenuRelatedDataBendexSearch => Visitor.GetElement(By.XPath(xpath_OpenTabMenu_RelatedData_BendexSearch));
    }
}
