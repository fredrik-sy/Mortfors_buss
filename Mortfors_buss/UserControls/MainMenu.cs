using System;
using System.Windows.Forms;

namespace Mortfors_buss.UserControls
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void BtnCustomerRegistration_Click(object sender, EventArgs e)
        {
            Visible = false;
            MainForm.UserControls[typeof(CustomerRegistration)].Visible = true;
        }

        private void BtnSearchBookTravel_Click(object sender, EventArgs e)
        {
            Visible = false;
            MainForm.UserControls[typeof(SearchBookTravel)].Visible = true;
        }

        private void BtnSearchCustomer_Click(object sender, EventArgs e)
        {
            Visible = false;
            MainForm.UserControls[typeof(SearchCustomer)].Visible = true;
        }

        private void BtnSearchBookDriver_Click(object sender, EventArgs e)
        {
            Visible = false;
            MainForm.UserControls[typeof(SearchBookDriver)].Visible = true;
        }
    }
}
