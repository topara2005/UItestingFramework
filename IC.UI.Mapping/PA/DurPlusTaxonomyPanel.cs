using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace IC.UI.Mapping.PA
{
    public class DurPlusTaxonomyPanel : DataPanel
    {
        

        TaxonomyMiniSearch _taxonomyMiniSearch1;
        TaxonomyMiniSearch _taxonomyMiniSearch2;
        TaxonomyMiniSearch _taxonomyMiniSearch3;
        TaxonomyTypeMiniSearch _diagnosisTypeMiniSearch;
        bool isFirstPass = false;

        const string Xpath_btndiag1 = ".//input[@type='submit' and contains(@id,'Taxonomy1_DataSearchButton_input')]";
        const string Xpath_btndiag2 = ".//input[@type='submit' and contains(@id,'Taxonomy2_DataSearchButton_input')]";
        const string Xpath_btndiag3 = ".//input[@type='submit' and contains(@id,'Taxonomy3_DataSearchButton_input')]";
        const string Xpath_btntype = ".//input[@type='submit' and contains(@id,'TaxonomyType_DataSearchButton_input')]";

        public DurPlusTaxonomyPanel(ITestVisitor visitor) : base(visitor)
        {

        }
        public override string PanelTestPrefixAttribute => "taxonomy";

        public override string Url => throw new NotImplementedException();


        public virtual IWebElement TxtDoseRatio => GetWebElement("TaxonomyDoseRatioNumber");

        public virtual IWebElement TxtDaysSupply => GetWebElement("TaxonomyDaySplyNumber");



        public virtual IWebElement BtnAdd =>
             RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'NewButton_input')]"));

        public virtual IWebElement BtnDelete =>
              RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'DeleteButton_input')]"));



        public TaxonomyMiniSearch Taxonomy1MiniSearch { get => _taxonomyMiniSearch1; }
        public TaxonomyMiniSearch Taxonomy2MiniSearch { get => _taxonomyMiniSearch2; }
        public TaxonomyMiniSearch Taxonomy3MiniSearch { get => _taxonomyMiniSearch3; }
        public TaxonomyTypeMiniSearch TaxonomyTypeMiniSearch { get => _diagnosisTypeMiniSearch; }



        private void ShowSearchCriteria(string btnName, ref TaxonomyMiniSearch miniSearch, string dialogName)
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
                miniSearch = new TaxonomyMiniSearch(this.Visitor, dialogName);
            }
        }


        public void ShowTaxonomy1MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag1, ref _taxonomyMiniSearch1, "_Taxonomy1");
        }
        public void ShowTaxonomy2MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag2, ref _taxonomyMiniSearch2, "_Taxonomy2");

        }
        public void ShowTaxonomy3MiniSeachPanel()
        {
            ShowSearchCriteria(Xpath_btndiag3, ref _taxonomyMiniSearch3, "_Taxonomy3");
        }
        public void ShowTaxonomyTypeMiniSearch()
        {
            BtnTaxonomyType.Click();
            Thread.Sleep(4000);
        }


        public virtual IWebElement BtnTaxonomy1SearchCriteria =>
           this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btndiag1)));
        //   RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'Diag1_DataSearchButton_input')]"));

        public virtual IWebElement BtnTaxonomy2SearchCriteria =>
                this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btndiag2)));

        public virtual IWebElement BtnTaxonomy3SearchCriteria =>
               this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btndiag3)));

        public virtual IWebElement BtnTaxonomyType =>
               this.Visitor.Wait.Until(d => RootContainer.FindElement(By.XPath(Xpath_btntype)));
    }
}
