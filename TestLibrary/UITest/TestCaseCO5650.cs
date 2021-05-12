using IC.UI.Mapping.PA;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestLibrary.Common;
using TestLibrary.UIMapping;

namespace TestLibrary.UITest
{
    [TestFixture(

        Author = "Bringas, Edgar",
        Description = "UI - Incorporate CT DUR+ panel code into MES PA solution",

        TestName = "CO 5650")]
    public class TestCaseCO5650 : BaseTest
    {
        MainPage mainPage = null;


        private void DeleteTestRows()
        {
            string delStatePatoPa = "delete from T_Auto_pa where dsc_auto_pa like 'test%'";
            var delete1 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_PRIM_DIAG pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete2 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_SECD_DIAG pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete3 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_PDL pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete4 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_STEP_THPY_1 pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete5 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_STEP_THPY_2 pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";


            var delete6 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_TXNMY pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete7 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_AGE pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete8 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_GRNDFTR pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";
            var delete9 = @"DELETE (SELECT pf.* FROM T_AUTO_PA_COMOR_DIAG pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%')";



            var errorcurred = false;
            try
            {
                using (var connection = new OracleConnection(base.ConnectionString))
                {
                    connection.Open();
                    var trans = connection.BeginTransaction();
                    var command1 = new OracleCommand(delete1, connection);
                    var command2 = new OracleCommand(delete2, connection);
                    var command3 = new OracleCommand(delete3, connection);
                    var command4 = new OracleCommand(delete4, connection);
                    var command5 = new OracleCommand(delete5, connection);
                    var command6 = new OracleCommand(delStatePatoPa, connection);
                    var command7 = new OracleCommand(delete6, connection);
                    var command8 = new OracleCommand(delete7, connection);
                    var command9 = new OracleCommand(delete8, connection);
                    var command10 = new OracleCommand(delete9, connection);



                    
                    // connection.Open();
                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                    command4.ExecuteNonQuery();
                    command5.ExecuteNonQuery();
                    command6.ExecuteNonQuery();
                    command7.ExecuteNonQuery();
                    command8.ExecuteNonQuery();
                    command9.ExecuteNonQuery();
                    command10.ExecuteNonQuery();
                    trans.Commit();

                }

            }
            catch (Exception e)
            {
                errorcurred = true;
            }
            Assert.IsFalse(errorcurred, "Table does not exists!!");

        }
        public TestCaseCO5650() : base(new LoginScreen())
        {

        }
        [OneTimeTearDown]
        public override void ClassCleanup()
        {
            PersistCurrentTestMetadata();
        }
        [OneTimeSetUp]
        public override void ClassInit()
        {   //  InitDriver();


            InitTestDocument("5650");

        }
        [SetUp]
        public override void SetupTest()
        {
            DeleteTestRows();
            //These tow methods allways need to be called in the method that contains   SetUp" attributte
            InitDriver();

            DoLogin();

            mainPage = new MainPage(this);
            mainPage.LoadAllMenus();
        }
        [TearDown]
        public override void TearDownTest()
        {
            CloseWebDriver();
        }


        public DURPlusPanelSearchPage NavigateToSearchPanel()
        {
            var page = new DURPlusPanelSearchPage(this);
            mainPage.MainMenu.Click();
            Thread.Sleep(500);
            mainPage.PASubMenu.Click();
            Thread.Sleep(500);
            mainPage.PASubMenuDURPlusSearch.Click();
            Thread.Sleep(7000);

            return page;
        }
        public DURPlusInfoPanelPage NavigateToInfoPanel()
        {
            var page = new DURPlusInfoPanelPage(this);
            mainPage.MainMenu.Click();
            Thread.Sleep(500);
            mainPage.PASubMenu.Click();
            Thread.Sleep(500);
            mainPage.PASubMenuDURPlusInfo.Click();
            Thread.Sleep(7000);

            return page;
        }


