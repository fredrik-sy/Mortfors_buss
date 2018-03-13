namespace Mortfors_buss.UserControls
{
    partial class CancelTrip
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnCancelTrip = new System.Windows.Forms.Button();
            this.txtWeek = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvNonCancelledTrip = new System.Windows.Forms.DataGridView();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNonCancelledTrip)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(210, 350);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 50;
            this.btnSearch.Text = "Sök";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(90, 350);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 49;
            this.btnBack.Text = "Tillbaka";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // btnCancelTrip
            // 
            this.btnCancelTrip.Enabled = false;
            this.btnCancelTrip.Location = new System.Drawing.Point(330, 350);
            this.btnCancelTrip.Name = "btnCancelTrip";
            this.btnCancelTrip.Size = new System.Drawing.Size(75, 23);
            this.btnCancelTrip.TabIndex = 48;
            this.btnCancelTrip.Text = "Ställ in";
            this.btnCancelTrip.UseVisualStyleBackColor = true;
            this.btnCancelTrip.Click += new System.EventHandler(this.BtnCancelTrip_Click);
            // 
            // txtWeek
            // 
            this.txtWeek.Location = new System.Drawing.Point(150, 87);
            this.txtWeek.Name = "txtWeek";
            this.txtWeek.Size = new System.Drawing.Size(30, 20);
            this.txtWeek.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Vecka";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(160, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 36);
            this.label1.TabIndex = 39;
            this.label1.Text = "Ställ in resa";
            // 
            // dgvNonCancelledTrip
            // 
            this.dgvNonCancelledTrip.AllowUserToAddRows = false;
            this.dgvNonCancelledTrip.AllowUserToDeleteRows = false;
            this.dgvNonCancelledTrip.AllowUserToOrderColumns = true;
            this.dgvNonCancelledTrip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNonCancelledTrip.Location = new System.Drawing.Point(30, 125);
            this.dgvNonCancelledTrip.Name = "dgvNonCancelledTrip";
            this.dgvNonCancelledTrip.ReadOnly = true;
            this.dgvNonCancelledTrip.Size = new System.Drawing.Size(440, 200);
            this.dgvNonCancelledTrip.TabIndex = 51;
            this.dgvNonCancelledTrip.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvNonCancelledTrip_CellClick);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(50, 87);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(40, 20);
            this.txtYear.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "År";
            // 
            // CancelTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvNonCancelledTrip);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCancelTrip);
            this.Controls.Add(this.txtWeek);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CancelTrip";
            this.Size = new System.Drawing.Size(500, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNonCancelledTrip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnCancelTrip;
        private System.Windows.Forms.TextBox txtWeek;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvNonCancelledTrip;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label label3;
    }
}
