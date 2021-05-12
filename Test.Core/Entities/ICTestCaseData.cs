using System.Collections.Generic;

namespace TestLibrary.Common
{
    public class ICTestCaseData
    {
       // Dictionary<string, IList<TestCaseTableRow>> rowsData;
      //  Dictionary<string, IDictionary<string, SnapShotData>> snapShotData;
        public ICTestCaseData()
        {
            //  Rows = new List<TestCaseTableRow>();
            //   SnapShots = new Dictionary<string, SnapShotData>();
            Rows = new Dictionary<string, IList<TestCaseTableRow>>();
            SnapShots = new Dictionary<string, IDictionary<string, SnapShotData>>();
        }
        public IDictionary<string, IList<TestCaseTableRow>> Rows { get; set; }
        public IDictionary<string, IDictionary<string, SnapShotData>> SnapShots { get; set; }
       
        //  public IList<TestCaseTableRow> Rows { get; set; }
        //  public IDictionary<string, SnapShotData> SnapShots { get; set; }

    }
}
