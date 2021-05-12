using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Test.Core.Entities;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;
using TestLibrary.Common;
using TestLibrary.Global;

namespace TestLibrary.UITest
{
    public abstract partial class BaseTest : ITestVisitor
    {
        protected IWebDriver driver;
        public string homeURL;
        public string loginUrl;
        protected TestContext testContextInstance;
        protected Actions driverActions;
        protected Helper helper = new Helper();
        DriverFactory factory = new DriverFactory();

        IReportWriter _reportWriter;
        protected string _author, _description, _testName, _CONumber;
        List<TestCaseSummaryAttribute> testAttributes = new List<TestCaseSummaryAttribute>();
        IDictionary<string, ICTestCaseData> data = new Dictionary<string, ICTestCaseData>();
        private readonly IDoLogin _LoginScreen;


        public virtual string ConnectionString => ConfigurationManager.ConnectionStrings["default"].ConnectionString; 
      
        #region Abstract
        public abstract void ClassInit();
        public abstract void ClassCleanup();
        public abstract void SetupTest();
         public abstract void TearDownTest();
        #endregion
        #region ITestVisitor
      
      
        public IWebDriver Driver { get{ return driver; } }
        public IWebElement GetElement(By by)
        {
            IWebElement result = null;
            try
            {
                result = this.Wait.Until(x => x.FindElement(by));
            }
            catch (TimeoutException ex)
            {
               // log.Error(ex.Message);
               // throw new NoSuchElementException(by, this, ex);
            }
            return result;
        }
        public IEnumerable<IWebElement> GetElements(By by)
        {
            IEnumerable<IWebElement> result = null;
            try
            {
                result = this.Wait.Until(x => x.FindElements(by));
            }
            catch (TimeoutException ex)
            {
                // log.Error(ex.Message);
                // throw new NoSuchElementException(by, this, ex);
            }
            return result;
        }
        public void WaitForElementPresent(By by)
        {
            this.GetElement(by);
        }
        public bool IsElementPresent(By by)
        {
            try
            {
                this.Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException ex)
            {
               // log.Error(ex.Message);
                return false;
            }
        }
        public WebDriverWait Wait { get; set; }

        #endregion
        /// <summary>
        /// This method should always being call for  the method that coantains   "OneTimeSetUp" attribute in the child class
        /// </summary>
        /// <param name="tesCaseNumber"></param>
        /// <param name="memberName">leave it as is</param>
        protected virtual void InitTestDocument (string tesCaseNumber, [CallerMemberName] string memberName = "")
        {

            StackTrace stackTrace = new StackTrace();
            var frames = stackTrace.GetFrames();
            if (frames.Any(fr => fr.GetMethod().Name.Equals(memberName)))
            {
                var methodCaller = frames.First(fr => fr.GetMethod().Name.Equals(memberName)).GetMethod();
                var attrs = System.Attribute.GetCustomAttributes(methodCaller.DeclaringType, false);  // Reflection. 
                var fixt = attrs.FirstOrDefault() as TestFixtureAttribute;
                GetTestCaseAttributes(methodCaller.DeclaringType);
                if (fixt != null)
                {
                    _author = fixt.Author;
                    _description = fixt.Description;
                    _testName = fixt.TestName;
                    if (fixt is TestFixtureCOAttribute)
                    {
                        _CONumber = (fixt as TestFixtureCOAttribute).CONumber;
                    }
                    else
                    {
                        _CONumber = _testName;
                    }
                }
            }
            else
            {
                _author ="Undefined";
                _description = "Undefined";
                _testName = "Undefined";
                _CONumber = "Undefined";
            }
        
            //It forces to create the currentTestMetadata
           // CurrentTestData();
        }

        private ICTestCaseData CurrentTestData() {
            if (!data.ContainsKey(TestContext.CurrentContext.Test.ClassName))
            {
                data.Add(TestContext.CurrentContext.Test.ClassName, new ICTestCaseData());
            }
            return data[TestContext.CurrentContext.Test.ClassName];
        }



