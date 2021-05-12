using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using TestLibrary.Global;
using TestLibrary.UIMapping;

namespace TestLibrary.UITest
{
    public class LoginScreen : IDoLogin
    {
        public void AcceptDoLogin(ITestVisitor visitor)
        {
            LoginPage page = new LoginPage(visitor);
            page.Open();
            Thread.Sleep(500);
            page.TxtUserName.SendKeys(ConfigurationManager.AppSettings["username"]);
            Thread.Sleep(300);
            page.TxtPassword.SendKeys(ConfigurationManager.AppSettings["password"]);
            Thread.Sleep(300);
            page.LoginButton.Click();
            Thread.Sleep(7000);
            // Thread.Sleep(100);
            //  visitor.Wait.Until(d => new MainPage(visitor).MainMenu);



            /*    var  loginUrl = $"{GlobalValues.baseURL}/Account/Login.aspx?ReturnUrl=%2fForeSiteApplication%2f";
                 visitor.Driver.Navigate().GoToUrl(loginUrl);

                 if (visitor.Driver.Title.Contains("Login"))
                 {

                     var el = visitor.GetElement( By.Id(LoginPageUIMapping.IdUsername));
                     Thread.Sleep(100);
                     el.SendKeys("bringedg");
                     Thread.Sleep(100);
                     visitor.GetElement(By.Id(LoginPageUIMapping.IdTxtPassword)).SendKeys("Mimama14*");
                     Thread.Sleep(100);
                     visitor.GetElement(By.Id(LoginPageUIMapping.IdBtnLogin)).Click();
                     var wait = new WebDriverWait(visitor.Driver, TimeSpan.FromSeconds(30));
                     var menu = wait.Until(d => d.FindElement(By.XPath(Menu.Xpath_MainMenu)));

                 }*/

        }
    }
}
