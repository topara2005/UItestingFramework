using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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
    public class MainPage : BasePage
    {
        public const string Xpath_MainMenu = "//*[contains(text(),'Main Menu')]//ancestor::a";
        public const string Xpath_MainMenu_MemberSubmenu = "Member";
        public const string Xpath_MainMenu_PASubmenu = "Prior Authorization";
        public const string Xpath_MainMenu_PASubmenu_DURPlusSerch = "Prior Authorization_DUR Plus Search";
        public const string Xpath_MainMenu_PASubmenu_DURPlusInfo = "Prior Authorization_DUR Plus Information";
        public const string Xpath_MainMenu_MemberSubmenu_TbqSearch = "Member_TBQ Search";//"//*[contains(text(),'TBQ Search')]//ancestor::a";
        public const string Xpath_MainMenu_MemberSubmenu_BuyIn = "Member_BuyIn";
        public const string Xpath_menus = "//div[@id='ctl00_MainMenu']//following::ul[contains(@class,'rmVertical') and contains(@class,'rmLevel1')]/li[contains(@class,'rmItem')]/a";
        Dictionary<string, RemoteWebElement> menuDictionary = new Dictionary<string, RemoteWebElement>();

        public MainPage(ITestVisitor visitor) : base(visitor)
        {

        }


        public void LoadAllMenus()
        {

            var elements = Visitor.GetElements(By.XPath(Xpath_menus));
            foreach (var item in elements)
            {
               
                var el = item as RemoteWebElement;
                var displayed = el.Displayed;
               
                var x = el.GetAttribute("type");
               // item.Click();
                var d = el.GetAttribute("innerHTML");
                var span= el.FindElementByTagName("span") as RemoteWebElement;
                     var spantext = span.GetAttribute("innerHTML");
                if (!menuDictionary.ContainsKey(spantext))
                {
                   // menuDictionary.Add(spantext, getAbsoluteXPath(el));
                    menuDictionary.Add(spantext, el);
                    LoadSubMenu(spantext, el);
                }

            }
        }
        private void LoadSubMenu(string menuName, RemoteWebElement element)
        {

            var children = element.FindElements(By.XPath("./..//div[contains(@class,'rmSlide')]/ul/li/a"));
            foreach (var item in children)
            {
                var el = item as RemoteWebElement;
                //   var inn = el.GetAttribute("innerHTML");
                if (el != null)
                {
                    var spanEl = el.FindElement(By.TagName("span")) as RemoteWebElement;
                    var i = spanEl.GetAttribute("innerHTML");
                    if (!menuDictionary.ContainsKey($"{menuName}_{i}"))
                    {
                        menuDictionary.Add($"{menuName}_{i}", el);
                    }      
                }
            }

        }
        public override string Url => GlobalValues.baseURL;

        public IWebElement MainMenu=> Visitor.GetElement(By.XPath(Xpath_MainMenu));
    
      
        public IWebElement MemberSubMenu => menuDictionary[Xpath_MainMenu_MemberSubmenu];
        public IWebElement MemberSubMenuBuyIn => menuDictionary[Xpath_MainMenu_MemberSubmenu_BuyIn];

        public IWebElement MemberSubMenuTBQ => menuDictionary[Xpath_MainMenu_MemberSubmenu_TbqSearch];

        public IWebElement PASubMenuDURPlusSearch=> menuDictionary[Xpath_MainMenu_PASubmenu_DURPlusSerch];
        public IWebElement PASubMenuDURPlusInfo => menuDictionary[Xpath_MainMenu_PASubmenu_DURPlusInfo];
        public IWebElement PASubMenu => menuDictionary[Xpath_MainMenu_PASubmenu];



        private String GetAbsoluteXPath(IWebElement element)
        {
            var js = (IJavaScriptExecutor)Visitor.Driver;
            return (String)(js.ExecuteScript(
                "function absoluteXPath(element) {" +
                    "var comp, comps = [];" +
                    "var parent = null;" +
                    "var xpath = '';" +
                    "var getPos = function(element) {" +
                    "var position = 1, curNode;" +
                    "if (element.nodeType == Node.ATTRIBUTE_NODE) {" +
                    "return null;" +
                    "}" +
                    "for (curNode = element.previousSibling; curNode; curNode = curNode.previousSibling) {" +
                    "if (curNode.nodeName == element.nodeName) {" +
                    "++position;" +
                    "}" +
                    "}" +
                    "return position;" +
                    "};" +

                    "if (element instanceof Document) {" +
                    "return '/';" +
                    "}" +

                    "for (; element && !(element instanceof Document); element = element.nodeType == Node.ATTRIBUTE_NODE ? element.ownerElement : element.parentNode) {" +
                    "comp = comps[comps.length] = {};" +
                    "switch (element.nodeType) {" +
                    "case Node.TEXT_NODE:" +
                    "comp.name = 'text()';" +
                    "break;" +
                    "case Node.ATTRIBUTE_NODE:" +
                    "comp.name = '@' + element.nodeName;" +
                    "break;" +
                    "case Node.PROCESSING_INSTRUCTION_NODE:" +
                    "comp.name = 'processing-instruction()';" +
                    "break;" +
                    "case Node.COMMENT_NODE:" +
                    "comp.name = 'comment()';" +
                    "break;" +
                    "case Node.ELEMENT_NODE:" +
                    "comp.name = element.nodeName;" +
                    "break;" +
                    "}" +
                    "comp.position = getPos(element);" +
                    "}" +

                    "for (var i = comps.length - 1; i >= 0; i--) {" +
                    "comp = comps[i];" +
                    "xpath += '/' + comp.name.toLowerCase();" +
                    "if (comp.position !== null) {" +
                    "xpath += '[' + comp.position + ']';" +
                    "}" +
                    "}" +

                    "return xpath;" +

                    "} return absoluteXPath(arguments[0]);", element));
        }
    }
}
