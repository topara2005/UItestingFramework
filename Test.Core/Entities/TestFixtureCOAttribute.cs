using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.Entities
{
    public class TestFixtureCOAttribute: TestFixtureAttribute
    {
        public string CONumber { get; set; }
    }
}
