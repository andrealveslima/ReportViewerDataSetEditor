namespace ReportViewerDataSetEditor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblReportPath = new System.Windows.Forms.Label();
            this.tbReportPath = new System.Windows.Forms.TextBox();
            this.btOpen = new System.Windows.Forms.Button();
            this.lblDataSet = new System.Windows.Forms.Label();
            this.cbDataSet = new System.Windows.Forms.ComboBox();
            this.btSaveChanges = new System.Windows.Forms.Button();
            this.lblColumns = new System.Windows.Forms.Label();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.ColumnDataField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btNewDataSet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReportPath
            // 
            this.lblReportPath.AutoSize = true;
            this.lblReportPath.Location = new System.Drawing.Point(13, 13);
            this.lblReportPath.Name = "lblReportPath";
            this.lblReportPath.Size = new System.Drawing.Size(64, 13);
            this.lblReportPath.TabIndex = 0;
            this.lblReportPath.Text = "Report Path";
            // 
            // tbReportPath
            // 
            this.tbReportPath.Enabled = false;
            this.tbReportPath.Location = new System.Drawing.Point(16, 30);
            this.tbReportPath.Name = "tbReportPath";
            this.tbReportPath.Size = new System.Drawing.Size(336, 20);
            this.tbReportPath.TabIndex = 1;
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(358, 28);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(98, 23);
            this.btOpen.TabIndex = 2;
            this.btOpen.Text = "Open";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // lblDataSet
            // 
            this.lblDataSet.AutoSize = true;
            this.lblDataSet.Location = new System.Drawing.Point(13, 53);
            this.lblDataSet.Name = "lblDataSet";
            this.lblDataSet.Size = new System.Drawing.Size(46, 13);
            this.lblDataSet.TabIndex = 0;
            this.lblDataSet.Text = "DataSet";
            // 
            // cbDataSet
            // 
            this.cbDataSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSet.Enabled = false;
            this.cbDataSet.FormattingEnabled = true;
            this.cbDataSet.Location = new System.Drawing.Point(16, 70);
            this.cbDataSet.Name = "cbDataSet";
            this.cbDataSet.Size = new System.Drawing.Size(169, 21);
            this.cbDataSet.TabIndex = 3;
            this.cbDataSet.SelectedValueChanged += new System.EventHandler(this.cbDataSet_SelectedValueChanged);
            // 
            // btSaveChanges
            // 
            this.btSaveChanges.Location = new System.Drawing.Point(462, 28);
            this.btSaveChanges.Name = "btSaveChanges";
            this.btSaveChanges.Size = new System.Drawing.Size(98, 23);
            this.btSaveChanges.TabIndex = 2;
            this.btSaveChanges.Text = "Save changes";
            this.btSaveChanges.UseVisualStyleBackColor = true;
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(13, 94);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(47, 13);
            this.lblColumns.TabIndex = 0;
            this.lblColumns.Text = "Columns";
            // 
            // dgvColumns
            // 
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDataField,
            this.ColumnTypeName});
            this.dgvColumns.Location = new System.Drawing.Point(16, 111);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.Size = new System.Drawing.Size(544, 274);
            this.dgvColumns.TabIndex = 4;
            // 
            // ColumnDataField
            // 
            this.ColumnDataField.DataPropertyName = "DataField";
            this.ColumnDataField.HeaderText = "Data Field";
            this.ColumnDataField.Name = "ColumnDataField";
            this.ColumnDataField.Width = 150;
            // 
            // ColumnTypeName
            // 
            this.ColumnTypeName.DataPropertyName = "TypeName";
            this.ColumnTypeName.HeaderText = "Type Name";
            this.ColumnTypeName.Name = "ColumnTypeName";
            this.ColumnTypeName.Width = 150;
            // 
            // btNewDataSet
            // 
            this.btNewDataSet.Enabled = false;
            this.btNewDataSet.Location = new System.Drawing.Point(191, 68);
            this.btNewDataSet.Name = "btNewDataSet";
            this.btNewDataSet.Size = new System.Drawing.Size(161, 23);
            this.btNewDataSet.TabIndex = 2;
            this.btNewDataSet.Text = "New DataSet";
            this.btNewDataSet.UseVisualStyleBackColor = true;
            this.btNewDataSet.Click += new System.EventHandler(this.btNewDataSet_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 399);
            this.Controls.Add(this.dgvColumns);
            this.Controls.Add(this.cbDataSet);
            this.Controls.Add(this.btSaveChanges);
            this.Controls.Add(this.btNewDataSet);
            this.Controls.Add(this.btOpen);
            this.Controls.Add(this.tbReportPath);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.lblDataSet);
            this.Controls.Add(this.lblReportPath);
            this.Name = "FormMain";
            this.Text = "Report Viewer DataSet Editor";
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReportPath;
        private System.Windows.Forms.TextBox tbReportPath;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.Label lblDataSet;
        private System.Windows.Forms.ComboBox cbDataSet;
        private System.Windows.Forms.Button btSaveChanges;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDataField;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTypeName;
        private System.Windows.Forms.Button btNewDataSet;
    }
}

