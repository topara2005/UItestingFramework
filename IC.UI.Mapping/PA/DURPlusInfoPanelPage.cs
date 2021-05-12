using IC.UI.Mapping.PA;
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
    public class DURPlusInfoPanelPage : BasePage
    {
        const  string successmessage = "//*//td[contains(text(),'Save was successful.  All panels were saved.')]";
        private  string XPath_PanelSubMenu;
        private string XPath_DurPlusSubMenu;

        DurPlusBaseInfoPanel base_InfoPanel;
        DurDiagnosisPrimaryPanel _primaryDiagnosis;
        DurDiagnosisSecondaryPanel _secondaryDiagnosis;
        DurStepTher1Panel   _step1Therapypanel;
        DurStepTher2Panel _step2Therapypanel;
        DurPDLPanel durPDLPanel;
        DurPlusTaxonomyPanel taxonamyPanel;
        DurPlusCoMorbidPanel _comorbidPanel;
        DurPlusGrandFather durPlusGrandFather;
        DurPlusAgePanel agePanel;
        public DURPlusInfoPanelPage(ITestVisitor visitor) : base(visitor)
        {
            XPath_PanelSubMenu = new StringBuilder().AppendFormat(XPath_RadNavigationBar, "Open Tab").ToString();
            XPath_DurPlusSubMenu = new StringBuilder().AppendFormat(XPath_RadNavigationBar, "DUR Plus").ToString();
        }

        public void LoadAgepanel()
        {
            if (agePanel == null)
            {
                agePanel = new DurPlusAgePanel(this.Visitor);
                agePanel.LoadChildrenTestElements();
            }
        }

        public void ShowAgePanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Min/Max Age Check Criteria -(A)')]//span"));
            tip.Click();
        }
          public bool SuccessMessageExists { get{
                try
                {
                    var elem = Visitor.GetElement(
                 By.XPath("//*//td[contains(text(),'Save was successful.')]"));
                }
                catch (global::System.Exception)
                {

                    return false;
                }
                return true;
            
            }
        }

        public IWebElement OpenTabSubMenu => Visitor.GetElement(
               By.XPath(XPath_PanelSubMenu));
        public IWebElement DURPlusSubMenu => Visitor.GetElement(
              By.XPath(XPath_DurPlusSubMenu));

        public void LoadPlusGrandFather()
        {
            if (durPlusGrandFather == null)
            {
                durPlusGrandFather = new DurPlusGrandFather(this.Visitor);
                durPlusGrandFather.LoadChildrenTestElements();
            }
        }

        public void ShowGrandFatherPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Grandfather Criteria -(A)')]//span"));
            tip.Click();
        }

        public void LoadPDLPanel()
        {
            if (durPDLPanel == null)
            {
                durPDLPanel = new DurPDLPanel(this.Visitor);
                durPDLPanel.LoadChildrenTestElements();
            }
        }

        public void LoadComorbidPanel()
        {
            if (_comorbidPanel == null)
            {
                _comorbidPanel = new DurPlusCoMorbidPanel(this.Visitor);
                _comorbidPanel.LoadChildrenTestElements();
            }
        }
        public void ShowComorbidPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Co-morbid Diagnosis Criteria -(A)')]//span"));
            tip.Click();
        }

        public void ShowPDLPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > PDL Criteria -(A)')]//span"));
            tip.Click();
        }

        public void LoadStep1TherapyPanel()
        {
            if (_step1Therapypanel == null)
            {
                _step1Therapypanel = new DurStepTher1Panel(this.Visitor);
                _step1Therapypanel.LoadChildrenTestElements();
            }
        }
        public void ShowStep1TherapyPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Step Therapy 1 Criteria -(A)')]//span"));
            tip.Click();
        }

        public void LoadStep2TherapyPanel()
        {
            if (_step2Therapypanel == null)
            {
                _step2Therapypanel = new DurStepTher2Panel(this.Visitor);
                _step2Therapypanel.LoadChildrenTestElements();
            }
        }
        public void ShowStep2TherapyPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Step Therapy 2 Criteria -(A)')]//span"));
            tip.Click();
        }

        public void ShowInfoPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Base Information -(A)')]//span"));
            tip.Click();
        }


        public void ShowPrimaryDiagnosisPanel()
        {
           var tip= Visitor.GetElement(
               By.XPath("//div[contains(@title,'DUR Plus > Diagnosis Criteria - Primary -(A)')]//span"));
            tip.Click();
        }

        public void ShowSecondaryDiagnosisPanel()
        {
            var tip = Visitor.GetElement(
                By.XPath("//div[contains(@title,'DUR Plus > Diagnosis Criteria - Secondary -(A)')]//span"));
            tip.Click();
        }

        public void LoadPrimaryDiagnosisPanel()
        {
            if(PrimaryDiagnosis==null)
            {
                _primaryDiagnosis = new DurDiagnosisPrimaryPanel(this.Visitor);
                PrimaryDiagnosis.LoadChildrenTestElements();
            }
        }
        public void LoadSecondaryDiagnosisPanel()
        {
            if (_secondaryDiagnosis == null)
            {
                _secondaryDiagnosis = new DurDiagnosisSecondaryPanel(this.Visitor);
                _secondaryDiagnosis.LoadChildrenTestElements();
            }
        }
        public void LoadBaseInfoPanel()
        {
            if (base_InfoPanel==null)
            {
                base_InfoPanel = new DurPlusBaseInfoPanel(this.Visitor);
                base_InfoPanel.LoadChildrenTestElements();
            }

            //iC_NavToolBar //div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rtbItem rtbBtn')]/a[@title='(ALT-s)']
        }

        public void LoadTaxonomyPanel()
        {
            if (taxonamyPanel == null)
            {
                taxonamyPanel = new DurPlusTaxonomyPanel(this.Visitor);
                taxonamyPanel.LoadChildrenTestElements();
            }

            //iC_NavToolBar //div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rtbItem rtbBtn')]/a[@title='(ALT-s)']
        }
        public void ShowTaxonomyPanel()
        {
            var tip = Visitor.GetElement(
              By.XPath("//div[contains(@title,'DUR Plus > Full Taxonomy/ Specialty  Criteria -(A)')]//span"));
            tip.Click();
        }

        public override string Url => "";

        public IWebElement MnuSave =>
            Visitor.GetElement(By.XPath("//div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rtbItem rtbBtn')]/a[@title='(ALT-s)']"));
        public IWebElement TxtDurPlusNumber => 
            Visitor.GetElement(By.XPath("//div[contains(@id,'PageNav_nav_ToolBar') and contains(@class,'RadToolBar ')]//li[contains(@class,'rtbItem rtbBtn')]/a[@title='(ALT-s)']"));

        public DurPlusBaseInfoPanel BaseInfoPanel { get => base_InfoPanel; }
        public DurDiagnosisPrimaryPanel PrimaryDiagnosis { get => _primaryDiagnosis; }

        public DurDiagnosisSecondaryPanel SecondaryDiagnosis { get => _secondaryDiagnosis; }

        public DurStepTher1Panel Step1TherapyPanel { get => _step1Therapypanel; }
        public DurStepTher2Panel Step2TherapyPanel { get => _step2Therapypanel; }
        public DurPDLPanel PDLPanel { get => durPDLPanel; }


        public DurPlusTaxonomyPanel TaxonamyPanel{ get => taxonamyPanel; }
        public DurPlusCoMorbidPanel ComorBid { get => _comorbidPanel; }
        public DurPlusGrandFather GrandFatherPanel { get => durPlusGrandFather; }
        public DurPlusAgePanel AgePanel { get => agePanel; }

        

    }
}
