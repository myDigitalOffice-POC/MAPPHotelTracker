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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.CBoxColumnName = new System.Windows.Forms.ComboBox();
            this.TextBoxTAMessage = new System.Windows.Forms.TextBox();
            this.AuditGridFilterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AuditGridFilterComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuditRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnGetAllSubmittals
            // 
            this.BtnGetAllSubmittals.Location = new System.Drawing.Point(22, 14);
            this.BtnGetAllSubmittals.Name = "BtnGetAllSubmittals";
            this.BtnGetAllSubmittals.Size = new System.Drawing.Size(130, 24);
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
            this.dataGridView1.Location = new System.Drawing.Point(22, 116);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.Size = new System.Drawing.Size(920, 219);
            this.dataGridView1.TabIndex = 6;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(854, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(79, 24);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // textBoxNonSubmittalCount
            // 
            this.textBoxNonSubmittalCount.Enabled = false;
            this.textBoxNonSubmittalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNonSubmittalCount.Location = new System.Drawing.Point(371, 85);
            this.textBoxNonSubmittalCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxNonSubmittalCount.Name = "textBoxNonSubmittalCount";
            this.textBoxNonSubmittalCount.Size = new System.Drawing.Size(292, 21);
            this.textBoxNonSubmittalCount.TabIndex = 13;
            // 
            // BtnClearSelection
            // 
            this.BtnClearSelection.Location = new System.Drawing.Point(157, 49);
            this.BtnClearSelection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnClearSelection.Name = "BtnClearSelection";
            this.BtnClearSelection.Size = new System.Drawing.Size(107, 24);
            this.BtnClearSelection.TabIndex = 9;
            this.BtnClearSelection.Text = "Clear Selection";
            this.BtnClearSelection.UseVisualStyleBackColor = true;
            this.BtnClearSelection.Click += new System.EventHandler(this.BtnClearSelection_Click);
            // 
            // CommonSearchTextBox
            // 
            this.CommonSearchTextBox.Location = new System.Drawing.Point(22, 87);
            this.CommonSearchTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CommonSearchTextBox.Name = "CommonSearchTextBox";
            this.CommonSearchTextBox.Size = new System.Drawing.Size(121, 20);
            this.CommonSearchTextBox.TabIndex = 17;
            this.CommonSearchTextBox.TextChanged += new System.EventHandler(this.TxtContainsText_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Search By -";
            // 
            // BtnProcessSelectedEmailAdr
            // 
            this.BtnProcessSelectedEmailAdr.Location = new System.Drawing.Point(289, 14);
            this.BtnProcessSelectedEmailAdr.Name = "BtnProcessSelectedEmailAdr";
            this.BtnProcessSelectedEmailAdr.Size = new System.Drawing.Size(142, 24);
            this.BtnProcessSelectedEmailAdr.TabIndex = 22;
            this.BtnProcessSelectedEmailAdr.Text = "Process Selected Group";
            this.BtnProcessSelectedEmailAdr.UseVisualStyleBackColor = true;
            this.BtnProcessSelectedEmailAdr.Click += new System.EventHandler(this.BtnProcessSelectedEmailAdr_Click);
            // 
            // ProcessAllRowsBtn
            // 
            this.ProcessAllRowsBtn.Location = new System.Drawing.Point(437, 14);
            this.ProcessAllRowsBtn.Name = "ProcessAllRowsBtn";
            this.ProcessAllRowsBtn.Size = new System.Drawing.Size(130, 24);
            this.ProcessAllRowsBtn.TabIndex = 23;
            this.ProcessAllRowsBtn.Text = "Process All Rows";
            this.ProcessAllRowsBtn.UseVisualStyleBackColor = true;
            this.ProcessAllRowsBtn.Click += new System.EventHandler(this.ProcessAllRowsBtn_Click);
            // 
            // BtnEmailAuditTrail
            // 
            this.BtnEmailAuditTrail.Location = new System.Drawing.Point(157, 14);
            this.BtnEmailAuditTrail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnEmailAuditTrail.Name = "BtnEmailAuditTrail";
            this.BtnEmailAuditTrail.Size = new System.Drawing.Size(127, 24);
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
            this.dataGridViewAuditRows.Location = new System.Drawing.Point(22, 369);
            this.dataGridViewAuditRows.MultiSelect = false;
            this.dataGridViewAuditRows.Name = "dataGridViewAuditRows";
            this.dataGridViewAuditRows.RowHeadersWidth = 72;
            this.dataGridViewAuditRows.Size = new System.Drawing.Size(920, 180);
            this.dataGridViewAuditRows.TabIndex = 26;
            this.dataGridViewAuditRows.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAuditRows_CellMouseClick);
            this.dataGridViewAuditRows.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewAuditRows_CellPainting);
            // 
            // btnGetAuditRows
            // 
            this.btnGetAuditRows.Location = new System.Drawing.Point(22, 49);
            this.btnGetAuditRows.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGetAuditRows.Name = "btnGetAuditRows";
            this.btnGetAuditRows.Size = new System.Drawing.Size(107, 24);
            this.btnGetAuditRows.TabIndex = 27;
            this.btnGetAuditRows.Text = "Get Audit Rows";
            this.btnGetAuditRows.UseVisualStyleBackColor = true;
            this.btnGetAuditRows.Click += new System.EventHandler(this.btnGetAuditRows_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(22, 555);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 72;
            this.dataGridView2.Size = new System.Drawing.Size(920, 135);
            this.dataGridView2.TabIndex = 32;
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
            "Group Id"});
            this.CBoxColumnName.Location = new System.Drawing.Point(221, 87);
            this.CBoxColumnName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBoxColumnName.Name = "CBoxColumnName";
            this.CBoxColumnName.Size = new System.Drawing.Size(113, 21);
            this.CBoxColumnName.TabIndex = 34;
            this.CBoxColumnName.SelectedIndexChanged += new System.EventHandler(this.CBoxColumnName_SelectedIndexChanged);
            // 
            // TextBoxTAMessage
            // 
            this.TextBoxTAMessage.Enabled = false;
            this.TextBoxTAMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxTAMessage.Location = new System.Drawing.Point(730, 86);
            this.TextBoxTAMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TextBoxTAMessage.Name = "TextBoxTAMessage";
            this.TextBoxTAMessage.Size = new System.Drawing.Size(213, 21);
            this.TextBoxTAMessage.TabIndex = 35;
            // 
            // AuditGridFilterTextBox
            // 
            this.AuditGridFilterTextBox.Location = new System.Drawing.Point(22, 344);
            this.AuditGridFilterTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.AuditGridFilterTextBox.Name = "AuditGridFilterTextBox";
            this.AuditGridFilterTextBox.Size = new System.Drawing.Size(121, 20);
            this.AuditGridFilterTextBox.TabIndex = 36;
            this.AuditGridFilterTextBox.TextChanged += new System.EventHandler(this.AuditGridFilterTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 344);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
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
            this.AuditGridFilterComboBox.Location = new System.Drawing.Point(221, 343);
            this.AuditGridFilterComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.AuditGridFilterComboBox.Name = "AuditGridFilterComboBox";
            this.AuditGridFilterComboBox.Size = new System.Drawing.Size(113, 21);
            this.AuditGridFilterComboBox.TabIndex = 38;
            this.AuditGridFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.AuditGridFilterComboBox_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 574);
            this.Controls.Add(this.AuditGridFilterComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AuditGridFilterTextBox);
            this.Controls.Add(this.TextBoxTAMessage);
            this.Controls.Add(this.CBoxColumnName);
            this.Controls.Add(this.dataGridView2);
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
            this.Name = "Form1";
            this.Text = "Automate MAPP";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuditRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox CBoxColumnName;
        private System.Windows.Forms.TextBox TextBoxTAMessage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.TextBox AuditGridFilterTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox AuditGridFilterComboBox;
    }
}
