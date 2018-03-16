namespace Mortfors_buss.UserControls
{
    partial class SearchCustomer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtLessThan = new System.Windows.Forms.TextBox();
            this.chkLessThan = new System.Windows.Forms.CheckBox();
            this.chkEqual = new System.Windows.Forms.CheckBox();
            this.txtEqual = new System.Windows.Forms.TextBox();
            this.txtGreaterThan = new System.Windows.Forms.TextBox();
            this.chkGreaterThan = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvTrip = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrip)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.25F);
            this.label1.Location = new System.Drawing.Point(108, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sök kundinformation";
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.AllowUserToAddRows = false;
            this.dgvCustomer.AllowUserToDeleteRows = false;
            this.dgvCustomer.AllowUserToOrderColumns = true;
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.Location = new System.Drawing.Point(30, 160);
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.ReadOnly = true;
            this.dgvCustomer.Size = new System.Drawing.Size(440, 110);
            this.dgvCustomer.TabIndex = 1;
            this.dgvCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCustomer_CellClick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(380, 121);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Tillbaka";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(380, 80);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 3;
            this.btnFilter.Text = "Filtrera";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);
            // 
            // txtLessThan
            // 
            this.txtLessThan.Location = new System.Drawing.Point(46, 19);
            this.txtLessThan.Name = "txtLessThan";
            this.txtLessThan.Size = new System.Drawing.Size(30, 20);
            this.txtLessThan.TabIndex = 5;
            // 
            // chkLessThan
            // 
            this.chkLessThan.AutoSize = true;
            this.chkLessThan.Location = new System.Drawing.Point(11, 21);
            this.chkLessThan.Name = "chkLessThan";
            this.chkLessThan.Size = new System.Drawing.Size(32, 17);
            this.chkLessThan.TabIndex = 6;
            this.chkLessThan.Text = "<";
            this.chkLessThan.UseVisualStyleBackColor = true;
            // 
            // chkEqual
            // 
            this.chkEqual.AutoSize = true;
            this.chkEqual.Location = new System.Drawing.Point(101, 21);
            this.chkEqual.Name = "chkEqual";
            this.chkEqual.Size = new System.Drawing.Size(32, 17);
            this.chkEqual.TabIndex = 7;
            this.chkEqual.Text = "=";
            this.chkEqual.UseVisualStyleBackColor = true;
            // 
            // txtEqual
            // 
            this.txtEqual.Location = new System.Drawing.Point(136, 19);
            this.txtEqual.Name = "txtEqual";
            this.txtEqual.Size = new System.Drawing.Size(30, 20);
            this.txtEqual.TabIndex = 8;
            // 
            // txtGreaterThan
            // 
            this.txtGreaterThan.Location = new System.Drawing.Point(225, 19);
            this.txtGreaterThan.Name = "txtGreaterThan";
            this.txtGreaterThan.Size = new System.Drawing.Size(30, 20);
            this.txtGreaterThan.TabIndex = 10;
            // 
            // chkGreaterThan
            // 
            this.chkGreaterThan.AutoSize = true;
            this.chkGreaterThan.Location = new System.Drawing.Point(190, 21);
            this.chkGreaterThan.Name = "chkGreaterThan";
            this.chkGreaterThan.Size = new System.Drawing.Size(32, 17);
            this.chkGreaterThan.TabIndex = 9;
            this.chkGreaterThan.Text = ">";
            this.chkGreaterThan.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEqual);
            this.groupBox1.Controls.Add(this.txtGreaterThan);
            this.groupBox1.Controls.Add(this.txtLessThan);
            this.groupBox1.Controls.Add(this.chkGreaterThan);
            this.groupBox1.Controls.Add(this.chkLessThan);
            this.groupBox1.Controls.Add(this.chkEqual);
            this.groupBox1.Location = new System.Drawing.Point(30, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 50);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Antal turer";
            // 
            // dgvTrip
            // 
            this.dgvTrip.AllowUserToAddRows = false;
            this.dgvTrip.AllowUserToDeleteRows = false;
            this.dgvTrip.AllowUserToOrderColumns = true;
            this.dgvTrip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrip.Location = new System.Drawing.Point(30, 276);
            this.dgvTrip.Name = "dgvTrip";
            this.dgvTrip.ReadOnly = true;
            this.dgvTrip.Size = new System.Drawing.Size(440, 105);
            this.dgvTrip.TabIndex = 12;
            // 
            // SearchCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTrip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvCustomer);
            this.Controls.Add(this.label1);
            this.Name = "SearchCustomer";
            this.Size = new System.Drawing.Size(500, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCustomer;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtLessThan;
        private System.Windows.Forms.CheckBox chkLessThan;
        private System.Windows.Forms.CheckBox chkEqual;
        private System.Windows.Forms.TextBox txtEqual;
        private System.Windows.Forms.TextBox txtGreaterThan;
        private System.Windows.Forms.CheckBox chkGreaterThan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvTrip;
    }
}
