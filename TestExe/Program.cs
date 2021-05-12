using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibrary;

using TestLibrary.UITest;
using TestLibrary.Common;

namespace TestExe
{
    class Program
    {
        static void Main(string[] args)
        {

            var test = new TestCaseCO5650();
            try
            {
              //  test.Test_Table_PDL_Exists();
                test.ClassInit();
                test.SetupTest();
              test.Test_DurPlus_Info_Panel_Base_And_PDLCriteria_Save();
                test.TearDownTest();
            //test.SetupTest();
            //    test.Test_DurPlus_Info_Panel_Loads();
            //    test.TearDownTest();
               // test.SetupTest();
               
                //   test.SetupTest();
                // test.Test_Bendex_Panel_Loads();*/

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally {
               test.TearDownTest();
                test.ClassCleanup();
            }
          
            Console.ReadLine();




            /* using (var helper = new MSWordHelper())
       {
           helper.SetInitialTestAttributes("46", "EBC", "X", @"C:\bit\test\TestExe\TestExe\bin\Debug");
           helper.CreateTestDocument();

           List<TestCaseTableRow> list = new List<TestCaseTableRow>
           {
                new TestCaseTableRow
                {
                    ActualResult="As Expected",
                    ExpectedResult="XX",
                    InputData="Input data",
                    TestCaseNumber="1.1.",
                    TestCondition="Test "
                },
                new TestCaseTableRow
                {
                    ActualResult="As Expected",
                    ExpectedResult="XX",
                    InputData="Input data",
                    TestCaseNumber="1.2",
                    TestCondition=""
                }
           };
           helper.CreateTestCaseTable("Tests", list);

       }*/

        }


    }
}
