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
    public class DurPlusAgePanel : DataPanel
    {
        public DurPlusAgePanel(ITestVisitor visitor) : base(visitor)
        {

        }
        public override string PanelTestPrefixAttribute => "age";

        public override string Url => throw new NotImplementedException();


        public virtual IWebElement TxtMinNumber => GetWebElement("AgeMinNumber");


        public virtual IWebElement TxtMaxNumber => GetWebElement("AgeMaxNumber");

        public virtual IWebElement TxtDoseRatio => GetWebElement("QTYDoseRatio");

        public virtual IWebElement TxtNumberDays => GetWebElement("DaySplyNumber");

        public virtual SelectElement DropBoxLinkIndicator => new SelectElement(GetWebElement("LinkIndicator"));


        public virtual IWebElement BtnAdd =>
          RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));
    }
}
