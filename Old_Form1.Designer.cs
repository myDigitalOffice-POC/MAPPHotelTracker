namespace MAPPOnBoardingStats
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.BtnGetAllSubmittals = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExit = new System.Windows.Forms.Button();
            this.textBoxNonSubmittalCount = new System.Windows.Forms.TextBox();
            this.BtnClearSelection = new System.Windows.Forms.Button();
            this.CommonSearchTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnProcessSelectedEmailAdr = new System.Windows.Forms.Button();
            this.ProcessAllRowsBtn = new System.Windows.Forms.Button();
            this.BtnEmailAuditTrail = new System.Windows.Forms.Button();
            this.dataGridViewAuditRows = new System.Windows.Forms.DataGridView();
            this.btnGetAuditRows = new System.Windows.Forms.Button();
            this.ResolutionsDataGrid = new System.Windows.Forms.DataGridView();
            this.HotelManagementGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HotelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resolutions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CBoxColumnName = new System.Windows.Forms.ComboBox();
            this.TextBoxTAMessage = new System.Windows.Forms.TextBox();
            this.AuditGridFilterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AuditGridFilterComboBox = new System.Windows.Forms.ComboBox();
            this.GroupNameAutoCompleteBox = new System.Windows.Forms.TextBox();
            this.CBoxGroupName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuditRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResolutionsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGetAllSubmittals
            // 
            this.BtnGetAllSubmittals.Location = new System.Drawing.Point(40, 26);
            this.BtnGetAllSubmittals.Margin = new System.Windows.Forms.Padding(6);
            this.BtnGetAllSubmittals.Name = "BtnGetAllSubmittals";
            this.BtnGetAllSubmittals.Size = new System.Drawing.Size(238, 44);
            this.BtnGetAllSubmittals.TabIndex = 2;
            this.BtnGetAllSubmittals.Text = "Get Email Submittals";
            this.BtnGetAllSubmittals.UseVisualStyleBackColor = true;
            this.BtnGetAllSubmittals.Click += new System.EventHandler(this.BtnGetAllSubmittals_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(40, 214);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.Size = new System.Drawing.Size(1687, 404);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 76);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(135, 36);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(135, 36);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1566, 26);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(145, 44);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // textBoxNonSubmittalCount
            // 
            this.textBoxNonSubmittalCount.Enabled = false;
            this.textBoxNonSubmittalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNonSubmittalCount.Location = new System.Drawing.Point(530, 96);
            this.textBoxNonSubmittalCount.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNonSubmittalCount.Name = "textBoxNonSubmittalCount";
            this.textBoxNonSubmittalCount.Size = new System.Drawing.Size(532, 31);
            this.textBoxNonSubmittalCount.TabIndex = 13;
            this.textBoxNonSubmittalCount.TextChanged += new System.EventHandler(this.textBoxNonSubmittalCount_TextChanged);
            // 
            // BtnClearSelection
            // 
            this.BtnClearSelection.Location = new System.Drawing.Point(288, 90);
            this.BtnClearSelection.Margin = new System.Windows.Forms.Padding(4);
            this.BtnClearSelection.Name = "BtnClearSelection";
            this.BtnClearSelection.Size = new System.Drawing.Size(196, 44);
            this.BtnClearSelection.TabIndex = 9;
            this.BtnClearSelection.Text = "Clear Selection";
            this.BtnClearSelection.UseVisualStyleBackColor = true;
            this.BtnClearSelection.Click += new System.EventHandler(this.BtnClearSelection_Click);
            // 
            // CommonSearchTextBox
            // 
            this.CommonSearchTextBox.Location = new System.Drawing.Point(1497, 162);
            this.CommonSearchTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.CommonSearchTextBox.Name = "CommonSearchTextBox";
            this.CommonSearchTextBox.Size = new System.Drawing.Size(230, 29);
            this.CommonSearchTextBox.TabIndex = 17;
            this.CommonSearchTextBox.TextChanged += new System.EventHandler(this.TxtContainsText_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1141, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Search By -";
            // 
            // BtnProcessSelectedEmailAdr
            // 
            this.BtnProcessSelectedEmailAdr.Location = new System.Drawing.Point(530, 26);
            this.BtnProcessSelectedEmailAdr.Margin = new System.Windows.Forms.Padding(6);
            this.BtnProcessSelectedEmailAdr.Name = "BtnProcessSelectedEmailAdr";
            this.BtnProcessSelectedEmailAdr.Size = new System.Drawing.Size(260, 44);
            this.BtnProcessSelectedEmailAdr.TabIndex = 22;
            this.BtnProcessSelectedEmailAdr.Text = "Process Selected Group";
            this.BtnProcessSelectedEmailAdr.UseVisualStyleBackColor = true;
            this.BtnProcessSelectedEmailAdr.Click += new System.EventHandler(this.BtnProcessSelectedEmailAdr_Click);
            // 
            // ProcessAllRowsBtn
            // 
            this.ProcessAllRowsBtn.Location = new System.Drawing.Point(801, 26);
            this.ProcessAllRowsBtn.Margin = new System.Windows.Forms.Padding(6);
            this.ProcessAllRowsBtn.Name = "ProcessAllRowsBtn";
            this.ProcessAllRowsBtn.Size = new System.Drawing.Size(238, 44);
            this.ProcessAllRowsBtn.TabIndex = 23;
            this.ProcessAllRowsBtn.Text = "Process All Rows";
            this.ProcessAllRowsBtn.UseVisualStyleBackColor = true;
            this.ProcessAllRowsBtn.Click += new System.EventHandler(this.ProcessAllRowsBtn_Click);
            // 
            // BtnEmailAuditTrail
            // 
            this.BtnEmailAuditTrail.Location = new System.Drawing.Point(288, 26);
            this.BtnEmailAuditTrail.Margin = new System.Windows.Forms.Padding(4);
            this.BtnEmailAuditTrail.Name = "BtnEmailAuditTrail";
            this.BtnEmailAuditTrail.Size = new System.Drawing.Size(233, 44);
            this.BtnEmailAuditTrail.TabIndex = 25;
            this.BtnEmailAuditTrail.Text = "Email Audit Trail";
            this.BtnEmailAuditTrail.UseVisualStyleBackColor = true;
            this.BtnEmailAuditTrail.Click += new System.EventHandler(this.BtnEmailAuditTrail_Click);
            // 
            // dataGridViewAuditRows
            // 
            this.dataGridViewAuditRows.AllowUserToAddRows = false;
            this.dataGridViewAuditRows.AllowUserToDeleteRows = false;
            this.dataGridViewAuditRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAuditRows.Location = new System.Drawing.Point(40, 681);
            this.dataGridViewAuditRows.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridViewAuditRows.MultiSelect = false;
            this.dataGridViewAuditRows.Name = "dataGridViewAuditRows";
            this.dataGridViewAuditRows.RowHeadersWidth = 72;
            this.dataGridViewAuditRows.Size = new System.Drawing.Size(1687, 332);
            this.dataGridViewAuditRows.TabIndex = 26;
            this.dataGridViewAuditRows.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAuditRows_CellMouseClick);
            this.dataGridViewAuditRows.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewAuditRows_CellPainting);
            // 
            // btnGetAuditRows
            // 
            this.btnGetAuditRows.Location = new System.Drawing.Point(40, 90);
            this.btnGetAuditRows.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetAuditRows.Name = "btnGetAuditRows";
            this.btnGetAuditRows.Size = new System.Drawing.Size(196, 44);
            this.btnGetAuditRows.TabIndex = 27;
            this.btnGetAuditRows.Text = "Get Audit Rows";
            this.btnGetAuditRows.UseVisualStyleBackColor = true;
            this.btnGetAuditRows.Click += new System.EventHandler(this.btnGetAuditRows_Click);
            // 
            // ResolutionsDataGrid
            // 
            this.ResolutionsDataGrid.AllowUserToAddRows = false;
            this.ResolutionsDataGrid.AllowUserToDeleteRows = false;
            this.ResolutionsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResolutionsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HotelManagementGroup,
            this.HotelName,
            this.Resolutions});
            this.ResolutionsDataGrid.Location = new System.Drawing.Point(40, 1025);
            this.ResolutionsDataGrid.Margin = new System.Windows.Forms.Padding(6);
            this.ResolutionsDataGrid.Name = "ResolutionsDataGrid";
            this.ResolutionsDataGrid.ReadOnly = true;
            this.ResolutionsDataGrid.RowHeadersVisible = false;
            this.ResolutionsDataGrid.RowHeadersWidth = 72;
            this.ResolutionsDataGrid.Size = new System.Drawing.Size(1687, 249);
            this.ResolutionsDataGrid.TabIndex = 32;
            this.ResolutionsDataGrid.Visible = false;
            // 
            // HotelManagementGroup
            // 
            this.HotelManagementGroup.HeaderText = "Hotel Management Group";
            this.HotelManagementGroup.MinimumWidth = 9;
            this.HotelManagementGroup.Name = "HotelManagementGroup";
            this.HotelManagementGroup.ReadOnly = true;
            this.HotelManagementGroup.Width = 175;
            // 
            // HotelName
            // 
            this.HotelName.HeaderText = "Hotel Name";
            this.HotelName.MinimumWidth = 9;
            this.HotelName.Name = "HotelName";
            this.HotelName.ReadOnly = true;
            this.HotelName.Width = 150;
            // 
            // Resolutions
            // 
            this.Resolutions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Resolutions.HeaderText = "Resolutions";
            this.Resolutions.MinimumWidth = 9;
            this.Resolutions.Name = "Resolutions";
            this.Resolutions.ReadOnly = true;
            // 
            // CBoxColumnName
            // 
            this.CBoxColumnName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxColumnName.FormattingEnabled = true;
            this.CBoxColumnName.Items.AddRange(new object[] {
            "Group Name",
            "Hotel Name",
            "Hotel Code",
            "Hotel Id",
            "Group Id",
            "Receiving Pace Reports"});
            this.CBoxColumnName.Location = new System.Drawing.Point(1262, 162);
            this.CBoxColumnName.Margin = new System.Windows.Forms.Padding(4);
            this.CBoxColumnName.Name = "CBoxColumnName";
            this.CBoxColumnName.Size = new System.Drawing.Size(211, 32);
            this.CBoxColumnName.TabIndex = 34;
            this.CBoxColumnName.SelectedIndexChanged += new System.EventHandler(this.CBoxColumnName_SelectedIndexChanged);
            // 
            // TextBoxTAMessage
            // 
            this.TextBoxTAMessage.Enabled = false;
            this.TextBoxTAMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxTAMessage.Location = new System.Drawing.Point(1073, 96);
            this.TextBoxTAMessage.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxTAMessage.Name = "TextBoxTAMessage";
            this.TextBoxTAMessage.Size = new System.Drawing.Size(493, 31);
            this.TextBoxTAMessage.TabIndex = 35;
            // 
            // AuditGridFilterTextBox
            // 
            this.AuditGridFilterTextBox.Location = new System.Drawing.Point(40, 635);
            this.AuditGridFilterTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AuditGridFilterTextBox.Name = "AuditGridFilterTextBox";
            this.AuditGridFilterTextBox.Size = new System.Drawing.Size(219, 29);
            this.AuditGridFilterTextBox.TabIndex = 36;
            this.AuditGridFilterTextBox.TextChanged += new System.EventHandler(this.AuditGridFilterTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 635);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 37;
            this.label1.Text = "Search By -";
            // 
            // AuditGridFilterComboBox
            // 
            this.AuditGridFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AuditGridFilterComboBox.FormattingEnabled = true;
            this.AuditGridFilterComboBox.Items.AddRange(new object[] {
            "Hotel ID",
            "From Address",
            "To Address"});
            this.AuditGridFilterComboBox.Location = new System.Drawing.Point(405, 633);
            this.AuditGridFilterComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.AuditGridFilterComboBox.Name = "AuditGridFilterComboBox";
            this.AuditGridFilterComboBox.Size = new System.Drawing.Size(204, 32);
            this.AuditGridFilterComboBox.TabIndex = 38;
            this.AuditGridFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.AuditGridFilterComboBox_SelectedIndexChanged);
            // 
            // GroupNameAutoCompleteBox
            // 
            this.GroupNameAutoCompleteBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.GroupNameAutoCompleteBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.GroupNameAutoCompleteBox.Location = new System.Drawing.Point(1279, 33);
            this.GroupNameAutoCompleteBox.Margin = new System.Windows.Forms.Padding(6);
            this.GroupNameAutoCompleteBox.Name = "GroupNameAutoCompleteBox";
            this.GroupNameAutoCompleteBox.Size = new System.Drawing.Size(83, 29);
            this.GroupNameAutoCompleteBox.TabIndex = 39;
            this.GroupNameAutoCompleteBox.TextChanged += new System.EventHandler(this.GroupNameAutoCompleteBox_TextChanged);
            // 
            // CBoxGroupName
            // 
            this.CBoxGroupName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CBoxGroupName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CBoxGroupName.FormattingEnabled = true;
            this.CBoxGroupName.Location = new System.Drawing.Point(44, 159);
            this.CBoxGroupName.Name = "CBoxGroupName";
            this.CBoxGroupName.Size = new System.Drawing.Size(336, 32);
            this.CBoxGroupName.TabIndex = 40;
            this.CBoxGroupName.SelectedIndexChanged += new System.EventHandler(this.CBoxGroupName_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1767, 1060);
            this.Controls.Add(this.CBoxGroupName);
            this.Controls.Add(this.GroupNameAutoCompleteBox);
            this.Controls.Add(this.AuditGridFilterComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AuditGridFilterTextBox);
            this.Controls.Add(this.TextBoxTAMessage);
            this.Controls.Add(this.CBoxColumnName);
            this.Controls.Add(this.ResolutionsDataGrid);
            this.Controls.Add(this.btnGetAuditRows);
            this.Controls.Add(this.dataGridViewAuditRows);
            this.Controls.Add(this.BtnEmailAuditTrail);
            this.Controls.Add(this.ProcessAllRowsBtn);
            this.Controls.Add(this.BtnProcessSelectedEmailAdr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CommonSearchTextBox);
            this.Controls.Add(this.textBoxNonSubmittalCount);
            this.Controls.Add(this.BtnClearSelection);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BtnGetAllSubmittals);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Automate MAPP";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuditRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResolutionsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnGetAllSubmittals;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox textBoxNonSubmittalCount;
        private System.Windows.Forms.Button BtnClearSelection;
        private System.Windows.Forms.TextBox CommonSearchTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnProcessSelectedEmailAdr;
        private System.Windows.Forms.Button ProcessAllRowsBtn;
        private System.Windows.Forms.Button BtnEmailAuditTrail;
        private System.Windows.Forms.DataGridView dataGridViewAuditRows;
        private System.Windows.Forms.Button btnGetAuditRows;
        private System.Windows.Forms.DataGridView ResolutionsDataGrid;
        private System.Windows.Forms.ComboBox CBoxColumnName;
        private System.Windows.Forms.TextBox TextBoxTAMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.TextBox AuditGridFilterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AuditGridFilterComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotelManagementGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resolutions;
        private System.Windows.Forms.TextBox GroupNameAutoCompleteBox;
        private System.Windows.Forms.ComboBox CBoxGroupName;
    }
}
