using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DW1 = DocumentFormat.OpenXml.Drawing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Word.DrawingShape;
using Style = DocumentFormat.OpenXml.Wordprocessing.Style;
using OpenXmlPowerTools;
using Test.Core.Interfaces;

namespace TestLibrary.Common
{
    public class MSWordHelper: IReportWriter, IDisposable
    {
        private IDictionary<string, string> _tags;
        private const string TAG_PATTERN= @"\[(.*?)[\.|\:](.*?)\]";
        private const string TAG_START = "$<";
        private const string TAG_END = ">";
        private  string _coNumber, _author, _changeDescription, _workingDirectory;
        private string fileName;
        const int _height=480, _width=640;
        WordprocessingDocument _wordDoc=null;


        /// <summary>
        private void AddShape(WordprocessingDocument wordDoc, string fullPath)
        {
           var shapes2 = wordDoc.MainDocumentPart.Document.Body.Descendants<WordprocessingShape>();
            string temp;
            MainDocumentPart mainPart = wordDoc.MainDocumentPart;
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Bmp);

            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                imagePart.FeedData(stream);
            }
         

            Int64Value width = _width * 9525;
            Int64Value height = _height * 9525;
            temp = mainPart.GetIdOfPart(imagePart);
            var dw = GetImageElement(temp, fullPath, "Picture1", _width, _height);
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(dw)));
            /*  foreach (var sp in shapes2)
              {
                  //Wps.ShapeProperties shapeProperties1 = 
                  A.BlipFill blipFill1 = new A.BlipFill() { Dpi = (UInt32Value)0U, RotateWithShape = true };
                  A.Blip blip1 = new A.Blip() { Embed = temp };

                  A.Stretch stretch1 = new A.Stretch();
                  A.FillRectangle fillRectangle1 = new A.FillRectangle() { Left = 10000, Top = 10000, Right = 10000, Bottom = 10000 };
                  Wps.WordprocessingShape wordprocessingShape1 = new Wps.WordprocessingShape();


                  stretch1.Append(fillRectangle1);
                  blipFill1.Append(blip1);
                  blipFill1.Append(stretch1);
                  Wps.ShapeProperties shapeProperties1 = sp.Descendants<Wps.ShapeProperties>().First();
                  shapeProperties1.Append(blipFill1);

              }*/
        }
        private string AddGraph(WordprocessingDocument wpd, string filepath)
        {
            ImagePart ip = wpd.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                if (fs.Length == 0) return string.Empty;
                ip.FeedData(fs);
            }

            return wpd.MainDocumentPart.GetIdOfPart(ip);
        }
        private void InsertImage(WordprocessingDocument wpd, OpenXmlElement parent, string filepath)
        {
            string relationId = AddGraph(wpd, filepath);
            if (!string.IsNullOrEmpty(relationId))
            {
                Int64Value width = _width * 9525;
                Int64Value height = _height * 9525;

                var draw = new Drawing(
                    new DW.Inline(
                        new DW.Extent() { Cx = width, Cy = height },
                        new DW.EffectExtent()
                        {
                            LeftEdge = 0L,
                            TopEdge = 0L,
                            RightEdge = 0L,
                            BottomEdge = 0L
                        },
                        new DW.DocProperties()
                        {
                            Id = (UInt32Value)1U,
                            Name = "my image name"
                        },
                        new DW.NonVisualGraphicFrameDrawingProperties(new A.GraphicFrameLocks() { NoChangeAspect = true }),
                        new A.Graphic(
                            new A.GraphicData(
                                new PIC.Picture(
                                    new PIC.NonVisualPictureProperties(
                                        new PIC.NonVisualDrawingProperties()
                                        {
                                            Id = (UInt32Value)0U,
                                            Name = relationId
                                        },
                                        new PIC.NonVisualPictureDrawingProperties()),
                                        new PIC.BlipFill(
                                            new A.Blip(
                                                new A.BlipExtensionList(
                                                    new A.BlipExtension() { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" })
                                                )
                                            {
                                                Embed = relationId,
                                                CompressionState =
                                                A.BlipCompressionValues.Print
                                            },
                                                new A.Stretch(
                                                    new A.FillRectangle())),
                                                    new PIC.ShapeProperties(
                                                        new A.Transform2D(
                                                            new A.Offset() { X = 0L, Y = 0L },
                                                            new A.Extents() { Cx = width, Cy = height }),
                                                            new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle })))
                            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                                                            )
                    {
                        DistanceFromTop = (UInt32Value)0U,
                        DistanceFromBottom = (UInt32Value)0U,
                        DistanceFromLeft = (UInt32Value)0U,
                        DistanceFromRight = (UInt32Value)0U,
                        EditId = "50D07946"
                    });

                parent.Append(draw);
            }
        }


        private  Drawing GetImageElement(
        string imagePartId,
        string fileName,
        string pictureName,
        double width,
        double height)
        {
            double englishMetricUnitsPerInch = 914400;
            double pixelsPerInch = 96;

            //calculate size in emu
            double emuWidth = width * englishMetricUnitsPerInch / pixelsPerInch;
            double emuHeight = height * englishMetricUnitsPerInch / pixelsPerInch;

            var element = new Drawing(
                new DW.Inline(
                    new DW.Extent { Cx = (Int64Value)emuWidth, Cy = (Int64Value)emuHeight },
                    new DW.EffectExtent { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
                    new DW.DocProperties { Id = (UInt32Value)1U, Name = pictureName },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                    new DW1.GraphicFrameLocks { NoChangeAspect = true }),
                    new DW1.Graphic(
                        new DW1.GraphicData(
                            new DW1.Pictures.Picture(
                                new DW1.Pictures.NonVisualPictureProperties(
                                    new DW1.Pictures.NonVisualDrawingProperties { Id = (UInt32Value)0U, Name = fileName },
                                    new DW1.Pictures.NonVisualPictureDrawingProperties()),
                                new DW1.Pictures.BlipFill(
                                    new DW1.Blip(
                                        new DW1.BlipExtensionList(
                                            new DW1.BlipExtension { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" }))
                                    {
                                        Embed = imagePartId,
                                        CompressionState = DW1.BlipCompressionValues.Print
                                    },
                                            new DW1.Stretch(new DW1.FillRectangle())),
                                new DW1.Pictures.ShapeProperties(
                                    new DW1.Transform2D(
                                        new DW1.Offset { X = 0L, Y = 0L },
                                        new DW1.Extents { Cx = (Int64Value)emuWidth, Cy = (Int64Value)emuHeight }),
                                    new DW1.PresetGeometry(
                                        new DW1.AdjustValueList())
                                    { Preset = DW1.ShapeTypeValues.Rectangle })))
                        {
                            Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                        }))
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U,
                    EditId = "50D07946"
                });
            return element;
       }
    

    /// </summary>




    public MSWordHelper() { }


       public  void SetInitialTestAttributes(string coNumber, string author, string changeDescription, string workingDirectory)
        {
            _coNumber = coNumber;
            _author = author;
            _changeDescription = changeDescription;
            _workingDirectory = workingDirectory;
            BuildTags();
        }
        private void BuildTags()
        {
            _tags = new Dictionary<string, string>();
            _tags.Add("[$CONumber]", _coNumber);
            _tags.Add("[$ChangeDescription]", _changeDescription);
            _tags.Add("[$Date]", DateTime.Now.ToString("MM/dd/yy"));
            _tags.Add("[$Author]", _author);
        }

        public void CreateTestDocument()
        {
            fileName= CloneTemplateFileForEditing();
            _wordDoc = WordprocessingDocument.Open(fileName, true);
            SearchAndReplaceTags();
        }

       
        private void SearchAndReplaceTags()
        {
            if (_tags!=null && _tags.Count>0)
            {
                _tags.Keys.ToList().ForEach(
                 key =>
                 {
                     if (_tags.ContainsKey(key) && _tags[key] != null)
                     {
                         TextReplacer.SearchAndReplace(wordDoc: _wordDoc, search: key, replace: _tags[key], matchCase: false);
                     }
                 }

                 );
                _wordDoc.Save();
            }
              
           
        }




        public void AddImageCaption(string imageCaption)
        {
            //TODO: Add paragraph style
            AddParagraph(imageCaption, null);
        }
        public virtual void  AddParagraph(string text)
        {
            AddParagraph(text, null);
        }
        private void AddParagraph(string text, Style style)
        {
            //TODO: Add paragraph style
            // Assign a reference to the existing document body.
            Body body = _wordDoc.MainDocumentPart.Document.Body;

            // Add a paragraph with some text.
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(text));
        }


        public void AddImage(string imageFullNameAndPath)
        {
    
           
                MainDocumentPart mainPart = _wordDoc.MainDocumentPart;
                Paragraph para = _wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                AddShape(_wordDoc, imageFullNameAndPath);
            // wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
            //  InsertImage(wordDoc, para, imageFullNameAndPath);

                 _wordDoc.MainDocumentPart.Document.Save();

            //  Body body = wordDoc.MainDocumentPart.Document.Body;
            /*        Uri imageUri = new Uri("/word/media/" +
          System.IO.Path.GetFileName(imageFullNameAndPath), UriKind.Relative);*/

            //Create "image" part in / word / media
            // Change content type for other image types.
            /*  var packageImagePart =  wordDoc.Package.CreatePart(imageUri, "Image/png");

                          // Feed data.
                          byte[] imageBytes = File.ReadAllBytes(imageFullNameAndPath);
                          packageImagePart.GetStream().Write(imageBytes, 0, imageBytes.Length);


                          var documentPackagePart = mainPart.OpenXmlPackage.Package.GetPart(new Uri("/word/document.xml", UriKind.Relative));

                          // URI to the image is relative to releationship document.
                          var imageReleationshipPart = documentPackagePart.CreateRelationship(
                                new Uri("media/" + System.IO.Path.GetFileName(imageFullNameAndPath), UriKind.Relative),
                                TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image");

                         // AddImageToBody(wordDoc, imageReleationshipPart.Id);

                          long iWidth = 0;
                          long iHeight = 0;

                         // ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
                          using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imageFullNameAndPath))
                          {
                              iWidth = bmp.Width;
                              iHeight = bmp.Height;
                          }

                          iWidth = (int)Math.Round((decimal)iWidth * 9525);
                          iHeight = (int)Math.Round((decimal)iHeight * 9525);
                          double maxWidthCm = 16.5;
                          const int emusPerCm = 360000;
                          long maxWidthEmus = (long)(maxWidthCm * emusPerCm);
                          if (iWidth > maxWidthEmus)
                          {
                              var ratio = (iHeight * 1.0m) / iWidth;
                              iWidth = maxWidthEmus;
                              iHeight = (long)(iWidth * ratio);

                          }*/
            /*  Paragraph para = wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph());
              Run run = para.AppendChild(new Run());
              Break pageBreak = run.AppendChild(new Break());
              pageBreak.Type = BreakValues.Page;*/
            /*   using (FileStream stream = new FileStream(imageFullNameAndPath, FileMode.Open))
               {
                   imagePart.FeedData(stream);
                   stream.Close();

               }*/
            /*   using (MemoryStream ms = new MemoryStream())
               {
                   image.Save(ms, imageFormatForSave);
                   byte[] ba = ms.ToArray();
                   using (Stream s = imagePart.GetStream(FileMode.Create, FileAccess.ReadWrite))
                       s.Write(ba, 0, ba.GetUpperBound(0) + 1);
               }*/
            //  AddImageToBody(wordDoc, imageReleationshipPart.Id, imageFullNameAndPath, iWidth, iHeight);
            // wordDoc.Save();
            //   fs.Flush();
            _wordDoc.Save();
           
        }
        private static void AddImageToBody(WordprocessingDocument wordDoc, string imageFullNameAndPath,  string relationshipId, Int64Value cx, Int64Value cy)
        {
          //  WriteDebugConsoleLogEntry(new LogEntry(2, "ReportHelper", "AddImageToBody called"));
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = cx, Cy = cy },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = imageFullNameAndPath
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                       Guid.NewGuid().ToString()
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U//,
                        // EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }
        /*  private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId, int imgWidth, int height)
          {
              // Define the reference of the image.
              var element =
                   new Drawing(
                       new DW.Inline(
                           new DW.Extent() { Cx = imgWidth, Cy = height },
                           new DW.EffectExtent()
                           {
                               LeftEdge = 0L,
                               TopEdge = 0L,
                               RightEdge = 0L,
                               BottomEdge = 0L
                           },
                           new DW.DocProperties()
                           {
                               Id = (UInt32Value)1U,
                               Name = "Picture 1"
                           },
                           new DW.NonVisualGraphicFrameDrawingProperties(
                               new A.GraphicFrameLocks() { NoChangeAspect = true }),
                           new A.Graphic(
                               new A.GraphicData(
                                   new PIC.Picture(
                                       new PIC.NonVisualPictureProperties(
                                           new PIC.NonVisualDrawingProperties()
                                           {
                                               Id = (UInt32Value)0U,
                                               Name = "New Bitmap Image.jpg"
                                           },
                                           new PIC.NonVisualPictureDrawingProperties()),
                                       new PIC.BlipFill(
                                           new A.Blip(
                                               new A.BlipExtensionList(
                                                   new A.BlipExtension()
                                                   {
                                                       Uri =
                                                          "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                   })
                                           )
                                           {
                                               Embed = relationshipId,
                                               CompressionState =
                                               A.BlipCompressionValues.Print
                                           },
                                           new A.Stretch(
                                               new A.FillRectangle())),
                                       new PIC.ShapeProperties(
                                           new A.Transform2D(
                                               new A.Offset() { X = 0L, Y = 0L },
                                               new A.Extents() { Cx = imgWidth, Cy = height }),
                                           new A.PresetGeometry(
                                               new A.AdjustValueList()
                                           )
                                           { Preset = A.ShapeTypeValues.Rectangle }))
                               )
                               { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                       )
                       {
                           DistanceFromTop = (UInt32Value)0U,
                           DistanceFromBottom = (UInt32Value)0U,
                           DistanceFromLeft = (UInt32Value)0U,
                           DistanceFromRight = (UInt32Value)0U,
                           EditId = "50D07946"
                       });

              // Append the reference to body, the element should be in a Run.
              wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
          }*/

        public void CreateTestCaseSummaryTable(IEnumerable<TestCaseSummaryAttribute> summary)
        {
            Body body = _wordDoc.MainDocumentPart.Document.Body;
            MSWordTableBuilder tableBuilder = new MSWordTableBuilder();
            tableBuilder.CreateSummaryTable(body, summary );
            _wordDoc.Save();
           
        }
        public void CreateTestCaseTable(string testDescription, IEnumerable<TestCaseTableRow> rows)
        {
            Body body = _wordDoc.MainDocumentPart.Document.Body;
            MSWordTableBuilder tableBuilder = new MSWordTableBuilder();
            tableBuilder.CreateTestCaseTable(body, testDescription, rows);
            _wordDoc.Save();
        }



        private  string CloneTemplateFileForEditing()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                    .Single(str => str.EndsWith("ConstructionUnitTestTemplate.docx"));
            try
            {
             
                var fullFileName = $"{_workingDirectory}\\{_coNumber}.docx";
                File.Delete(fullFileName);
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (FileStream mem = File.OpenWrite(fullFileName))
                {
                    stream.Position = 0;
                    stream.CopyTo(mem);
                    stream.Flush();
                
                }
                return fullFileName;
            }
            catch (Exception e)
            {

                throw;
            }
        }

       
        public void Dispose()
        {
            _wordDoc.Close();
        }
    }

   
}
