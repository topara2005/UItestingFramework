using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace Test.InfraStructure.Common
{
    public class MiniSearchPanel : DataPanel
    {
        //RadWindowWrapper_ctl00_MainContent_InformationPageCtrl_ctl01_Datapanel_ctl00_ctl04_PublicHlthPgm_PublicHlthPgm_win
        //ctl00_MainContent_InformationPageCtrl_ctl01_Datapanel_ctl00_ctl04_PublicHlthPgm_PublicHlthPgm_win_C_SearchPage_Datalist table
        //tbody/tr[0]
        public MiniSearchPanel(ITestVisitor visitor) : base(visitor)
        {

        }

        public virtual string WindowPrefixAttribute
        {
            get;
            set;
        }
        public virtual string TxtCodePrefixAttribute
        {
            get;
            set;
        }
        public virtual string TxtDescriptionPrefixAttribute
        {
            get;
            set;
        }

        public virtual IWebElement TxtCode => GetWebElement(TxtCodePrefixAttribute);
        public virtual IWebElement TxtDescription => GetWebElement(TxtDescriptionPrefixAttribute);
        public virtual IWebElement BtnSearch =>
             RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'SearchButton')]"));
        public virtual IWebElement BtnClear =>
            RootContainer.FindElement(By.XPath(".//input[@type='submit' and contains(@id,'ClearButton')]"));

        public override string PanelTestPrefixAttribute => WindowPrefixAttribute;

        public virtual IWebElement Datalist =>
            RootContainer.FindElement(By.XPath(".//table[contains(@id,'SearchPage_Datalist')]"));

        public IWebElement GetRow(int index)
        {
            var elements= Datalist.FindElements(By.XPath(".//tr[contains(@class,'iC_DataListItem')]"));
            if (elements.Count >= index && index>0)
            {
                return elements[index-1];
            }
            return null;
        }

        public override IWebElement RootContainer =>
            this.Visitor.Wait.Until(d=>d.FindElement(By.XPath($"//div[contains(concat(' ', @class, ' '),'RadWindow') and contains(@id,'{WindowPrefixAttribute}_win')]")));
     

          

        public override string Url => throw new NotImplementedException();
    }
}
