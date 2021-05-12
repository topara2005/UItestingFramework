using OpenQA.Selenium;

namespace Test.InfraStructure.Common
{
    public interface IDataPanel
    {
        string PanelTestPrefixAttribute { get; }
        IWebElement RootContainer { get; }
        void LoadChildrenTestElements();
    }
}