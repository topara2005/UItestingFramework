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
   public  class BendexInfoPanelPage : BasePage
    {
        public const string Xpath_TxtMedicareId = "//div[contains(@title,'> BENDEX')]/ancestor::div[3]//*//tr[@class='iC_EditItem']//input[contains(@id,'MedicareId')]";
        public const string Xpath_BtnSearch  = "//div[contains(@title,'> BENDEX')]/ancestor::div[3]//*//tr[@class='iC_EditItem']//input[contains(@value,'search') and contains(@type,'sub')]";
        public BendexInfoPanelPage(ITestVisitor visitor) : base(visitor)
        {

        }

        public override string Url => "";


        public IWebElement TxtMedicareId => Visitor.GetElement(By.XPath(Xpath_TxtMedicareId));
        public IWebElement BtnSearch => Visitor.GetElement(By.XPath(Xpath_BtnSearch));
    }
}
