using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace IC.UI.Mapping
{
    public class TaxonomyMiniSearch : MiniSearchPanel
    {
        public TaxonomyMiniSearch(ITestVisitor visitor, string parentSelectorPostFix) : base(visitor)
        {
            WindowPrefixAttribute = parentSelectorPostFix;//"SecdCriteria2";
            TxtCodePrefixAttribute = "mb_TaxonomyCode";
            TxtDescriptionPrefixAttribute = "mb_Description";
        }
    }
}
