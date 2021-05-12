using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class DurPlusCodeMiniSearch : MiniSearchPanel
    {
        public DurPlusCodeMiniSearch(ITestVisitor visitor) : base(visitor)
        {
            WindowPrefixAttribute = "Criteria_Criteria";
            TxtCodePrefixAttribute = "$Code$mb_Code";
            TxtDescriptionPrefixAttribute = "$Description$mb_Description";
        }
    }
}
