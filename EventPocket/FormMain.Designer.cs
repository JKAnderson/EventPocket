namespace EventPocket
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblHz;
            System.Windows.Forms.Label lblAttached;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFlags = new System.Windows.Forms.DataGridView();
            this.dgvFlagsIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFlagsStateCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.dgvValuesIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvValuesWidthCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvValuesValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nudHertz = new System.Windows.Forms.NumericUpDown();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblAttachedValue = new System.Windows.Forms.Label();
            this.llbUpdate = new System.Windows.Forms.LinkLabel();
            lblHz = new System.Windows.Forms.Label();
            lblAttached = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHertz)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHz
            // 
            lblHz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblHz.AutoSize = true;
            lblHz.Location = new System.Drawing.Point(602, 14);
            lblHz.Name = "lblHz";
            lblHz.Size = new System.Drawing.Size(20, 13);
            lblHz.TabIndex = 3;
            lblHz.Text = "Hz";
            // 
            // lblAttached
            // 
            lblAttached.AutoSize = true;
            lblAttached.Location = new System.Drawing.Point(12, 14);
            lblAttached.Name = "lblAttached";
            lblAttached.Size = new System.Drawing.Size(53, 13);
            lblAttached.TabIndex = 4;
            lblAttached.Text = "Attached:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvFlags, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvValues, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 38);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 403);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvFlags
            // 
            this.dgvFlags.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFlags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFlags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvFlagsIDCol,
            this.dgvFlagsStateCol});
            this.dgvFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFlags.Location = new System.Drawing.Point(0, 0);
            this.dgvFlags.Margin = new System.Windows.Forms.Padding(0);
            this.dgvFlags.Name = "dgvFlags";
            this.dgvFlags.Size = new System.Drawing.Size(308, 403);
            this.dgvFlags.TabIndex = 0;
            // 
            // dgvFlagsIDCol
            // 
            this.dgvFlagsIDCol.HeaderText = "Flag ID";
            this.dgvFlagsIDCol.Name = "dgvFlagsIDCol";
            // 
            // dgvFlagsStateCol
            // 
            this.dgvFlagsStateCol.HeaderText = "Flag State";
            this.dgvFlagsStateCol.Name = "dgvFlagsStateCol";
            this.dgvFlagsStateCol.ReadOnly = true;
            // 
            // dgvValues
            // 
            this.dgvValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvValuesIDCol,
            this.dgvValuesWidthCol,
            this.dgvValuesValueCol});
            this.dgvValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvValues.Location = new System.Drawing.Point(308, 0);
            this.dgvValues.Margin = new System.Windows.Forms.Padding(0);
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.Size = new System.Drawing.Size(308, 403);
            this.dgvValues.TabIndex = 1;
            // 
            // dgvValuesIDCol
            // 
            this.dgvValuesIDCol.HeaderText = "Base ID";
            this.dgvValuesIDCol.Name = "dgvValuesIDCol";
            // 
            // dgvValuesWidthCol
            // 
            this.dgvValuesWidthCol.HeaderText = "Bits";
            this.dgvValuesWidthCol.Name = "dgvValuesWidthCol";
            // 
            // dgvValuesValueCol
            // 
            this.dgvValuesValueCol.HeaderText = "Value";
            this.dgvValuesValueCol.Name = "dgvValuesValueCol";
            this.dgvValuesValueCol.ReadOnly = true;
            // 
            // nudHertz
            // 
            this.nudHertz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHertz.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudHertz.Location = new System.Drawing.Point(527, 12);
            this.nudHertz.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudHertz.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHertz.Name = "nudHertz";
            this.nudHertz.Size = new System.Drawing.Size(69, 20);
            this.nudHertz.TabIndex = 2;
            this.nudHertz.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudHertz.ValueChanged += new System.EventHandler(this.nudHertz_ValueChanged);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 1000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lblAttachedValue
            // 
            this.lblAttachedValue.AutoSize = true;
            this.lblAttachedValue.Location = new System.Drawing.Point(71, 14);
            this.lblAttachedValue.Name = "lblAttachedValue";
            this.lblAttachedValue.Size = new System.Drawing.Size(33, 13);
            this.lblAttachedValue.TabIndex = 5;
            this.lblAttachedValue.Text = "None";
            // 
            // llbUpdate
            // 
            this.llbUpdate.Location = new System.Drawing.Point(9, 7);
            this.llbUpdate.Name = "llbUpdate";
            this.llbUpdate.Size = new System.Drawing.Size(616, 26);
            this.llbUpdate.TabIndex = 6;
            this.llbUpdate.TabStop = true;
            this.llbUpdate.Text = "Update available!";
            this.llbUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llbUpdate.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 450);
            this.Controls.Add(this.lblAttachedValue);
            this.Controls.Add(lblAttached);
            this.Controls.Add(lblHz);
            this.Controls.Add(this.nudHertz);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.llbUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "EventPocket";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHertz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFlags;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.NumericUpDown nudHertz;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFlagsIDCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFlagsStateCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvValuesIDCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvValuesWidthCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvValuesValueCol;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblAttachedValue;
        private System.Windows.Forms.LinkLabel llbUpdate;
    }
}