        [TestCaseSummary(
        TestName = "0",
     TestCaseNumber = "0.1",
     Description = "DB – Table AIM.T_AUTO_PA_PDL exist ",
     Category = "BE1SS72.01",
     TestGroupTitle = "DUR plus informtion",
     RequirementNumber = "BE1SS72.01"
     )]
        public void Test_Table_PDL_Exists()
        {
            string query = @"SELECT count(*) FROM AIM.T_AUTO_PA_PDL";
            var errorcurred = false;
            try
            {
                using (var connection = new OracleConnection(base.ConnectionString))
                {
                    var command = new OracleCommand(query, connection);
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var medicareId = reader.GetInt32(0); // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetInt32(1));
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                errorcurred = true;
            }
            Assert.IsFalse(errorcurred, "Table does not exists!!");

        }

        [TestCaseSummary(
           TestName = "1",
        TestCaseNumber = "1.1",
        Description = "Sub Menu – Option 'Dur Plus Seach' exists",
        Category = "BE1SS72.01",
        TestGroupTitle = "DUR Searh Record",
        RequirementNumber = "BE1SS72.01"
        )]
        public void Test_DurPlus_Search_Menu_Exists()
        {

            var panel = NavigateToSearchPanel();
            Thread.Sleep(5000);

            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'Related Data' option Exists");
            Thread.Sleep(300);
            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            //   var bendexMenu = panel.;
            // Thread.Sleep(500);
            //base.TakeScreenShot("Screen 2", "'BENDEX Search' option Exists");

            Assert.IsNotNull(panel, "Bendex option does not exists!!");
        }

        [TestCaseSummary(
          TestName = "1",
       TestCaseNumber = "1.2",
       Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
       Category = "BE1SS72.01",
       TestGroupTitle = "DUR plus informtion",
       RequirementNumber = "BE1SS72.01"
       )]
        public void Test_DurPlus_Search_Panel_Loads()
        {
            var panel = NavigateToSearchPanel();
            Thread.Sleep(6000);

            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'Related Data' option Exists");
            Thread.Sleep(300);
            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            //   var bendexMenu = panel.;
            // Thread.Sleep(500);
            //base.TakeScreenShot("Screen 2", "'BENDEX Search' option Exists");

            // Assert.IsNotNull(bendexMenu, "Bendex option does not exists!!");
        }

        private void FillDURPlusBaseInfoPanel(DurPlusBaseInfoPanel baseInfoPanel)
        {



            baseInfoPanel.TxtDescription.SendKeys("test");
            Thread.Sleep(350);
            baseInfoPanel.TxtEndDate.SendKeys(DateTime.Now.AddMonths(1).ToShortDateString());
            Thread.Sleep(350);
            baseInfoPanel.TxtEfectiveDate.SendKeys(DateTime.Now.AddMonths(-1).ToShortDateString());
            Thread.Sleep(350);

            baseInfoPanel.DropDownGroupIndicator.SelectByText("No");
            Thread.Sleep(500);
            // A postback occured,  we need to refresh controls by selenium
            //  baseInfoPanel.RefreshElements();
            //  Thread.Sleep(500);

            baseInfoPanel.DropDownCriteriaIndicator.SelectByText("GCN");
            Thread.Sleep(500);
            baseInfoPanel.ShowPublicHealthMiniSearchPanel();
            Thread.Sleep(6000);
            //panel.BaseInfoPanel.PublicHealthMiniSearchPanel.RefreshElements();
            //Thread.Sleep(1000);
            baseInfoPanel.PublicHealthMiniSearchPanel.BtnSearch.Click();
            Thread.Sleep(5000);
            var row = baseInfoPanel.PublicHealthMiniSearchPanel.GetRow(5);
            row.Click();
            Thread.Sleep(2000);
            //We made a postback, so, we need to refresh panel's web elements
            baseInfoPanel.RefreshElements();

            baseInfoPanel.ShowDurPlusCodeMiniSearchPanel();
            Thread.Sleep(5000);
            var row2 = baseInfoPanel.DurPlusCodeMiniSearchPanel.GetRow(1);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(2000);
            }
            baseInfoPanel.RefreshElements();

            baseInfoPanel.DropDownLetterIndicator.SelectByValue("Y");
            Thread.Sleep(500);

            baseInfoPanel.DropDownStatus.SelectByText("Active");
            Thread.Sleep(500);

            baseInfoPanel.RefreshElements();
            Thread.Sleep(500);

        }

