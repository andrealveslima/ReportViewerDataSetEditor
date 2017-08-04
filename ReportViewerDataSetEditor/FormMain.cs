using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportViewerDataSetEditor
{
    public partial class FormMain : Form
    {
        private DataSetParser _dataSetParser;

        public FormMain()
        {
            InitializeComponent();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "RDLC Files (*.rdlc)|*.rdlc";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbReportPath.Text = dialog.FileName;
                    _dataSetParser = new DataSetParser(tbReportPath.Text);
                    cbDataSet.Enabled = true;
                    cbDataSet.DataSource = _dataSetParser.DataSetNames;
                }
            }
        }

        private void cbDataSet_SelectedValueChanged(object sender, EventArgs e)
        {
            dgvColumns.DataSource = _dataSetParser.DataSets[cbDataSet.SelectedValue.ToString()];
        }
    }
}
