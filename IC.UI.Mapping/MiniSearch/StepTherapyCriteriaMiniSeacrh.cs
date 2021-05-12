using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
   public  class StepTherapyCriteriaMiniSearch : MiniSearchPanel
    {
        public StepTherapyCriteriaMiniSearch(ITestVisitor visitor, string parentSelectorPostFix) : base(visitor)
        {
            WindowPrefixAttribute = parentSelectorPostFix;//"SecdCriteria2";
            TxtCodePrefixAttribute = "Code_mb_Code";
            TxtDescriptionPrefixAttribute = "mb_Description";
        }
    }
}
