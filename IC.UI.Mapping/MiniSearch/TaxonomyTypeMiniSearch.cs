using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace IC.UI.Mapping
{
    public class TaxonomyTypeMiniSearch : MiniSearchPanel
    {
        public TaxonomyTypeMiniSearch(ITestVisitor visitor) : base(visitor)
        {
            WindowPrefixAttribute = "_TaxonomyType";//"SecdCriteria2";
            TxtCodePrefixAttribute = "Sak_mb_Sak";
            TxtDescriptionPrefixAttribute = "mb_Dsc50";
        }
    }
}
