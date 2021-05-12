using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using TestLibrary.Common;

namespace Test.InfraStructure.Common
{
    public class ReportWriterFactory
    {
        public IReportWriter GetReportWriter()
        {
            switch (ConfigurationManager.AppSettings["WriterType"])
            {
                case "WORD":


                    return new MSWordHelper();

                default:
                    return null;
            }
        }
    }
}
