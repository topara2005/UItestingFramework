using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
    public class PublicHealthProgramMiniSearch: MiniSearchPanel
    {
        public PublicHealthProgramMiniSearch(ITestVisitor visitor) : base(visitor)
        {
            WindowPrefixAttribute = "PublicHlthPgm";
            TxtCodePrefixAttribute = "mb_PgmHealthCode";
            TxtDescriptionPrefixAttribute = "mb_PgmHealthDescription";
        }

    }
}
