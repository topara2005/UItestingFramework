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
    public class DurDiagnosisSecondaryPanel : DataPanel
    {
        DiagnosisMiniSearch _diagnosisiMiniSearch1;
        DiagnosisMiniSearch _diagnosisiMiniSearch2;
        DiagnosisMiniSearch _diagnosisiMiniSearch3;
        DiagnosisTypeMiniSearch _diagnosisTypeMiniSearch;
        const string Xpath_btndiag1 = ".//input[@type='submit' and contains(@id,'Diag1_DataSearchButton_input')]";
        const string Xpath_btndiag2 = ".//input[@type='submit' and contains(@id,'Diag2_DataSearchButton_input')]";
        const string Xpath_btndiag3 = ".//input[@type='submit' and contains(@id,'Diag3_DataSearchButton_input')]";
        const string Xpath_btntype = ".//input[@type='submit' and contains(@id,'DiagTypePrim_DataSearchButton_input')]";
        bool isFirstPass = false;

        public DurDiagnosisSecondaryPanel(ITestVisitor visitor) : base(visitor)
        {
            _diagnosisTypeMiniSearch = new DiagnosisTypeMiniSearch(visitor);
        }
        public override string Url => "secondarydiagnosis";

        public override string PanelTestPrefixAttribute => "secondarydiagnosis";


        public virtual SelectElement DropBoxPrimaryDiagnoisis => new SelectElement(GetWebElement("DiagIeSecdIndicator"));
        public virtual SelectElement DropBoxLinkIndicator => new SelectElement(GetWebElement("LinkIndicator"));
        public virtual IWebElement TxtDoseRatio => GetWebElement("SecdDiagDoseRatioNumber");
        public virtual IWebElement TxtDaysSupply => GetWebElement("SecdDiagDaySplyNumber");


        public virtual IWebElement BtnDignosis1SearchCriteria =>
            this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btndiag1)));
        //   RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'Diag1_DataSearchButton_input')]"));

        public virtual IWebElement BtnDignosis2SearchCriteria =>
                this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btndiag2)));

        public virtual IWebElement BtnDignosis3SearchCriteria =>
               this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btndiag3)));

        public virtual IWebElement BtnDignosisType =>
               this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btntype)));

        public virtual IWebElement BtnAdd =>
               RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));


        public DiagnosisMiniSearch Diagnosis1MiniSearch { get => _diagnosisiMiniSearch1; }
        public DiagnosisMiniSearch Diagnosis2MiniSearch { get => _diagnosisiMiniSearch2; }
        public DiagnosisMiniSearch Diagnosis3MiniSearch { get => _diagnosisiMiniSearch3; }

        public DiagnosisTypeMiniSearch DiagnosisTypeMiniSearch { get => _diagnosisTypeMiniSearch; }



        private void ShowSearchCriteria(string btnName, ref DiagnosisMiniSearch miniSearch, string dialogName)
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
                miniSearch = new DiagnosisMiniSearch(this.Visitor, dialogName);
            }
        }

        public void ShowDiagnosis1MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag1, ref _diagnosisiMiniSearch1, "Diag1_Diag1");
        }
        public void ShowDiagnosis2MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag2, ref _diagnosisiMiniSearch2, "Diag2_Diag2");

        }
        public void ShowDiagnosis3MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag3, ref _diagnosisiMiniSearch3, "Diag3_Diag3");
        }
        public void ShowDiagnosisTypeMiniSearch()
        {
            BtnDignosisType.Click();
            Thread.Sleep(4000);
        }

    }
}
