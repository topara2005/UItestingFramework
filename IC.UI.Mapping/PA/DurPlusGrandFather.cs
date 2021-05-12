using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace IC.UI.Mapping.PA
{
    public class DurPlusGrandFather : DataPanel
    {
        public DurPlusGrandFather(ITestVisitor visitor) : base(visitor)
        {

        }
        public override string PanelTestPrefixAttribute => "grandfather";

        public override string Url => throw new NotImplementedException();


        public virtual IWebElement TxtDoseRatio => GetWebElement("GrndftrDoseRatioNumber");


        public virtual IWebElement TxtDaysSupply => GetWebElement("GrndftrDaySplyNumber");

        public virtual IWebElement TxtNumberDays => GetWebElement("GrndftrDaysNumber");

        public virtual SelectElement DropBoxLinkIndicator => new SelectElement(GetWebElement("LinkIndicator"));


        public virtual IWebElement BtnAdd =>
             RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));
    }
}
