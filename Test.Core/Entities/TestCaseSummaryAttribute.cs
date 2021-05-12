using NUnit.Framework;

namespace TestLibrary.Common
{
    public class TestCaseSummaryAttribute : TestCaseAttribute


    {

        public string TestCaseNumber { get; set; }
        public string RequirementNumber { get; set; }

        public string TestGroupTitle { get; set; }
        public string CONumber { get; set; }

    }



}
