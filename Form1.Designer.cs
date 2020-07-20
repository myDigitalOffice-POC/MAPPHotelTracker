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
            this.BtnProcessAllGroups = new System.Windows.Forms.Button();
            this.BtnGetAllSubmittals = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.textBoxNonSubmittalCount = new System.Windows.Forms.TextBox();
            this.BtnClearSelection = new System.Windows.Forms.Button();
            this.TxtContainsText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnProcessSelectedEmailAdr = new System.Windows.Forms.Button();
            this.ProcessAllRowsBtn = new System.Windows.Forms.Button();
            this.BtnEmailAuditTrail = new System.Windows.Forms.Button();
            this.dataGridViewAuditRows = new System.Windows.Forms.DataGridView();
            this.btnGetAuditRows = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuditRows)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnProcessAllGroups
            // 
            this.BtnProcessAllGroups.Location = new System.Drawing.Point(1319, 28);
            this.BtnProcessAllGroups.Margin = new System.Windows.Forms.Padding(6);
            this.BtnProcessAllGroups.Name = "BtnProcessAllGroups";
            this.BtnProcessAllGroups.Size = new System.Drawing.Size(238, 43);
            this.BtnProcessAllGroups.TabIndex = 5;
            this.BtnProcessAllGroups.Text = "Get Audit Messages";
            this.BtnProcessAllGroups.UseVisualStyleBackColor = true;
            this.BtnProcessAllGroups.Click += new System.EventHandler(this.BtnProcessAllGroups_Click);
            // 
            // BtnGetAllSubmittals
            // 
            this.BtnGetAllSubmittals.Location = new System.Drawing.Point(40, 25);
            this.BtnGetAllSubmittals.Margin = new System.Windows.Forms.Padding(6);
            this.BtnGetAllSubmittals.Name = "BtnGetAllSubmittals";
            this.BtnGetAllSubmittals.Size = new System.Drawing.Size(238, 45);
            this.BtnGetAllSubmittals.TabIndex = 2;
            this.BtnGetAllSubmittals.Text = "Get Email Submittals";
            this.BtnGetAllSubmittals.UseVisualStyleBackColor = true;
            this.BtnGetAllSubmittals.Click += new System.EventHandler(this.BtnGetAllSubmittals_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(40, 169);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.Size = new System.Drawing.Size(1687, 404);
            this.dataGridView1.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1583, 27);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(144, 44);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // textBoxNonSubmittalCount
            // 
            this.textBoxNonSubmittalCount.Location = new System.Drawing.Point(40, 116);
            this.textBoxNonSubmittalCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxNonSubmittalCount.Name = "textBoxNonSubmittalCount";
            this.textBoxNonSubmittalCount.Size = new System.Drawing.Size(626, 29);
            this.textBoxNonSubmittalCount.TabIndex = 13;
            // 
            // BtnClearSelection
            // 
            this.BtnClearSelection.Location = new System.Drawing.Point(1103, 26);
            this.BtnClearSelection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnClearSelection.Name = "BtnClearSelection";
            this.BtnClearSelection.Size = new System.Drawing.Size(197, 45);
            this.BtnClearSelection.TabIndex = 9;
            this.BtnClearSelection.Text = "Clear Selection";
            this.BtnClearSelection.UseVisualStyleBackColor = true;
            this.BtnClearSelection.Click += new System.EventHandler(this.BtnClearSelection_Click);
            // 
            // TxtContainsText
            // 
            this.TxtContainsText.Location = new System.Drawing.Point(1046, 113);
            this.TxtContainsText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TxtContainsText.Name = "TxtContainsText";
            this.TxtContainsText.Size = new System.Drawing.Size(218, 29);
            this.TxtContainsText.TabIndex = 17;
            this.TxtContainsText.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(898, 116);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Search String";
            this.label5.Visible = false;
            // 
            // BtnProcessSelectedEmailAdr
            // 
            this.BtnProcessSelectedEmailAdr.Location = new System.Drawing.Point(569, 26);
            this.BtnProcessSelectedEmailAdr.Margin = new System.Windows.Forms.Padding(6);
            this.BtnProcessSelectedEmailAdr.Name = "BtnProcessSelectedEmailAdr";
            this.BtnProcessSelectedEmailAdr.Size = new System.Drawing.Size(238, 45);
            this.BtnProcessSelectedEmailAdr.TabIndex = 22;
            this.BtnProcessSelectedEmailAdr.Text = "Process Selected Group";
            this.BtnProcessSelectedEmailAdr.UseVisualStyleBackColor = true;
            this.BtnProcessSelectedEmailAdr.Click += new System.EventHandler(this.BtnProcessSelectedEmailAdr_Click);
            // 
            // ProcessAllRowsBtn
            // 
            this.ProcessAllRowsBtn.Location = new System.Drawing.Point(839, 25);
            this.ProcessAllRowsBtn.Margin = new System.Windows.Forms.Padding(6);
            this.ProcessAllRowsBtn.Name = "ProcessAllRowsBtn";
            this.ProcessAllRowsBtn.Size = new System.Drawing.Size(238, 45);
            this.ProcessAllRowsBtn.TabIndex = 23;
            this.ProcessAllRowsBtn.Text = "Process All Rows";
            this.ProcessAllRowsBtn.UseVisualStyleBackColor = true;
            this.ProcessAllRowsBtn.Click += new System.EventHandler(this.ProcessAllRowsBtn_Click);
            // 
            // BtnEmailAuditTrail
            // 
            this.BtnEmailAuditTrail.Location = new System.Drawing.Point(309, 25);
            this.BtnEmailAuditTrail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnEmailAuditTrail.Name = "BtnEmailAuditTrail";
            this.BtnEmailAuditTrail.Size = new System.Drawing.Size(232, 44);
            this.BtnEmailAuditTrail.TabIndex = 25;
            this.BtnEmailAuditTrail.Text = "Email Audit Trail";
            this.BtnEmailAuditTrail.UseVisualStyleBackColor = true;
            this.BtnEmailAuditTrail.Click += new System.EventHandler(this.BtnEmailAuditTrail_Click);
            // 
            // dataGridViewAuditRows
            // 
            this.dataGridViewAuditRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAuditRows.Location = new System.Drawing.Point(40, 585);
            this.dataGridViewAuditRows.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridViewAuditRows.MultiSelect = false;
            this.dataGridViewAuditRows.Name = "dataGridViewAuditRows";
            this.dataGridViewAuditRows.RowHeadersWidth = 72;
            this.dataGridViewAuditRows.Size = new System.Drawing.Size(1687, 348);
            this.dataGridViewAuditRows.TabIndex = 26;
            // 
            // btnGetAuditRows
            // 
            this.btnGetAuditRows.Location = new System.Drawing.Point(1292, 106);
            this.btnGetAuditRows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGetAuditRows.Name = "btnGetAuditRows";
            this.btnGetAuditRows.Size = new System.Drawing.Size(197, 45);
            this.btnGetAuditRows.TabIndex = 27;
            this.btnGetAuditRows.Text = "Get Audit Rows";
            this.btnGetAuditRows.UseVisualStyleBackColor = true;
            this.btnGetAuditRows.Click += new System.EventHandler(this.btnGetAuditRows_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2276, 1404);
            this.Controls.Add(this.btnGetAuditRows);
            this.Controls.Add(this.dataGridViewAuditRows);
            this.Controls.Add(this.BtnEmailAuditTrail);
            this.Controls.Add(this.ProcessAllRowsBtn);
            this.Controls.Add(this.BtnProcessSelectedEmailAdr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtContainsText);
            this.Controls.Add(this.textBoxNonSubmittalCount);
            this.Controls.Add(this.BtnClearSelection);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BtnGetAllSubmittals);
            this.Controls.Add(this.BtnProcessAllGroups);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Automate MAPP";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAuditRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnProcessAllGroups;
        private System.Windows.Forms.Button BtnGetAllSubmittals;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox textBoxNonSubmittalCount;
        private System.Windows.Forms.Button BtnClearSelection;
        private System.Windows.Forms.TextBox TxtContainsText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnProcessSelectedEmailAdr;
        private System.Windows.Forms.Button ProcessAllRowsBtn;
        private System.Windows.Forms.Button BtnEmailAuditTrail;
        private System.Windows.Forms.DataGridView dataGridViewAuditRows;
        private System.Windows.Forms.Button btnGetAuditRows;
    }
}
