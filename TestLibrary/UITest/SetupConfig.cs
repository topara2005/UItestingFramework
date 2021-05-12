using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.UITest
{
    [SetUpFixture]
    public class SetupConfig
    {


        [OneTimeSetUp]
        public void Setup()
        {
           
        }

        [OneTimeTearDown]
        public void TearDown()
        {

        }
    }
}
