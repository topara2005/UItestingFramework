using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class DurPDLPanel  : DataPanel
    {
        public DurPDLPanel(ITestVisitor visitor) : base(visitor)
        {

        }
        public override string Url => throw new NotImplementedException();

        public override string PanelTestPrefixAttribute => "pdl";

    
        public virtual SelectElement DropBoxLinkIndicator => new SelectElement(GetWebElement("LinkIndicator"));
     
        public virtual IWebElement TxtNumDays => GetWebElement("PrimDrugDaysNumber");
        public virtual IWebElement TxtCriteriaCount => GetWebElement("PrimDrugGCNCNT");
        public virtual IWebElement TxtDurationDays => GetWebElement("PrimDrugDaySplyNumber");
        public virtual IWebElement TxtDoseRatio => GetWebElement("PrimDrugDoseRatioNumber");

        public virtual IWebElement BtnAdd =>
            RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));
    }
}
