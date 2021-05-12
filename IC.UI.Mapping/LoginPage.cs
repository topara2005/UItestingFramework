using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;
using TestLibrary.Global;

namespace TestLibrary.UIMapping
{
    public class LoginPage: BasePage
    {
        public const string IdTxtPassword = "MainContent_GenericPageCtrl_baseLogin_dataPanel_password_0_mb_password_0";
        public const string IdUsername = "MainContent_GenericPageCtrl_baseLogin_dataPanel_userName_0_mb_userName_0";
        public const string IdBtnLogin = "ctl00_MainContent_GenericPageCtrl_baseLogin_dataPanel_ctl02_ctl03_LoginButton_input";

        public LoginPage(ITestVisitor visitor):base(visitor)
        {

        }

        public override string Url => $"{GlobalValues.baseURL}/Account/Login.aspx?ReturnUrl=%2fForeSiteApplication%2f";

        public IWebElement LoginButton => Visitor.GetElement(By.Id(IdBtnLogin));
        public IWebElement TxtPassword=> this.Visitor.GetElement(By.Id(IdTxtPassword));
        public IWebElement TxtUserName => Visitor.GetElement(By.Id(IdUsername));
      
    }
}
