using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class DiagnosisMiniSearch : MiniSearchPanel
    {
        public DiagnosisMiniSearch(ITestVisitor visitor, string criteriaId) : base(visitor)
        {
            WindowPrefixAttribute = criteriaId;
            TxtCodePrefixAttribute = "mb_ServiceCode";
            TxtDescriptionPrefixAttribute = "Name_mb_Name";
        }


        public virtual SelectElement DropDownICD =>
             new  SelectElement(RootContainer.FindElement(By.XPath(".//select[contains(@id,'IcdVersion_mb_IcdVersion')]")));
    }
}
