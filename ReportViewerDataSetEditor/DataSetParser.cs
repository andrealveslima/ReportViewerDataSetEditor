using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReportViewerDataSetEditor
{
    public class DataSetParser
    {
        private string _reportPath;
        public BindingList<string> DataSetNames { get; private set; }
        public Dictionary<string, DataTable> DataSets { get; private set; }
        public List<string> AvailableTypes { get; private set; }

        public DataSetParser(string reportPath)
        {
            _reportPath = reportPath;
            DataSets = new Dictionary<string, DataTable>();
            DataSetNames = new BindingList<string>();
            AvailableTypes = InitializeAvailableTypes();

            var doc = XDocument.Load(reportPath);
            var reportTag = doc.Descendants().First();
            var ns = reportTag.GetDefaultNamespace();
            var rdNamespace = reportTag.GetNamespaceOfPrefix("rd");
            var dataSets = reportTag.Descendants(ns + "DataSet");

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

                DataSetNames.Add(dataSetName);
                DataSets.Add(dataSetName, dataTable);
            }
        }

        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("DataField");
            dataTable.Columns.Add("TypeName");
            return dataTable;
        }

        public void AddDataSet(string dataSetName)
        {
            DataSets.Add(dataSetName, CreateDataTable());
            DataSetNames.Add(dataSetName);
        }

        private List<string> InitializeAvailableTypes()
        {
            var list = new List<string>();

            list.Add("System.Boolean");
            list.Add("System.Byte");
            list.Add("System.Byte[]");
            list.Add("System.Char");
            list.Add("System.DateTime");
            list.Add("System.DateTimeOffset");
            list.Add("System.Decimal");
            list.Add("System.Double");
            list.Add("System.Guid");
            list.Add("System.Int16");
            list.Add("System.Int32");
            list.Add("System.Int64");
            list.Add("System.Object");
            list.Add("System.SByte");
            list.Add("System.Single");
            list.Add("System.String");
            list.Add("System.TimeSpan");
            list.Add("System.UInt16");
            list.Add("System.UInt32");
            list.Add("System.UInt64");

            return list;
        }

        public void SaveChanges()
        {
            var doc = XDocument.Load(_reportPath);
            var reportTag = doc.Descendants().First();
            var ns = reportTag.GetDefaultNamespace();
            var rdNamespace = reportTag.GetNamespaceOfPrefix("rd");
            var dataSetsElement = reportTag.Descendants(ns + "DataSets");

            if (dataSetsElement == null || !dataSetsElement.Any())
            {
                reportTag.Add(new XElement(ns + "DataSets"));
                dataSetsElement = reportTag.Descendants(ns + "DataSets");
            }

            var dataSetElements = dataSetsElement.Descendants(ns + "DataSet");

            foreach (var dataSetName in DataSetNames)
            {
                if (!dataSetElements.Any(ds => string.Compare(ds.Attribute("Name").Value, dataSetName, StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    var newDataSet = new XElement(ns + "DataSet");
                    newDataSet.SetAttributeValue("Name", dataSetName);
                    newDataSet.Add(new XElement(ns + "Fields"));
                    dataSetsElement.First().Add(newDataSet);
                    dataSetElements = dataSetsElement.Descendants(ns + "DataSet");
                }

                var dataSetElement = dataSetElements.First(ds => string.Compare(ds.Attribute("Name").Value, dataSetName, StringComparison.InvariantCultureIgnoreCase) == 0);

            }

            doc.Save(_reportPath);


            //if (dataSets == null)
            //{
            //    dataSets = new XElement("DataSets");
            //}

            //foreach (var dataSet in dataSets)
            //{
            //    var dataSetName = dataSet.Attribute("Name").Value;
            //    var fields = dataSet.Descendants(ns + "Field");
            //    var dataTable = CreateDataTable();
            //    foreach (var field in fields)
            //    {
            //        var dataField = field.Element(ns + "DataField").Value;
            //        var typeName = field.Element(rdNamespace + "TypeName").Value;
            //        dataTable.Rows.Add(dataField, typeName);
            //    }

            //    DataSetNames.Add(dataSetName);
            //    DataSets.Add(dataSetName, dataTable);
            //}
        }
    }
}
