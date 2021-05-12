using System.Collections.Generic;
using TestLibrary.Common;

namespace Test.Core.Interfaces
{
    public interface IReportWriter
    {
        void CreateTestDocument();
        void AddImageCaption(string imageCaption);
        void AddParagraph(string text);
        void AddImage(string imageFullNameAndPath);
        void CreateTestCaseSummaryTable(IEnumerable<TestCaseSummaryAttribute> summary);

        void CreateTestCaseTable(string testDescription, IEnumerable<TestCaseTableRow> rows);
        void SetInitialTestAttributes(string coNumber, string author, string changeDescription, string workingDirector);
    }
}
