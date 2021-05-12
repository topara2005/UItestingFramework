using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;

namespace Test.InfraStructure.Common
{
    public abstract class DataPanel : BasePage, IDataPanel
    {
        private const string _prefix = "data-test-field";
        protected ReadOnlyCollection<IWebElement> _children = null;
        //  protected StringCollection _childrenNames = new StringCollection();
        protected IDictionary<string, IWebElement> _elementsDictionary = null;
        private string _xpathToRootElement = "";
        public DataPanel(ITestVisitor visitor) : base(visitor)
        {

        }

        protected virtual void LoadTestElements(IWebElement container)
        {
            if (container != null && (_children == null || _children.Count == 0))
            {
                _children = container.FindElements(By.XPath($".//*[contains(concat(' ', @class, ' '), ' {_prefix} ')]"));
                _elementsDictionary = _children.ToDictionary(el => el.GetAttribute("id"), el => el);
            }
        }

        public virtual void LoadChildrenTestElements()
        {
            LoadTestElements(RootContainer);
        }

        public void RefreshElements()
        {
            if (_elementsDictionary != null)
            {
                _elementsDictionary.Clear();
            }
            if(_children!=null)
            {
                _children = null;
            }
            LoadChildrenTestElements();
        }
        protected IWebElement GetWebElement(string dataFieldName)
        {
            LoadChildrenTestElements();
            var fullName = _elementsDictionary.Keys.FirstOrDefault(c => c.Contains(dataFieldName));
            return !string.IsNullOrEmpty(fullName) ? _elementsDictionary[fullName] : null;
        }

        public abstract string PanelTestPrefixAttribute { get; }
        protected string XPathRoot => $"//span[contains(concat(' ', @class, ' '), ' data-test-{PanelTestPrefixAttribute} ')]";
        public virtual IWebElement RootContainer =>
            Visitor.Wait.Until(d => d.FindElement(By.XPath(XPathRoot)));
        //Visitor.GetElement(By.XPath(XPathRoot));
    }
}
