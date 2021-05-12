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
    public class TBQIformationPanelPage : BasePage
    {
        public const string Xpath_CurrentHICN = "//*/label/a[contains(text(),'Current HICN/RRB')]";
        public const string Xpath_OpenTabMenu = "//*/a/span[contains(text(),'Open Tab')]//ancestor::a";
        public const string Xpath_OpenTabMenu_TBQ = "//*/a/span[contains(text(),'Open Tab')]//ancestor::li/*//span[contains(text(),'TBQ')]//ancestor::a";
        public const string Xpath_OpenTabMenu_TBQ_Ineligible = "//*/a/span[contains(text(),'Open Tab')]//ancestor::li/*//span[contains(text(),'TBQ')]//ancestor::div[1]//child::li/ul/li/*/span[contains(text(),'Ine')]//parent::div/input";

        public const string Xpath_TBQPanelTitle = "//div[contains(@title,'TBQ > Ineligible Periods')]//*/span[contains(text(),'Ine')]";
       

        public TBQIformationPanelPage(ITestVisitor visitor) : base(visitor)
        {

        }
        public override string Url => "";


        public IWebElement TxtCurrentHICN => Visitor.GetElement(By.XPath(Xpath_CurrentHICN));

        public IWebElement OpenTabMenu => Visitor.GetElement(By.XPath(Xpath_OpenTabMenu));

        public IWebElement OpenTabMenuTBQ => Visitor.GetElement(By.XPath(Xpath_OpenTabMenu_TBQ));
        public IWebElement SubMenuIneligible => Visitor.GetElement(By.XPath(Xpath_OpenTabMenu_TBQ_Ineligible));
        public IWebElement TBQPanelTittle => Visitor.GetElement(By.XPath(Xpath_TBQPanelTitle));

    }
}