        /// <summary>
        /// It writes all the metadata to WORD of the current test
        /// It must be always called in the the child's class method  that contains "TearDown" attribute
        /// </summary>
        public virtual void PersistCurrentTestMetadata()
        {
            var reportFactory = new ReportWriterFactory();
            _reportWriter = reportFactory.GetReportWriter();
            _reportWriter.SetInitialTestAttributes(_CONumber, _author, _description, TestContext.CurrentContext.TestDirectory);
            _reportWriter.CreateTestDocument();
            _reportWriter.CreateTestCaseSummaryTable(testAttributes);
            foreach (var data in data)
            {
                var currentTestData = data.Value; //CurrentTestData();
                var testCases = currentTestData.Rows.Keys;
                foreach (var testCaseNumber in testCases)
                {
                  
                   
                     _reportWriter.CreateTestCaseTable(testCaseNumber, currentTestData.Rows[testCaseNumber]);
                    var snapshots=currentTestData.SnapShots[testCaseNumber];
                    foreach (var snapshot in snapshots)
                    {
                        //if (snapshot.Value.Taken)
                        //{
                            _reportWriter.AddImageCaption(snapshot.Value.Caption);
                            _reportWriter.AddImage(snapshot.Value.FileName);
                            snapshot.Value.AttachedToDocument = true;
                        //}
                    }

                }
               /* foreach (var item in currentTestData.Rows[testCaseNumber])
                {
                    _reportWriter.CreateTestCaseTable(data.Key, item.Value);
                    
                }
                foreach (var item in currentTestData.SnapShots)
                {
                    foreach (var snapshot in item.Value)
                    {
                        if (snapshot.Value.Taken)
                        {
                            _reportWriter.AddImageCaption(snapshot.Value.Caption);
                            _reportWriter.AddImage(snapshot.Value.FileName);
                            snapshot.Value.AttachedToDocument = true;
                        }
                    }
                }
                */


            }
           
           
        }

        private BaseTest()
        {
            homeURL = GlobalValues.baseURL;
            loginUrl = $"{homeURL}/Account/Login.aspx?ReturnUrl=%2fForeSiteApplication%2f";
        }
        public BaseTest(IDoLogin loginScreen):this()
        {
            _LoginScreen = loginScreen;

        }

        public virtual void AddTestCaseStep(string testCaseNumber, string testCondition, string inputData, string expectedResult, string actualResult)
        {
            var currentest = this.TestContext.Test.Name;
            var className = this.TestContext.Test.ClassName;
            var currentTestData = CurrentTestData();
            if(!currentTestData.Rows.ContainsKey(currentest))
            {
                currentTestData.Rows.Add(currentest, new List<TestCaseTableRow>());

            }
            currentTestData.Rows[currentest].Add(
                 new TestCaseTableRow {
                     TestCaseNumber=  testCaseNumber,
                     ActualResult = actualResult,
                     ExpectedResult = expectedResult,
                     InputData= inputData,
                     TestCondition= testCondition
                 }
                );
        }
        /// <summary>
        /// iT TAKES A SCREENSHOT. The file is stored in the current test directory and with the jpg extension
        /// </summary>
        /// <param name="fileName">the file name only, without extension or directory</param>
        protected virtual void TakeScreenShot(string captionNumber, string description)
        {
           
            var currentTestData = CurrentTestData();
            var currentest = this.TestContext.Test.Name;
            if (!currentTestData.SnapShots .ContainsKey(currentest))
            {
                currentTestData.SnapShots.Add(currentest, new Dictionary<string, SnapShotData>());

            }
            var fileName = $"{TestContext.CurrentContext.TestDirectory}\\{currentest}_{captionNumber}.png";
            driver.TakeScreenshot(fileName, ScreenshotImageFormat.Png);
            if (!currentTestData.SnapShots[currentest].ContainsKey(captionNumber))
            {
                currentTestData.SnapShots[currentest].Add(captionNumber,
                new SnapShotData
                {
                    AttachedToDocument = false,
                    Caption = $"{captionNumber}.{description}",
                    FileName = fileName,
                    Taken = true
                }); ;
            }
            else
            {
                currentTestData.SnapShots[currentest][captionNumber] = new SnapShotData
                {
                    AttachedToDocument = false,
                    Caption = $"{captionNumber}.{description}",
                    FileName = fileName,
                    Taken = false
                };
            }
            
            //_wordHelper.AddImage(fullFileName);
        }
        protected virtual IEnumerable<TestCaseAttribute> GetTestCaseAttributes(Type type)
        {
            testAttributes.Clear();
            var methods = type.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(TestCaseSummaryAttribute), false).Length > 0)
                ;
            foreach (var mInfo in methods)
            {
                // var atts= mInfo.CustomAttributes.ToList();
                var atts = mInfo.GetCustomAttributes(typeof(TestCaseSummaryAttribute), false).OfType<TestCaseSummaryAttribute>();
                testAttributes.AddRange(atts);
               // var att = atts.FirstOrDefault();
            }
            
            return testAttributes;
        }
        protected virtual void InitDriver()
        {
            testContextInstance = TestContext.CurrentContext; 
            driver = factory.GetDriver();
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
         //   driver.IgnoreExceptionTypes(typeof(NoSuchElementException));
          //  driver.IgnoreExceptionTypes(typeof(WebDriverException));
            driverActions = new Actions(driver);
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        public virtual void CloseWebDriver()
        {
            factory.CloseDriver(driver);
        }


        public virtual WebDriverWait DoLogin()
        {
            _LoginScreen.AcceptDoLogin(this);
            return null;
          

        }
      /*  public virtual void Wait(WebDriverWait wait, int seconds, int maxTimeOutSeconds = 60)
        {
           
            var delay = new TimeSpan(0, 0, 0, 0, seconds);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }*/
    }
}
