using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Core.Entities;
using TestLibrary.Common;
using TestLibrary.UIMapping;

namespace TestLibrary.UITest
{

    [TestFixtureCO(

        Author = "Bringas, Edgar",
        Description = "UI – Create the BENDEX panel in BuyIn Search",
        CONumber="4895",
        TestName = "CO 4895")]
    public class TestCaseCO4895 : BaseTest
    {
        MainPage mainPage = null;
        string medicareId = "239996372O";
        string countQuery = "select count(*), ID_MEDICARE from t_buy_bendex where SAK_BENDEX=1 group by ID_MEDICARE";
        string sqlQuery = @"insert into t_buy_bendex
(SAK_BENDEX, SAK_RECIP, DTE_LAST_UPDATE, ID_MEDICARE, NAM_LAST, NAM_FIRST, NAM_MIDDLE, NAM_SUFFIX, CDE_SEX, ADR_PAYEE_1, ADR_PAYEE_2, 
ADR_PAYEE_3, ADR_PAYEE_4, ADR_PAYEE_5, ADR_PAYEE_6, ADR_ZIP_CODE, CDE_STATE_COUNTY, CDE_DIRECT_DEP, CDE_AGENCY, CDE_SOURCE, CDE_AID_CAT, 
CDE_DWI, IND_EARN_REQ, DTE_DEATH, IND_PROOF_DEATH, CTL_DATA_STATE, CDE_IEVS_AGENCY, CDE_OLD_BIC, NUM_SSN, CDE_PMT_STATUS, DTE_INITIAL_ENTITLE,
DTE_CUR_ENTITLE, DTE_DISABILITY_ONSET, DTE_BIRTH, IND_PROOF_BIRTH, CDE_COMMUNICATION, DTE_EFFECTIVE, AMT_BENEFIT, AMT_PAYABLE, AMT_NET,
NUM_BOAN, IND_MED_STAT, NUM_SSN_DUAL, CDE_BIC_DUAL, NUM_SSN_TRIPLE, CDE_BIC_TRIPLE, IND_DUAL_ENTITLE, NUM_SSN_XREF, CDE_BIC_XREF, DTE_SSA_PROC,
IND_PMT_CYCLE, AMT_PMT_RETRO, DTE_OP_RECV_END, DTE_SSI_ENTR_TERM, CDE_SSI_STATUS, NUM_RRB, IND_RRB_STATUS, DTE_RRB_START, DTE_RRB_END, 
AMT_OP_DEDUCT, AMT_OP_SSI, AMT_GARNISHMENT, DTE_HI_CONTS, NUM_HI_ENTRIES, AMT_HI_PREMIUM, DTE_HI_START_1, DTE_HI_END_1, CDE_HI_BASIS_1, 
CDE_HI_NON_1,IND_HI_TYPE_1, CDE_HI_PERIOD_1, DTE_HI_START_2, DTE_HI_END_2, CDE_HI_BASIS_2, CDE_HI_NON_2 , DTE_HI_START_3, DTE_HI_END_3,
CDE_HI_BASIS_3, CDE_HI_NON_3, CDE_HI_TP_PAYER, DTE_HI_TP_START, DTE_HI_TP_STOP, CDE_HI_TP_CAT,  AMT_SMI_PREMIUM, DTE_SMI_CONTS,NUM_SMI_ENTRIES, 
DTE_SMI_START_1, DTE_SMI_END_1, CDE_SMI_BASIS_1,CDE_SMI_NON_1,CDE_SMI_PERIOD_1,DTE_SMI_START_2,DTE_SMI_END_2,CDE_SMI_BASIS_2,CDE_SMI_NON_2, 
DTE_SMI_START_3,DTE_SMI_END_3,CDE_SMI_BASIS_3,CDE_SMI_NON_3,CDE_SMI_TP_PAYER,DTE_SMI_TP_START,DTE_SMI_TP_STOP, CDE_SMI_TP_CAT,AMT_SMI_VAR_PREMIUM,
DTE_SMI_VAR_START,DTE_SMI_VAR_STOP,DTE_CITZ_START_1,DTE_CITZ_STOP_1,CDE_CITZ_COUNTRY_1, IND_USCITZ_PROOF_1,DTE_CITZ_START_2,DTE_CITZ_STOP_2,
CDE_CITZ_COUNTRY_2,IND_USCITZ_PROOF_2,DTE_CITZ_START_3,DTE_CITZ_STOP_3, CDE_CITZ_COUNTRY_3, IND_USCITZ_PROOF_3
)
values
(1,  3850,  201009,  '239996372O',  'PATIENCE',  'GOODROW',  'X',   'Mr', 'F',    '8191 WRRGT MKPI TF',  'yyyyyy',   'adde 3',   'affr 4',  'addr 5',  'addr 6', '84610',  '01',  
 'Y', 'CPA',  'M',   'Y' ,   'Y', 'N',  0,  'N',  '00000172',  'W',  'de',  '839950516',  'DE',  201208,   201608,  201208, 
 19930226,  'N',  'COMMUN' ,   20010117,  5200 ,  2100,   230,  '23544',  'Y',  '505685707',   'DD',   'tripleA',   '2S',  'N',  'SSN33',  'XR',
 20010117,  'Y',   5200 ,  201001,   201002 ,  'Y','23544',  'Y',  201201,  201201,  530.2,  518.3,  519.4,
 201501,  2,  580.2,  201211,   201212,    'Y', 'D', 'T',   'T',  201011,   201211,  'N',  'Y',  201801,   201812,
 'Y',   'L',  'W',   201011,  201211,   'A',  3.2,  201801,1,  200801,  200812,  'A',  'B',  'I', 201011,  201211,  'A',  'A',   201012,  201211, 
  'A',  'B',  'I',  200901,  200912,  'A',   230.3, 201201,  201501,   201810,  201901,  'AZ',  'Y',  200901,  201701,  'AZ',  'A',  201801,  201812,  'EP', 'U'
);";
        public TestCaseCO4895() : base(new LoginScreen())
        {

        }
        [OneTimeTearDown]
        public override void ClassCleanup()
        {
            PersistCurrentTestMetadata();
        }
        [OneTimeSetUp]
        public override void ClassInit()
        {
            using (var connection = new OracleConnection(base.ConnectionString))
            {
                var count = 0;
                connection.Open();
                var selectCommand= new OracleCommand(countQuery, connection);
                OracleDataReader reader = selectCommand.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                        if (count > 0)
                        {
                            medicareId = reader.GetOracleString(1).Value;
                        }
                      
                    }
                }
                finally
                {
                    // always call Close when done reading.
                    reader.Close();
                    connection.Close();
                }
                if (count == 0)
                {
                    try
                    {
                        connection.Open();
                        using (var command = new OracleCommand(sqlQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {


                    }
                    finally {
                        connection.Close();
                    }
                  
                      
                }
              
              
               
            }
            InitTestDocument("4895");
        }
        [SetUp]
        public override void SetupTest()
        {
          
            InitDriver();
          
           
        }


        public BuyInPage NavigateToPanel()
        {
            var page = new BuyInPage(this);
            DoLogin();
            mainPage = new MainPage(this);
            mainPage.LoadAllMenus();
            mainPage.MainMenu.Click();
            Thread.Sleep(300);
            mainPage.MemberSubMenu.Click();
            Thread.Sleep(300);
            mainPage.MemberSubMenuBuyIn.Click();
            Thread.Sleep(7000);
            page.TabMenu.Click();
            Thread.Sleep(300);
            page.TabMenuRelatedData.Click();
            Thread.Sleep(300);
            return page;
        }
        [TestCaseSummary(
             TestName = "1",
          TestCaseNumber = "1.1",
          Description = "Sub Menu – Option 'BENDEX Search' exists",
          Category = "BE1SS72.01",
          TestGroupTitle = "BENDEX Record",
          RequirementNumber = "BE1SS72.01"
          )]
        public void Test_Bendex_Menu_Exists()
        {
            var page = NavigateToPanel();
            Thread.Sleep(7000);
           
           
            AddTestCaseStep("1.1", "Sub Menu", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'Related Data' option Exists");
            Thread.Sleep(300);
            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            var bendexMenu = page.TabMenuRelatedDataBendexSearch;
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 2", "'BENDEX Search' option Exists");

            Assert.IsNotNull(bendexMenu, "Bendex option does not exists!!");
        }

        [TestCaseSummary(
            TestName = "2",
         TestCaseNumber = "2.1",
         Description = "Panel Loads – Clicking on 'BENDEX Search' option loads panel Bendex Search",
         Category = "BE1SS72.01",
         TestGroupTitle = "BENDEX Record",
         RequirementNumber = "BE1SS72.01"
         )]
        public void Test_Bendex_Panel_Loads()
        {
            var page = NavigateToPanel();
            Thread.Sleep(7000);
     

            AddTestCaseStep("2.1", "Panel Loads", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(200);
        
            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            var bendexMenu = page.TabMenuRelatedDataBendexSearch;
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'BENDEX Search' option Exists");
            AddTestCaseStep("", "Panel Loads", "User Clicks on Option 'BENDEX Search'", "'Bendex Search' panel is load", "Same as expected");
            Thread.Sleep(2000);
            base.TakeScreenShot("Screen 2", "'BENDEX Search' panel loads");
            Assert.IsNotNull(bendexMenu, "Bendex panel does not exists!!");
        }


        [TestCaseSummary(
      TestName = "3",
   TestCaseNumber = "3.1",
   Description = "Panel Loads – Entering a Medica ID in search textbox loads bendex info",
   Category = "BE1SS72.01",
   TestGroupTitle = "BENDEX Record",
   RequirementNumber = "BE1SS72.01"
   )]
        public void Test_Bendex_Panel_Loads_Data()
        {
            var panel = new BendexInfoPanelPage(this);
            var page = NavigateToPanel();
            Thread.Sleep(7000);


            AddTestCaseStep("3.1", "Panel Loads Data", "User navigates to 'Related Data'", "User Clicks on 'Related Data' option and 'BENDEX Search' is shown", "Same as expected");
            Thread.Sleep(200);

            AddTestCaseStep("", "Sub Menu", "User Navigates to Option 'BENDEX Search'", "User Clicks on 'BENDEX Search' option", "Same as expected");
            var bendexMenu = page.TabMenuRelatedDataBendexSearch;
            Thread.Sleep(500);
            base.TakeScreenShot("Screen 1", "'BENDEX Search' option Exists");
            AddTestCaseStep("", "Panel Loads Data", "User Clicks on Option 'BENDEX Search'", "'Bendex Search' panel is load", "Same as expected");
            Thread.Sleep(2000);
            base.TakeScreenShot("Screen 2", "'BENDEX Search' panel loads");
            panel.TxtMedicareId.SendKeys(medicareId);
            Thread.Sleep(200);
            panel.BtnSearch.Click();
            Thread.Sleep(5000);
            Assert.IsNotNull(bendexMenu, "Bendex panel does not exists!!");
        }


        [TearDown]
        public override void TearDownTest()
        {
            CloseWebDriver();
        }
    }
}
