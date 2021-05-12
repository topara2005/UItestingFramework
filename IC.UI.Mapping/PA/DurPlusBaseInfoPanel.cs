using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;



namespace TestLibrary.UIMapping
{
    public class DurPlusBaseInfoPanel : DataPanel
    {

        PublicHealthProgramMiniSearch _healthProgram;
        DurPlusCodeMiniSearch _DurplusCode;
        public DurPlusBaseInfoPanel(ITestVisitor visitor) : base(visitor)
        {
            _healthProgram = new PublicHealthProgramMiniSearch(visitor);
            _DurplusCode= new DurPlusCodeMiniSearch(visitor);
          

        }

        public PublicHealthProgramMiniSearch PublicHealthMiniSearchPanel => _healthProgram;
        public DurPlusCodeMiniSearch DurPlusCodeMiniSearchPanel => _DurplusCode;

        public void ShowPublicHealthMiniSearchPanel() {
            BtnHealthCriteria.Click();
            Thread.Sleep(500);
            BtnHealthCriteria.Click();
            Thread.Sleep(4000);
        }

        public void ShowDurPlusCodeMiniSearchPanel()
        {
           
            BtnCodeSearchCriteria.Click();
            Thread.Sleep(5000);
       
        }

        public override string Url => throw new NotImplementedException();
        public override string PanelTestPrefixAttribute => "durplusbase";


        public virtual IWebElement TxtDescription => GetWebElement("AutoPaDescription");
        public virtual IWebElement TxtEfectiveDate => GetWebElement("EffectiveDate");

        public virtual IWebElement TxtEndDate => GetWebElement("EndDate");
        public virtual IWebElement TxtDsCriteria => //GetWebElement("mb_ds_Criteria");
             this.RootContainer.FindElement(By.XPath(".//input[contains(@id,'mb_ds_Criteria') and @type='text']"));
        public virtual IWebElement BtnCodeSearchCriteria =>
                  RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'Criteria_DataSearchButton')]"));

        public virtual IWebElement TxtHealthCriteria =>
            this.RootContainer.FindElement(By.XPath(".//input[contains(@id,'ds_PublicHlthPgm') and @type='text']"));


        //public virtual SelectElement DropDownCriteriaIndicator => new SelectElement(GetWebElement("CriteriaIndicator")); 
        public virtual SelectElement DropDownCriteriaIndicator =>
            new SelectElement(this.RootContainer.FindElement(By.XPath(".//select[contains(@id,'mb_CriteriaIndicator')]")));

        public virtual SelectElement DropDownGroupIndicator =>
            new SelectElement(this.RootContainer.FindElement(By.XPath(".//select[contains(@id,'mb_IndType')]")));

        public virtual SelectElement DropDownStatus =>
          new SelectElement(this.RootContainer.FindElement(By.XPath(".//select[contains(@id,'mb_StatusCode')]")));

        public virtual SelectElement DropDownLetterIndicator =>
         new SelectElement(this.RootContainer.FindElement(By.XPath(".//select[contains(@id,'mb_LtrGenIndicator')]")));




        // mb_CriteriaIndicator
        public virtual IWebElement BtnHealthCriteria => 
            RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'PublicHlthPgm_DataSearchButton_input')]"));
        

    }
}
