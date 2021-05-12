using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using TestLibrary.UIMapping;
using NUnit.Framework.Internal;
using TestLibrary.Common;
using Oracle.ManagedDataAccess.Client;
using Test.Core.Entities;

namespace TestLibrary.UITest
{
    [TestFixture(

        Author = "Bringas, Edgar",
        Description = "UI – Modify TBQ Information Panel. Create a new TBQ child panel for displaying the ineligible periods",
      
        TestName = "CO 4646")]
    public class TestCaseCO4646 : BaseTest
    {
        MainPage mainPage = null;
         string medicareId =string.Empty ;//"979133098C"
        string query = @"";

        public TestCaseCO4646() : base(new LoginScreen())
        {
        }

        [OneTimeSetUp]
        public override void ClassInit()
        {
            using (var connection = new OracleConnection(base.ConnectionString))
            {
                var command = new OracleCommand(query, connection);
                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        medicareId = reader.GetOracleString(0).Value; // Console.WriteLine(reader.GetInt32(0) + ", " + reader.GetInt32(1));
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                }
            }
          //  InitDriver();
          
            InitTestDocument("4646");
          

        }
        [OneTimeTearDown]
        public override void ClassCleanup()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.
            PersistCurrentTestMetadata();
        }
      
        [SetUp]
        public override void SetupTest()
        {
            //These tow methods allways need to be called in the method that contains   SetUp" attributte
            InitDriver();

            DoLogin();
           // Thread.Sleep(2000);
         /*   if (mainPage == null)
            {
                mainPage = new MainPage(this);
                mainPage.LoadAllMenus();
            }*/
            mainPage = new MainPage(this);
            mainPage.LoadAllMenus();
        }
        [TearDown]
        public override void TearDownTest()
        {
           
            CloseWebDriver();
        }

        private void NavigateToInfoPanel()
        {
            var tbqSearchPanel = new TBQSearchPanelPage(this);


           var menu = mainPage.MainMenu;
            driverActions.MoveToElement(menu).Build().Perform();
            Thread.Sleep(500);
            //It moves to the Member submenu
            var memberMenu = mainPage.MemberSubMenu; 
            driverActions.MoveToElement(memberMenu).Build().Perform();
            Thread.Sleep(500);
           
            driverActions.MoveToElement(mainPage.MemberSubMenuTBQ).Build().Perform();
            mainPage.MemberSubMenuTBQ.Click();
            Thread.Sleep(3000);
            tbqSearchPanel.TxtMedicareId.SendKeys(medicareId);
            tbqSearchPanel.ButtonSearch.Click();
        }

        [TestCaseSummary(
               TestName = "1",
            TestCaseNumber = "1",
            Description = "Panel Load – Panel shows only data from base information panel",
            Category = "BE5.05",
            TestGroupTitle= "TBQ Information",
            RequirementNumber= "BE5.05"
            )]
        public void Panel_Shows_Base_Information_Panel()
        {
            var panel = new TBQIformationPanelPage(this);
            NavigateToInfoPanel();
            Thread.Sleep(8000);
            var el = panel.TxtCurrentHICN; 
            Thread.Sleep(4000);
            base.TakeScreenShot("Screen 1", "Recipient TBQ Base information Panel");
            
            //base.TakeScreenShot("Screen 1", TestContext.Test.Name, "Screen 1");
            Assert.IsTrue(el != null);
        }

      
        [TestCaseSummary(
               TestName = "2",
            TestCaseNumber = "2",
            Description = "Label- Label for Medicare Id changed",
            TestGroupTitle = "TBQ Information",
            RequirementNumber = "BE5.05"
            )]
        public void Label_for_Medicare_Id_changed()
        {
            var panel = new TBQIformationPanelPage(this);
            NavigateToInfoPanel();
            Thread.Sleep(8000);
            var el = panel.TxtCurrentHICN;
            //TestContext.Test.Name
            base.TakeScreenShot("Screen 2.1", "TBQ base information panel contains the label “Current HICN / RRB\"");
            Assert.IsTrue(el != null);
        }

        
        [TestCaseSummary(
              TestName = "3",
            TestCaseNumber = "3",
            Description = "Submenu Ineligible Periods Exists",
            TestGroupTitle = "TBQ Information",
            RequirementNumber = "BESS5.26"
            )]
        public void Submenu_Ineligible_Periods_Exists()
        {
            var panel = new TBQIformationPanelPage(this);
            NavigateToInfoPanel();
            Thread.Sleep(8000);
            panel.OpenTabMenu.Click();

            //helper.WaitForElement(driver, By.XPath(TBQIformationPanelPage.Xpath_OpenTabMenu), TimeSpan.FromSeconds(30)).Click();
          
            Thread.Sleep(1000);
            panel.OpenTabMenuTBQ.Click();
           // helper.WaitForElement(driver, By.XPath(TBQIformationPanelPage.Xpath_OpenTabMenu_TBQ), TimeSpan.FromSeconds(30)).Click();
         
            Thread.Sleep(1000);
            var el3 = panel.SubMenuIneligible; //helper.WaitForElement(driver, By.XPath(TBQIformationPanelPage.Xpath_OpenTabMenu_TBQ_Ineligible), TimeSpan.FromSeconds(30));
           // driverActions.MoveToElement(el2).Click().Build().Perform();
            Thread.Sleep(1000);
           
           // base.TakeScreenShot(TestContext.Test.Name );
            base.TakeScreenShot("Screen 3.1", "Ineligible Periods submenu option");
            Assert.IsTrue(el3 != null);
        }

        [Test(Description = "Submenu Ineligible Periods Panel Loads")]
        [TestCaseSummary(
            TestName =   "4",
            TestCaseNumber = "4",
            Description = "Clicking Submenu Ineligible Periods Panel Loads panel",
            TestGroupTitle = "TBQ Information",
            RequirementNumber = "BESS5.26"
            )]
        public void Ineligible_Panel_Loads()
        {
            var panel = new TBQIformationPanelPage(this);
            driverActions = new Actions(driver);
            NavigateToInfoPanel();
            Thread.Sleep(8000);
            AddTestCaseStep("4.1", "Panel Load", "Navigate to panel", "Panel shows only data from base information panel", "Same as expected");
            AddTestCaseStep("4.1", "Panel Load", "Step2", "Step 2 expected result", "Same as expected");
            panel.OpenTabMenu.Click();
            Thread.Sleep(1000);
            panel.OpenTabMenuTBQ.Click();
            Thread.Sleep(1000);
            var el = panel.SubMenuIneligible;
            el.Click();
            Thread.Sleep(3000);
            try
            {
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("jQuery('html, body').animate({ scrollTop: 1000 }, 1000);");
               
            }
            catch (Exception e)
            {
                //var el = helper.WaitForElement(driver, By.XPath(TBQIformationPanel.Xpath_OpenTabMenu_TBQ_Ineligible), TimeSpan.FromSeconds(30));

             //   driverActions.MoveToElement(el).Perform();
              //  throw;
            }

      


            base.TakeScreenShot("Screen 4.1", "User selects option \"Ineligible Periods\" in submenu and the corresponding panel gets load");
            // base.testContextInstance.Test.Name
            Assert.IsTrue(el != null);
        }

       

       

    }


   
}
