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
    public class DurStepTher1Panel : DataPanel
    {
        bool isFirstPass = false;
        const string Xpath_btndiag1 = ".//input[@type='submit' and contains(@id,'PrimCriteria1_DataSearchButton_input')]";
        const string Xpath_btndiag2 = ".//input[@type='submit' and contains(@id,'PrimCriteria2_DataSearchButton_input')]";
        const string Xpath_btndiag3 = ".//input[@type='submit' and contains(@id,'PrimCriteria3_DataSearchButton_input')]";
        StepTherapyCriteriaMiniSearch criteria1, criteria2, criteria3;
        ProductNDCTypeMinSeachPanel drugTypeMiniserach;
        public DurStepTher1Panel(ITestVisitor visitor) : base(visitor)
        {
         //   criteria1 = new StepTherapyCriteriaMiniSearch(visitor, "SecdCriteria1");
          //  criteria2 = new StepTherapyCriteriaMiniSearch(visitor, "SecdCriteria2");
          //  criteria3 = new StepTherapyCriteriaMiniSearch(visitor, "SecdCriteria3");
          //  drugTypeMiniserach = new ProductNDCTypeMinSeachPanel(visitor);
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
            ShowSearchCriteria(Xpath_btndiag1, ref criteria1, "PrimCriteria1_PrimCriteria1");//PrimCriteria1_win
        }
        public void ShowDiagnosis2MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag2, ref criteria2, "PrimCriteria2_PrimCriteria2");

        }
        public void ShowDiagnosis3MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag3, ref criteria3, "PrimCriteria3_PrimCriteria3");
        }
        public StepTherapyCriteriaMiniSearch Criteria1MiniSearch { get=> criteria1; }
        public StepTherapyCriteriaMiniSearch Criteria2MiniSearch { get => criteria2; }
        public StepTherapyCriteriaMiniSearch Criteria3MiniSearch { get => criteria3; }

        public ProductNDCTypeMinSeachPanel ProductNDCTherapeuthicMiniSearch { get => drugTypeMiniserach; }


        public override string Url => throw new NotImplementedException();

        public override string PanelTestPrefixAttribute => "therapy1";


        public virtual SelectElement DropBoxGroupIndicator => new SelectElement(GetWebElement("CriteriaIndicator"));
        public virtual SelectElement DropBoxLinkIndicator =>new  SelectElement(GetWebElement("LinkIndicator"));
        public virtual SelectElement DropLine1DrugTherapy => new SelectElement(GetWebElement("DrugIePrimIndicator"));
        public virtual IWebElement TxtNumDays => GetWebElement("PrimDrugDaysNumber");
        public virtual IWebElement TxtCriteriaCount => GetWebElement("PrimDrugGCNCNT");
        public virtual IWebElement TxtDurationDays => GetWebElement("PrimDrugDaySplyNumber");
        public virtual IWebElement TxtDoseRatio => GetWebElement("PrimDrugDoseRatioNumber");

        public virtual IWebElement BtnSearch1Criteria =>
                RootContainer.FindElement(By.XPath(Xpath_btndiag1));

        public virtual IWebElement BtnSearch2Criteria =>
               RootContainer.FindElement(By.XPath(Xpath_btndiag2));
        public virtual IWebElement BtnSearch3Criteria =>
            RootContainer.FindElement(By.XPath(Xpath_btndiag3));

        public virtual IWebElement BtnDrugType =>
           RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DrugType_DataSearchButton_input')]"));
        public virtual IWebElement BtnAdd =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));
    }
}
