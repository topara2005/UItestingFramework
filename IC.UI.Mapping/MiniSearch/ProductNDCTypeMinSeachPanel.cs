using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class ProductNDCTypeMinSeachPanel: MiniSearchPanel
    {
        public ProductNDCTypeMinSeachPanel(ITestVisitor visitor) : base(visitor)
        {
            WindowPrefixAttribute = "_DrugType";
            TxtCodePrefixAttribute = "mb_Code";
            TxtDescriptionPrefixAttribute = "mb_Description";

        }

    }
}
