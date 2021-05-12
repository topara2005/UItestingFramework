using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.InfraStructure.Common;

namespace TestLibrary.UIMapping
{
   public  class DiagnosisTypeMiniSearch: MiniSearchPanel
    {
        public DiagnosisTypeMiniSearch(ITestVisitor visitor) : base(visitor)
        {
            WindowPrefixAttribute = "DiagTypePrim_DiagTypePrim";
            TxtCodePrefixAttribute = "DiagTypePrim_DiagTypePrim";
            TxtDescriptionPrefixAttribute = "Dsc50_mb_Dsc50";
        }

       
    }
}
