using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReportViewerDataSetEditor
{
    public class DataSetParser
    {
        public string[] DataSetNames { get; set; }
        public Dictionary<string, DataTable> DataSets { get; set; }

        public DataSetParser(string reportPath)
        {
            DataSets = new Dictionary<string, DataTable>();

            var doc = XDocument.Load(reportPath);
            var reportTag = doc.Descendants().First();
            var ns = reportTag.GetDefaultNamespace();
            var rdNamespace = reportTag.GetNamespaceOfPrefix("rd");
            var dataSets = reportTag.Descendants(ns + "DataSet");
            var dataSetNames = new List<string>();

            foreach (var dataSet in dataSets)
            {
                var dataSetName = dataSet.Attribute("Name").Value;
                var fields = dataSet.Descendants(ns + "Field");
                var dataTable = CreateDataTable();
                foreach (var field in fields)
                {
                    var dataField = field.Element(ns + "DataField").Value;
                    var typeName = field.Element(rdNamespace + "TypeName").Value;
                    dataTable.Rows.Add(dataField, typeName);
                }

                dataSetNames.Add(dataSetName);
                DataSets.Add(dataSetName, dataTable);
            }

            DataSetNames = dataSetNames.ToArray();
        }

        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("DataField");
            dataTable.Columns.Add("TypeName");
            return dataTable;
        }
    }
}
