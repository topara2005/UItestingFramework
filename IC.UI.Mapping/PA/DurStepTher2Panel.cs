using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class DurStepTher2Panel :  DataPanel
    {
        bool isFirstPass = false;
        const string Xpath_btndiag1 = ".//input[@type='submit' and contains(@id,'SecdCriteria1_DataSearchButton_input')]";
        const string Xpath_btndiag2 = ".//input[@type='submit' and contains(@id,'SecdCriteria2_DataSearchButton_input')]";
        const string Xpath_btndiag3 = ".//input[@type='submit' and contains(@id,'SecdCriteria3_DataSearchButton_input')]";
        StepTherapyCriteriaMiniSearch criteria1, criteria2, criteria3;
        ProductNDCTypeMinSeachPanel drugTypeMiniserach;
        public DurStepTher2Panel(ITestVisitor visitor) : base(visitor)
        {

        }

        private void ShowSearchCriteria(string btnName, ref StepTherapyCriteriaMiniSearch miniSearch, string dialogName)
        {
            try
            {
                var root = this.Visitor.Driver.FindElement(By.XPath(this.XPathRoot));
                var btn = this.Visitor.Wait.Until(d => root.FindElement(By.XPath(btnName)));
                btn.Click();
                Thread.Sleep(4000);
                if (!isFirstPass)
                {
                    root = this.Visitor.Driver.FindElement(By.XPath(this.XPathRoot));
                    btn = this.Visitor.Wait.Until(d => root.FindElement(By.XPath(btnName)));
                    btn.Click();
                    Thread.Sleep(4000);
                }
                isFirstPass = true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            if (miniSearch == null)
            {
                miniSearch = new StepTherapyCriteriaMiniSearch(this.Visitor, dialogName);
            }
        }

        public void ShowDiagnosis1MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag1, ref criteria1, "SecdCriteria1_SecdCriteria1");//PrimCriteria1_win
        }
        public void ShowDiagnosis2MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag2, ref criteria2, "SecdCriteria2_SecdCriteria2");

        }
        public void ShowDiagnosis3MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag3, ref criteria3, "SecdCriteria3_SecdCriteria3");
        }
        public StepTherapyCriteriaMiniSearch Criteria1MiniSearch { get => criteria1; }
        public StepTherapyCriteriaMiniSearch Criteria2MiniSearch { get => criteria2; }
        public StepTherapyCriteriaMiniSearch Criteria3MiniSearch { get => criteria3; }

        public ProductNDCTypeMinSeachPanel ProductNDCTherapeuthicMiniSearch { get => drugTypeMiniserach; }
        public override string Url => throw new NotImplementedException();

        public override string PanelTestPrefixAttribute => "therapy2";


        public virtual SelectElement DropBoxGroupIndicator => new SelectElement(GetWebElement("CriteriaIndicator"));
        public virtual SelectElement DropBoxLinkIndicator => new SelectElement(GetWebElement("LinkIndicator"));
        public virtual SelectElement DropLine2DrugTherapy => new SelectElement(GetWebElement("DrugIeSecdIndicator"));
        public virtual IWebElement TxtNumDays => GetWebElement("SecdDrugDaysNumber");
        public virtual IWebElement TxtCriteriaCount => GetWebElement("SecdDrugGCNCNT");
        public virtual IWebElement TxtDurationDays => GetWebElement("SecdDrugDaySplyNumber");
        public virtual IWebElement TxtDoseRatio => GetWebElement("SecdDrugDoseRatioNumber");

        public virtual IWebElement BtnSearch1Criteria =>
                RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'SecdCriteria1_DataSearchButton_input')]"));

        public virtual IWebElement BtnSearch2Criteria =>
               RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'SecdCriteria2_DataSearchButton_input')]"));
        public virtual IWebElement BtnSearch3Criteria =>
            RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'SecdCriteria3_DataSearchButton_input')]"));

        public virtual IWebElement BtnDrugType =>
           RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DrugType_DataSearchButton_input')]"));
        public virtual IWebElement BtnAdd =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));
    }
}