        private void FillPrimaryDiagnosisPanel(DurDiagnosisPrimaryPanel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtDaysSupply.SendKeys("3");
            Thread.Sleep(300);
            panel.TxtDoseRatio.SendKeys("3");
            Thread.Sleep(300);
            panel.DropBoxPrimaryDiagnoisis.SelectByText("Exclude");
            Thread.Sleep(300);
            panel.DropBoxLinkIndicator.SelectByValue("Y");
            Thread.Sleep(300);

            panel.ShowDiagnosis1MiniSeachPanel();
            panel.Diagnosis1MiniSearch.DropDownICD.SelectByText("ICD-10");
            Thread.Sleep(300);
            panel.Diagnosis1MiniSearch.BtnSearch.Click();
            Thread.Sleep(3000);
            var row1 = panel.Diagnosis1MiniSearch.GetRow(1);
            if (row1 != null)
            {
                row1.Click();
                Thread.Sleep(2000);
            }
            panel.RefreshElements();
            panel.ShowDiagnosis2MiniSeachPanel();
            panel.Diagnosis2MiniSearch.DropDownICD.SelectByText("ICD-10");
            Thread.Sleep(300);
            panel.Diagnosis2MiniSearch.BtnSearch.Click();
            Thread.Sleep(3000);
            var row2 = panel.Diagnosis2MiniSearch.GetRow(2);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(2000);
            }
            panel.RefreshElements();
            panel.ShowDiagnosis3MiniSeachPanel();
            panel.Diagnosis3MiniSearch.DropDownICD.SelectByText("ICD-10");
            Thread.Sleep(300);
            panel.Diagnosis3MiniSearch.BtnSearch.Click();
            Thread.Sleep(3000);
            var row3 = panel.Diagnosis3MiniSearch.GetRow(3);
            if (row3 != null)
            {
                row3.Click();
                Thread.Sleep(2000);
            }
            panel.RefreshElements();

            /*   panel.ShowDiagnosisTypeMiniSearch();
               panel.DiagnosisTypeMiniSearch.BtnSearch.Click();
               Thread.Sleep(3000);
               var row4 = panel.DiagnosisTypeMiniSearch.GetRow(1);
               if (row4 != null)
               {
                   row4.Click();
                   Thread.Sleep(2000);
               }
               panel.RefreshElements();*/
            Thread.Sleep(3000);

        }


        private void FillSecondaryDiagnosisPanel(DurDiagnosisSecondaryPanel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtDaysSupply.SendKeys("4");
            Thread.Sleep(300);
            panel.TxtDoseRatio.SendKeys("4");
            Thread.Sleep(300);
            panel.DropBoxPrimaryDiagnoisis.SelectByText("Include");
            Thread.Sleep(300);
            panel.DropBoxLinkIndicator.SelectByValue("N");
            Thread.Sleep(300);

            panel.ShowDiagnosis1MiniSeachPanel();
            panel.Diagnosis1MiniSearch.DropDownICD.SelectByText("ICD-9");
            Thread.Sleep(300);
            panel.Diagnosis1MiniSearch.BtnSearch.Click();
            Thread.Sleep(3000);
            var row1 = panel.Diagnosis1MiniSearch.GetRow(1);
            if (row1 != null)
            {
                row1.Click();
                Thread.Sleep(2000);
            }
            panel.RefreshElements();
            panel.ShowDiagnosis2MiniSeachPanel();
            panel.Diagnosis2MiniSearch.DropDownICD.SelectByText("ICD-9");
            Thread.Sleep(300);
            panel.Diagnosis2MiniSearch.BtnSearch.Click();
            Thread.Sleep(3000);
            var row2 = panel.Diagnosis2MiniSearch.GetRow(2);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(2000);
            }
            panel.RefreshElements();
            panel.ShowDiagnosis3MiniSeachPanel();
            panel.Diagnosis3MiniSearch.DropDownICD.SelectByText("ICD-9");
            Thread.Sleep(300);
            panel.Diagnosis3MiniSearch.BtnSearch.Click();
            Thread.Sleep(3000);
            var row3 = panel.Diagnosis3MiniSearch.GetRow(3);
            if (row3 != null)
            {
                row3.Click();
                Thread.Sleep(2000);
            }
            panel.RefreshElements();

