namespace Mortfors_buss
{
    partial class MainForm
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
            this.mainMenu = new Mortfors_buss.UserControls.MainMenu();
            this.customerRegistration = new Mortfors_buss.UserControls.CustomerRegistration();
            this.searchBookTravel = new Mortfors_buss.UserControls.SearchBookTravel();
            this.searchCustomer = new Mortfors_buss.UserControls.SearchCustomer();
            this.searchBookDriver = new Mortfors_buss.UserControls.SearchBookDriver();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(500, 400);
            this.mainMenu.TabIndex = 1;
            // 
            // customerRegistration
            // 
            this.customerRegistration.Location = new System.Drawing.Point(0, 0);
            this.customerRegistration.Name = "customerRegistration";
            this.customerRegistration.Size = new System.Drawing.Size(500, 400);
            this.customerRegistration.TabIndex = 0;
            this.customerRegistration.Visible = false;
            // 
            // searchBookTravel
            // 
            this.searchBookTravel.Location = new System.Drawing.Point(0, 0);
            this.searchBookTravel.Name = "searchBookTravel";
            this.searchBookTravel.Size = new System.Drawing.Size(500, 400);
            this.searchBookTravel.TabIndex = 2;
            this.searchBookTravel.Visible = false;
            // 
            // searchCustomer
            // 
            this.searchCustomer.Location = new System.Drawing.Point(0, 0);
            this.searchCustomer.Name = "searchCustomer";
            this.searchCustomer.Size = new System.Drawing.Size(500, 400);
            this.searchCustomer.TabIndex = 3;
            this.searchCustomer.Visible = false;
            // 
            // searchBookDriver
            // 
            this.searchBookDriver.Location = new System.Drawing.Point(0, 0);
            this.searchBookDriver.Name = "searchBookDriver";
            this.searchBookDriver.Size = new System.Drawing.Size(500, 400);
            this.searchBookDriver.TabIndex = 4;
            this.searchBookDriver.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 402);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.customerRegistration);
            this.Controls.Add(this.searchBookTravel);
            this.Controls.Add(this.searchCustomer);
            this.Controls.Add(this.searchBookDriver);
            this.Name = "MainForm";
            this.Text = "Mörtfors buss";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.CustomerRegistration customerRegistration;
        private UserControls.MainMenu mainMenu;
        private UserControls.SearchBookTravel searchBookTravel;
        private UserControls.SearchCustomer searchCustomer;
        private UserControls.SearchBookDriver searchBookDriver;
    }
}

