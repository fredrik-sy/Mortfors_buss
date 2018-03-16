namespace Mortfors_buss.UserControls
{
    partial class MainMenu
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
            this.btnRegisterCustomer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchBookTravel = new System.Windows.Forms.Button();
            this.btnSearchCustomer = new System.Windows.Forms.Button();
            this.btnSearchBookDriver = new System.Windows.Forms.Button();
            this.btnCancelTravel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegisterCustomer
            // 
            this.btnRegisterCustomer.Location = new System.Drawing.Point(100, 100);
            this.btnRegisterCustomer.Name = "btnRegisterCustomer";
            this.btnRegisterCustomer.Size = new System.Drawing.Size(140, 40);
            this.btnRegisterCustomer.TabIndex = 0;
            this.btnRegisterCustomer.Text = "Registrera kund";
            this.btnRegisterCustomer.UseVisualStyleBackColor = true;
            this.btnRegisterCustomer.Click += new System.EventHandler(this.BtnRegisterCustomer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mörtfors buss";
            // 
            // btnSearchBookTravel
            // 
            this.btnSearchBookTravel.Location = new System.Drawing.Point(100, 170);
            this.btnSearchBookTravel.Name = "btnSearchBookTravel";
            this.btnSearchBookTravel.Size = new System.Drawing.Size(140, 40);
            this.btnSearchBookTravel.TabIndex = 2;
            this.btnSearchBookTravel.Text = "Sök och boka resa";
            this.btnSearchBookTravel.UseVisualStyleBackColor = true;
            this.btnSearchBookTravel.Click += new System.EventHandler(this.BtnSearchBookTrip_Click);
            // 
            // btnSearchCustomer
            // 
            this.btnSearchCustomer.Location = new System.Drawing.Point(100, 240);
            this.btnSearchCustomer.Name = "btnSearchCustomer";
            this.btnSearchCustomer.Size = new System.Drawing.Size(140, 40);
            this.btnSearchCustomer.TabIndex = 3;
            this.btnSearchCustomer.Text = "Sök kundinformation";
            this.btnSearchCustomer.UseVisualStyleBackColor = true;
            this.btnSearchCustomer.Click += new System.EventHandler(this.BtnSearchCustomer_Click);
            // 
            // btnSearchBookDriver
            // 
            this.btnSearchBookDriver.Location = new System.Drawing.Point(100, 310);
            this.btnSearchBookDriver.Name = "btnSearchBookDriver";
            this.btnSearchBookDriver.Size = new System.Drawing.Size(140, 40);
            this.btnSearchBookDriver.TabIndex = 4;
            this.btnSearchBookDriver.Text = "Sök och boka chaufför";
            this.btnSearchBookDriver.UseVisualStyleBackColor = true;
            this.btnSearchBookDriver.Click += new System.EventHandler(this.BtnSearchBookDriver_Click);
            // 
            // btnCancelTravel
            // 
            this.btnCancelTravel.Location = new System.Drawing.Point(270, 100);
            this.btnCancelTravel.Name = "btnCancelTravel";
            this.btnCancelTravel.Size = new System.Drawing.Size(140, 40);
            this.btnCancelTravel.TabIndex = 5;
            this.btnCancelTravel.Text = "Ställ in resa";
            this.btnCancelTravel.UseVisualStyleBackColor = true;
            this.btnCancelTravel.Click += new System.EventHandler(this.BtnCancelTrip_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancelTravel);
            this.Controls.Add(this.btnSearchBookDriver);
            this.Controls.Add(this.btnSearchCustomer);
            this.Controls.Add(this.btnSearchBookTravel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegisterCustomer);
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(500, 400);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRegisterCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchBookTravel;
        private System.Windows.Forms.Button btnSearchCustomer;
        private System.Windows.Forms.Button btnSearchBookDriver;
        private System.Windows.Forms.Button btnCancelTravel;
    }
}