            /*   panel.ShowDiagnosisTypeMiniSearch();
               panel.DiagnosisTypeMiniSearch.BtnSearch.Click();
               Thread.Sleep(3000);
               var row4 = panel.DiagnosisTypeMiniSearch.GetRow(1);
               if (row4 != null)
               {
                   row4.Click();
                   Thread.Sleep(2000);
               }
               panel.RefreshElements();*/
            Thread.Sleep(3000);

        }

        private void FillStepTherapy1Panel(DurStepTher1Panel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtCriteriaCount.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtNumDays.SendKeys("6");
            Thread.Sleep(200);
            panel.TxtDurationDays.SendKeys("8");
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("2.3");
            Thread.Sleep(200);
            panel.DropLine1DrugTherapy.SelectByText("Include");
            Thread.Sleep(300);
            panel.DropBoxGroupIndicator.SelectByValue("G");
            Thread.Sleep(300);
            panel.DropBoxLinkIndicator.SelectByValue("Y");
            Thread.Sleep(300);
            panel.ShowDiagnosis1MiniSeachPanel();
            Thread.Sleep(2000);
            var row1 = panel.Criteria1MiniSearch.GetRow(3);
            if (row1 != null)
            {
                row1.Click();
                Thread.Sleep(3000);
            }
            panel.ShowDiagnosis2MiniSeachPanel();
            Thread.Sleep(2000);
            var row2 = panel.Criteria2MiniSearch.GetRow(1);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(3000);
            }
            panel.ShowDiagnosis3MiniSeachPanel();
            Thread.Sleep(2000);
            var row3 = panel.Criteria3MiniSearch.GetRow(5);
            if (row3 != null)
            {
                row3.Click();
                Thread.Sleep(3000);
            }
        }
        private void FillStepTherapy2Panel(DurStepTher2Panel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtCriteriaCount.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtNumDays.SendKeys("6");
            Thread.Sleep(200);
            panel.TxtDurationDays.SendKeys("8");
            Thread.Sleep(200);
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("2.3");
            Thread.Sleep(200);
            panel.DropLine2DrugTherapy.SelectByText("Include");
            Thread.Sleep(300);
            panel.DropBoxGroupIndicator.SelectByValue("G");
            Thread.Sleep(300);
            panel.DropBoxLinkIndicator.SelectByValue("Y");
            Thread.Sleep(300);
            panel.ShowDiagnosis1MiniSeachPanel();
            Thread.Sleep(2000);
            var row1 = panel.Criteria1MiniSearch.GetRow(3);
            if (row1 != null)
            {
                row1.Click();
                Thread.Sleep(3000);
            }
            panel.ShowDiagnosis2MiniSeachPanel();
            Thread.Sleep(2000);
            var row2 = panel.Criteria2MiniSearch.GetRow(1);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(3000);
            }
            panel.ShowDiagnosis3MiniSeachPanel();
            Thread.Sleep(2000);
            var row3 = panel.Criteria3MiniSearch.GetRow(5);
            if (row3 != null)
            {
                row3.Click();
                Thread.Sleep(3000);
            }

        }
        private void FillPDLPanel(DurPDLPanel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtCriteriaCount.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtNumDays.SendKeys("6");
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("3.25");
            Thread.Sleep(200);
            panel.TxtDurationDays.SendKeys("14");
            Thread.Sleep(200);
            panel.DropBoxLinkIndicator.SelectByValue("Y");
            Thread.Sleep(300);
        }


        private void FillTaxonomyPanel(DurPlusTaxonomyPanel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            panel.RefreshElements();
            panel.TxtDaysSupply.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("32");
            Thread.Sleep(200);
            panel.ShowTaxonomy1MiniSeachPanel();
            Thread.Sleep(2000);
            panel.Taxonomy1MiniSearch.BtnSearch.Click();
            Thread.Sleep(2000);
            var row1 = panel.Taxonomy1MiniSearch.GetRow(3);
            if (row1 != null)
            {
                row1.Click();
                Thread.Sleep(3000);
            }
            panel.ShowTaxonomy2MiniSeachPanel();
            Thread.Sleep(2000);
            panel.Taxonomy2MiniSearch.BtnSearch.Click();
            Thread.Sleep(2000);
            var row2 = panel.Taxonomy2MiniSearch.GetRow(5);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(3000);
            }
            Thread.Sleep(2000);
            //panel.Taxonomy3MiniSearch.BtnSearch.Click();
            //Thread.Sleep(2000);
            //var row3 = panel.Taxonomy3MiniSearch.GetRow(1);
            //if (row3 != null)
            //{
            //    row3.Click();
            //    Thread.Sleep(3000);
            //}
        }

        private void FillComorbidPanel(DurPlusCoMorbidPanel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            panel.RefreshElements();
            panel.TxtDaysSupply.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("32");
            Thread.Sleep(200);
            panel.ShowDiagnosis1MiniSeachPanel();
            Thread.Sleep(2000);
            panel.Diagnosis1MiniSearch.BtnSearch.Click();
            Thread.Sleep(2000);
            var row1 = panel.Diagnosis1MiniSearch.GetRow(3);
            if (row1 != null)
            {
                row1.Click();
                Thread.Sleep(3000);
            }
            panel.ShowDiagnosis2MiniSeachPanel();
            Thread.Sleep(2000);
            panel.Diagnosis2MiniSearch.BtnSearch.Click();
            Thread.Sleep(2000);
            var row2 = panel.Diagnosis2MiniSearch.GetRow(5);
            if (row2 != null)
            {
                row2.Click();
                Thread.Sleep(3000);
            }
            Thread.Sleep(2000);
            //panel.Taxonomy3MiniSearch.BtnSearch.Click();
            //Thread.Sleep(2000);
            //var row3 = panel.Taxonomy3MiniSearch.GetRow(1);
            //if (row3 != null)
            //{
            //    row3.Click();
            //    Thread.Sleep(3000);
            //}
        }


        private void FillGrandFatherPanel(DurPlusGrandFather panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtDaysSupply.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtNumberDays.SendKeys("6");
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("3.25");
            Thread.Sleep(200);
         
            panel.DropBoxLinkIndicator.SelectByValue("Y");
            Thread.Sleep(300);
        }

        private void FillAgePanel(DurPlusAgePanel panel)
        {
            panel.BtnAdd.Click();
            Thread.Sleep(1000);
            //as we executed a postback, wwe need to update selenium with the new DOM
            panel.RefreshElements();
            panel.TxtMinNumber.SendKeys("5");
            Thread.Sleep(200);
            panel.TxtMaxNumber.SendKeys("20");
            Thread.Sleep(200);
            panel.TxtNumberDays.SendKeys("6");
            Thread.Sleep(200);
            panel.TxtDoseRatio.SendKeys("3.25");
            Thread.Sleep(200);

            panel.DropBoxLinkIndicator.SelectByValue("Y");
            Thread.Sleep(300);
        }

        [TestCaseSummary(
        TestName = "1",
     TestCaseNumber = "1.1",
     Description = "Sub Menu – Option 'Dur Plus Seach' exists",
     Category = "BE1SS72.01",
     TestGroupTitle = "DUR Searh Record",
     RequirementNumber = "BE1SS72.01"
     )]

        public void Test_DurPlus_Info_Panel_Base_And_DiagnosisPrimary_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Diagnosis Criteria - Secondary"];
            menuItem.Click();
            Thread.Sleep(2000);



            panel.ShowInfoPanel();
            Thread.Sleep(1000);
            panel.LoadBaseInfoPanel();
            Thread.Sleep(2000);


            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            
            panel.LoadPrimaryDiagnosisPanel();
            panel.ShowPrimaryDiagnosisPanel();
            Thread.Sleep(1000);
            FillPrimaryDiagnosisPanel(panel.PrimaryDiagnosis);



            /*
            panel.ShowSecondaryDiagnosisPanel();
            Thread.Sleep(1000);
            panel.LoadSecondaryDiagnosisPanel();
            Thread.Sleep(1000);
            FillSecondaryDiagnosisPanel(panel.SecondaryDiagnosis);

            panel.ShowStep1TherapyPanel();
            Thread.Sleep(1000);
            panel.LoadStep1TherapyPanel();
            Thread.Sleep(1000);
            FillStepTherapy1Panel(panel.Step1TherapyPanel);

            panel.ShowStep2TherapyPanel();
            Thread.Sleep(1000);
            panel.LoadStep2TherapyPanel();
            Thread.Sleep(1000);
            FillStepTherapy2Panel(panel.Step2TherapyPanel);


            panel.ShowPDLPanel();
            Thread.Sleep(1000);
            panel.LoadPDLPanel();
            Thread.Sleep(1000);
            FillPDLPanel(panel.PDLPanel);
            */
            panel.MnuSave.Click();
            Thread.Sleep(10000000);


            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(500);
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'Related Data' option Exists");
            Thread.Sleep(300);
            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            //   var bendexMenu = panel.;
            // Thread.Sleep(500);
            //base.TakeScreenShot("Screen 2", "'BENDEX Search' option Exists");

            // Assert.IsNotNull(panel, "Bendex option does not exists!!");
        }


      
        public void Test_DurPlus_Info_Panel_Base_And_DiagnosisSecondary_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Diagnosis Criteria - Secondary"];
            menuItem.Click();
            Thread.Sleep(2000);


            panel.ShowInfoPanel();
            Thread.Sleep(1000);
            panel.LoadBaseInfoPanel();
            Thread.Sleep(2000);
            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.ShowSecondaryDiagnosisPanel();
            Thread.Sleep(1000);
            panel.LoadSecondaryDiagnosisPanel();
            Thread.Sleep(1000);
            FillSecondaryDiagnosisPanel(panel.SecondaryDiagnosis);

            panel.MnuSave.Click();
            Thread.Sleep(10000000);
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'Related Data' option Exists");
            Thread.Sleep(300);
            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            //   var bendexMenu = panel.;
            // Thread.Sleep(500);
            //base.TakeScreenShot("Screen 2", "'BENDEX Search' option Exists");

            // Assert.IsNotNull(bendexMenu, "Bendex option does not exists!!");
        }
        [TestCaseSummary(
       TestName = "2",
    TestCaseNumber = "1.2",
    Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
    Category = "BE1SS72.01",
    TestGroupTitle = "DUR plus informtion",
    RequirementNumber = "BE1SS72.01"
    )]
        public void Test_DurPlus_Info_Panel_Base_And_StepTherapy1Criteria_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Diagnosis Criteria - Secondary"];
            menuItem.Click();
            Thread.Sleep(2000);


            panel.ShowInfoPanel();
            Thread.Sleep(1000);
            panel.LoadBaseInfoPanel();
            Thread.Sleep(2000);
            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.ShowStep1TherapyPanel();
            Thread.Sleep(1000);
            panel.LoadStep1TherapyPanel();
            Thread.Sleep(1000);
            FillStepTherapy1Panel(panel.Step1TherapyPanel);

            panel.MnuSave.Click();
            Thread.Sleep(5000);
            var res = CheckForRecordCount("T_AUTO_PA_STEP_THPY_1");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_STEP_THPY_1 does not exists");
        }

        [TestCaseSummary(
       TestName = "3",
    TestCaseNumber = "1.2",
    Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
    Category = "BE1SS72.01",
    TestGroupTitle = "DUR plus informtion",
    RequirementNumber = "BE1SS72.01"
    )]
        public void Test_DurPlus_Info_Panel_Base_And_StepTherapy2Criteria_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Diagnosis Criteria - Secondary"];
            menuItem.Click();
            Thread.Sleep(2000);


            panel.ShowInfoPanel();
            Thread.Sleep(1000);
            panel.LoadBaseInfoPanel();
            Thread.Sleep(2000);
            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.ShowStep2TherapyPanel();
            Thread.Sleep(1000);
            panel.LoadStep2TherapyPanel();
            Thread.Sleep(1000);
            FillStepTherapy2Panel(panel.Step2TherapyPanel);

            panel.MnuSave.Click();
            Thread.Sleep(5000);
            var res = CheckForRecordCount("T_AUTO_PA_STEP_THPY_2");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_STEP_THPY_2 does not exists");
        }

        [TestCaseSummary(
     TestName = "4",
  TestCaseNumber = "1.2",
  Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
  Category = "BE1SS72.01",
  TestGroupTitle = "DUR plus informtion",
  RequirementNumber = "BE1SS72.01"
  )]
        public void Test_DurPlus_Info_Panel_Base_And_PDLCriteria_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Diagnosis Criteria - Secondary"];
            menuItem.Click();
            Thread.Sleep(2000);


            panel.ShowInfoPanel();
            Thread.Sleep(1000);
            panel.LoadBaseInfoPanel();
            Thread.Sleep(2000);
            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.ShowPDLPanel();
            Thread.Sleep(1000);
            panel.LoadPDLPanel();
            Thread.Sleep(1000);
            FillPDLPanel(panel.PDLPanel);

            panel.MnuSave.Click();
            Thread.Sleep(5000);
            var res = CheckForRecordCount("T_AUTO_PA_PDL");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_PDL dows not exists");
        }

        [TestCaseSummary(
   TestName = "5",
TestCaseNumber = "1.2",
Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
Category = "BE1SS72.01",
TestGroupTitle = "DUR plus informtion",
RequirementNumber = "BE1SS72.01"
)]
        public void Test_DurPlus_Info_Panel_Base_And_Taxonomy_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Full Taxonomy/ Specialty  Criteria"];
            //  menuItem.Click();
            //  Thread.Sleep(2000);

            panel.LoadBaseInfoPanel();
            Thread.Sleep(1000);
            panel.ShowInfoPanel();
            Thread.Sleep(1000);
         
            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.LoadTaxonomyPanel();
            Thread.Sleep(1000);
            panel.ShowTaxonomyPanel();
            Thread.Sleep(1000);
            FillTaxonomyPanel(panel.TaxonamyPanel);

            panel.MnuSave.Click();
            Thread.Sleep(5000);
            var res = CheckForRecordCount("T_AUTO_PA_TXNMY");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_TXNMY dows not exists");
        }

        [TestCaseSummary(
TestName = "6",
TestCaseNumber = "1.2",
Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
Category = "BE1SS72.01",
TestGroupTitle = "DUR plus informtion",
RequirementNumber = "BE1SS72.01"
)]
        public void Test_DurPlus_Info_Panel_Base_And_Comorbid_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Full Taxonomy/ Specialty  Criteria"];
            //  menuItem.Click();
            //  Thread.Sleep(2000);

            panel.LoadBaseInfoPanel();
            Thread.Sleep(1000);
            panel.ShowInfoPanel();
            Thread.Sleep(1000);

            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.LoadComorbidPanel();
            Thread.Sleep(1000);
            panel.ShowComorbidPanel();
            Thread.Sleep(1000);
            FillComorbidPanel(panel.ComorBid);

            panel.MnuSave.Click();
            Thread.Sleep(5000);

            var res = CheckForRecordCount("T_AUTO_PA_COMOR_DIAG");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_COMOR_DIAG does not exists");
        }

        [TestCaseSummary(
TestName = "7",
TestCaseNumber = "1.2",
Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
Category = "BE1SS72.01",
TestGroupTitle = "DUR plus informtion",
RequirementNumber = "BE1SS72.01"
)]
        public void Test_DurPlus_Info_Panel_Base_And_GrandFather_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Grandfather Criteria"];
            //  menuItem.Click();
            //  Thread.Sleep(2000);

            panel.LoadBaseInfoPanel();
            Thread.Sleep(1000);
            panel.ShowInfoPanel();
            Thread.Sleep(1000);

            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.LoadPlusGrandFather();
            Thread.Sleep(1000);
            panel.ShowGrandFatherPanel();
            Thread.Sleep(1000);
            FillGrandFatherPanel(panel.GrandFatherPanel);

            panel.MnuSave.Click();
            Thread.Sleep(5000);
            var res = CheckForRecordCount("T_AUTO_PA_GRNDFTR");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_GRNDFTR does not exists");
            // Thread.Sleep(10000000);
        }
        [TestCaseSummary(
TestName = "8",
TestCaseNumber = "1.2",
Description = "Sub Menu – Option 'Dur Plus Search' loads Search Panel",
Category = "BE1SS72.01",
TestGroupTitle = "DUR plus informtion",
RequirementNumber = "BE1SS72.01"
)]
        public void Test_DurPlus_Info_Panel_Base_And_Age_Save()
        {
            var panel = NavigateToInfoPanel();
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Base Information' option", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            panel.OpenTabSubMenu.Click();
            Thread.Sleep(500);
            panel.DURPlusSubMenu.Click();
            Thread.Sleep(500);
            var items = panel.GetSubMenuItems("DUR Plus");
            var menuItem = items["Min/Max Age Check Criteria"];
            //  menuItem.Click();
            //  Thread.Sleep(2000);

            panel.LoadBaseInfoPanel();
            Thread.Sleep(1000);
            panel.ShowInfoPanel();
            Thread.Sleep(1000);

            FillDURPlusBaseInfoPanel(panel.BaseInfoPanel);
            Thread.Sleep(1000);

            panel.LoadAgepanel();
            Thread.Sleep(1000);
            panel.ShowAgePanel();
            Thread.Sleep(1000);
            FillAgePanel(panel.AgePanel);

            panel.MnuSave.Click();
            Thread.Sleep(5000);
            var res = CheckForRecordCount("T_AUTO_PA_AGE");
            Assert.IsTrue(res && panel.SuccessMessageExists, "A record in table T_AUTO_PA_AGE dows not exists");
          //  Thread.Sleep(10000000);
        }


        private bool CheckForRecordCount(string tableName)
        {

            string delStatePatoPa = $"SELECT count(*) FROM {tableName} pf INNER JOIN T_AUTO_PA pr ON pf.sak_auto_pa = pr.sak_auto_pa  where dsc_auto_pa like 'test%'";
            var count = 0;

            try
            {
                using (var connection = new OracleConnection(base.ConnectionString))
                {
                    connection.Open();
                    var command1 = new OracleCommand(delStatePatoPa, connection);

                    var reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        count= reader.GetInt32(0);
                    }
                
                 

                }
               

            }
            catch (Exception e)
            {
               
            }
            return count > 0;
        }

    }
}
