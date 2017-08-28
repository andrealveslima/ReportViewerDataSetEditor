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
            var defaultNamespace = reportTag.GetDefaultNamespace();
            var rdNamespace = reportTag.GetNamespaceOfPrefix("rd");
            var dataSetsTag = reportTag.Descendants(defaultNamespace + "DataSets");
            var dataSourcesTag = reportTag.Descendants(defaultNamespace + "DataSources");

            if (dataSetsTag == null || !dataSetsTag.Any())
            {
                reportTag.Add(new XElement(defaultNamespace + "DataSets"));
                dataSetsTag = reportTag.Descendants(defaultNamespace + "DataSets");
            }

            if (dataSourcesTag == null || !dataSourcesTag.Any())
            {
                reportTag.Add(new XElement(defaultNamespace + "DataSources"));
                dataSourcesTag = reportTag.Descendants(defaultNamespace + "DataSources");
            }

            var dataSetTags = dataSetsTag.Descendants(defaultNamespace + "DataSet");
            var dataSourceTags = dataSourcesTag.Descendants(defaultNamespace + "DataSource");

            foreach (var dataSetName in DataSetNames)
            {
                if (!dataSetTags.Any(ds => string.Compare(ds.Attribute("Name").Value, dataSetName, StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    var newDataSet = new XElement(defaultNamespace + "DataSet");
                    newDataSet.SetAttributeValue("Name", dataSetName);

                    var queryTag = new XElement(defaultNamespace + "Query");
                    var dataSourceNameTag = new XElement(defaultNamespace + "DataSourceName");
                    dataSourceNameTag.Value = dataSetName;
                    var commandTextTag = new XElement(defaultNamespace + "CommandText");
                    commandTextTag.Value = "/* Local Query */";
                    queryTag.Add(dataSourceNameTag);
                    queryTag.Add(commandTextTag);

                    newDataSet.Add(queryTag);
                    newDataSet.Add(new XElement(defaultNamespace + "Fields"));

                    dataSetsTag.First().Add(newDataSet);
                    dataSetTags = dataSetsTag.Descendants(defaultNamespace + "DataSet");
                }

                if (!dataSourceTags.Any(ds => string.Compare(ds.Attribute("Name").Value, dataSetName, StringComparison.InvariantCultureIgnoreCase) == 0))
                {
                    var newDataSource = new XElement(defaultNamespace + "DataSource");
                    newDataSource.SetAttributeValue("Name", dataSetName);

                    var connectionPropertiesTag = new XElement(defaultNamespace + "ConnectionProperties");
                    var dataProviderTag = new XElement(defaultNamespace + "DataProvider");
                    dataProviderTag.Value = "System.Data.DataSet";
                    var connectStringTag = new XElement(defaultNamespace + "ConnectString");
                    connectStringTag.Value = "/* Local Connection */";
                    connectionPropertiesTag.Add(dataProviderTag);
                    connectionPropertiesTag.Add(connectStringTag);

                    newDataSource.Add(connectionPropertiesTag);
                    dataSourcesTag.First().Add(newDataSource);
                    dataSourceTags = dataSourcesTag.Descendants(defaultNamespace + "DataSource");
                }

                var currentDataSetTag = dataSetTags.First(ds => string.Compare(ds.Attribute("Name").Value, dataSetName, StringComparison.InvariantCultureIgnoreCase) == 0);
                var fieldsTag = currentDataSetTag.Descendants(defaultNamespace + "Fields");
                var fieldTags = fieldsTag.Descendants(defaultNamespace + "Field");

                if (fieldTags.Any())
                {
                    foreach (var fieldTag in fieldTags.ToArray())
                    {
                        fieldTag.Remove();
                    }
                }

                foreach (DataRow field in DataSets[dataSetName].Rows)
                {
                    var fieldName = field["DataField"].ToString();
                    var typeName = field["TypeName"].ToString();

                    var fieldTag = new XElement(defaultNamespace + "Field");
                    fieldTag.SetAttributeValue("Name", fieldName);

                    var dataFieldTag = new XElement(defaultNamespace + "DataField");
                    dataFieldTag.Value = fieldName;
                    var typeNameTag = new XElement(rdNamespace + "TypeName");
                    typeNameTag.Value = typeName;

                    fieldTag.Add(dataFieldTag);
                    fieldTag.Add(typeNameTag);

                    fieldsTag.First().Add(fieldTag);
                }
            }

            doc.Save(_reportPath);
        }
    }
}
