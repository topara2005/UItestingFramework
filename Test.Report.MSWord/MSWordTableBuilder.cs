using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

using System;
using System.Collections.Generic;
using System.Linq;


namespace TestLibrary.Common
{
    class MSWordTableBuilder
    {
        private  TableRow CreateSummaryTableRowCells(string testCaseNumber, string requirementNumber, string testGroupTitle, string groupDescription)
        {
            TableRow tr = new TableRow();
            RunProperties rp = new RunProperties();
            rp.Append(new RunFonts { Ascii = "Times New Roman" });
           // rp.Append(new Indentation { Left = "144", Right = "144" });
            rp.Append(new Color { ThemeColor = ThemeColorValues.Text1 });
            rp.Append(new Justification { Val = JustificationValues.Center } );
            //  rp.Append(new Color{ Val = "#2E74B5" });
            var cellProperties = new TableCellProperties(
              //  new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" },
               // new TableCellSpacing { Type = TableWidthUnitValues.Dxa, Width = "144" },
                  new TableCellBorders(
                new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = (UInt32Value)1U },
                new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = (UInt32Value)2U },
                new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = (UInt32Value)2U }
                ));
              TableCell tc1 = new TableCell(new Paragraph(new Run(rp, new Text(testCaseNumber))), cellProperties);
            TableCell tc2 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(requirementNumber))));
            tc2.AppendChild(cellProperties.Clone() as TableCellProperties);
            TableCell tc3 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(testGroupTitle))));
            tc3.AppendChild(cellProperties.Clone() as TableCellProperties);
            TableCell tc4 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(groupDescription))));
            tc4.AppendChild(cellProperties.Clone() as TableCellProperties);
            tr.Append(tc1, tc2, tc3, tc4);
            return tr;
        }

        public void CreateSummaryTable(Body body, IEnumerable<TestCaseSummaryAttribute> testCases)
        {
            var tables = body.Descendants<Table>()
                .SelectMany(t => t.Descendants<TableProperties>())
                .Where(tp =>
                tp.Descendants<TableCaption>().Any(tc => tc.Val.ToString().Equals("Test Case Summary", StringComparison.InvariantCultureIgnoreCase)))
                .Select(tc => tc.Parent);
            if (tables.Any())
            {
                var table = tables.First();
                TableGrid tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn(), new GridColumn());
                table.AppendChild(tg);
                var prop = table.Descendants<TableCell>().First().TableCellProperties;
                foreach (var summary in testCases)
                {
                    table.AppendChild(
                        CreateSummaryTableRowCells(summary.TestCaseNumber, summary.RequirementNumber,
                        summary.TestGroupTitle, summary.Description));
                }
            }
        }

        public Table CreateTestCaseTable(Body body, string testDescription, IEnumerable<TestCaseTableRow> rows)
        {
            // Create a table.
            Table tbl = new Table();

            TableStyle tableStyle = new TableStyle() { Val = "TableGrid" };

            // Make the table width 100% of the page width.
            TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size =12},
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 12 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 12 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = 12 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size =6 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = 6 }
                )
            );
            tblProp.Append(tableStyle, tableWidth);
            // Append the TableProperties object to the empty table.
            tbl.AppendChild<TableProperties>(tblProp);
            // Add 5 columns to the table.
            TableGrid tg = new TableGrid(new GridColumn(), new GridColumn(), new GridColumn(), new GridColumn(), new GridColumn());
            tbl.AppendChild(tg);

            // Create 1 row to the table.
            TableRow tr1 = new TableRow();
            //tc.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Auto }));
            // Add a cell to each column in the row.
            TableCell tc1 = new TableCell(CreateTableHeader("TEST CASE NUM"));
            tc1.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
            TableCell tc2 = new TableCell(CreateTableHeader("TEST COND"));
            tc2.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
            TableCell tc3 = new TableCell(CreateTableHeader("INPUT DATA"));
            tc3.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
            TableCell tc4 = new TableCell(CreateTableHeader("EXPECTED RESULTS"));
            tc4.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
            TableCell tc5= new TableCell(CreateTableHeader("ACTUAL RESULTS"));
            tc5.Append(new TableCellProperties(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }));
            tr1.Append(tc1, tc2, tc3, tc4, tc5);

            // Add row to the table.
            tbl.AppendChild(tr1);
            foreach (var row in rows)
            {
                tbl.AppendChild(CreateTescaseTableRowCells(row));
            }
            body.Append(tbl);
            return tbl;
        }
        private Paragraph CreateTableHeader(string text)
        {
            var p = new Paragraph();
            ParagraphProperties pp = new ParagraphProperties();
            pp.Justification = new Justification() { Val = JustificationValues.Center };
           
            p.Append(pp);
            var r1 = new Run();
            var t = new Text(text) { Space = SpaceProcessingModeValues.Preserve };
            RunProperties runProperties = r1.AppendChild(new RunProperties());
            Bold bold = new Bold();
            bold.Val = OnOffValue.FromBoolean(true);
            runProperties.AppendChild(bold);
            r1.Append(t);
            p.Append(r1);
            return p;
        }

        private TableRow CreateTescaseTableRowCells(TestCaseTableRow data)
        {
            TableRow tr = new TableRow();
            RunProperties rp = new RunProperties();
            rp.Append(new RunFonts { Ascii = "Times New Roman" });
            // rp.Append(new Indentation { Left = "144", Right = "144" });
            rp.Append(new Color { ThemeColor = ThemeColorValues.Text1 });
            rp.Append(new Justification { Val = JustificationValues.Center });
            //  rp.Append(new Color{ Val = "#2E74B5" });
            var cellProperties = new TableCellProperties(
                  //  new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" },
                  // new TableCellSpacing { Type = TableWidthUnitValues.Dxa, Width = "144" },
                  /*   new TableCellBorders(*/
                  new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size =0 }
                 /*new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = (UInt32Value)2U },
                   new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = (UInt32Value)2U },
                   new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = (UInt32Value)2U }
                   )*/
                  );
            var p = new Paragraph();
            var r1 = new Run();
            var t= new Text(data.TestCaseNumber) { Space = SpaceProcessingModeValues.Preserve };
        //    var t = new Text(data.TestCaseNumber);
            RunProperties runProperties = r1.AppendChild(new RunProperties());

            Bold bold = new Bold();

            bold.Val = OnOffValue.FromBoolean(true);

            runProperties.AppendChild(bold);
            r1.Append(t);
            p.Append(r1);



            TableCell tc1 =new TableCell(p); //new TableCell(new Paragraph(new Run(rp, new Text(data.TestCaseNumber))), cellProperties);
            TableCell tc2 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(data.TestCondition))));
            tc2.AppendChild(cellProperties.Clone() as TableCellProperties);
            TableCell tc3 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(data.InputData))));
            tc3.AppendChild(cellProperties.Clone() as TableCellProperties);
            TableCell tc4 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(data.ExpectedResult))));
            tc4.AppendChild(cellProperties.Clone() as TableCellProperties);
            TableCell tc5 = new TableCell(new Paragraph(new Run(rp.CloneNode(true), new Text(data.ActualResult))));
            tc5.AppendChild(cellProperties.Clone() as TableCellProperties);
            tr.Append(tc1, tc2, tc3, tc4, tc5);
            return tr;
        }
    }
}
