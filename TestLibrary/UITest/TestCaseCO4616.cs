using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;

using TestLibrary.Global;
using TestLibrary.UITest;
using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestLibrary
{
    [TestFixture]
   // [TestClass]
    public class TestCaseCO4616 : BaseTest
    {

        public TestCaseCO4616():base(new LoginScreen())
        {

        }
         [SetUp]
       // [TestInitialize()]
        public override void SetupTest()
        {


            //   DoLogin();

        }
          [TearDown]
       // [TestCleanup()]
        public override void TearDownTest()
        {
            driver.Close();
        }



        [Test(Description = "Do login")]
       // [TestMethod]
        public void Login_is_on_home_page()
        {


              var wait=DoLogin();

              if (wait != null)
              {


                Thread.Sleep(10000);
                Assert.IsTrue(driver.Title.Contains("interChange"));
            }
            else
            {
                Assert.Fail("Error: Could not log in");
            }
           

            // Thread.Sleep(2000);
           
           
        }

        public override void ClassInit()
        {
            throw new NotImplementedException();
        }

        public override void ClassCleanup()
        {
            throw new NotImplementedException();
        }
    }

}
