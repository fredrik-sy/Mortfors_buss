using System;
using System.Windows.Forms;
using Mortfors_buss.Lib;

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
            ControlUtil.ChangeControl(this, typeof(CustomerRegistration));
        }

        private void BtnSearchBookTravel_Click(object sender, EventArgs e)
        {
            ControlUtil.ChangeControl(this, typeof(SearchBookTravel));
        }

        private void BtnSearchCustomer_Click(object sender, EventArgs e)
        {
            ControlUtil.ChangeControl(this, typeof(SearchCustomer));
        }

        private void BtnSearchBookDriver_Click(object sender, EventArgs e)
        {
            ControlUtil.ChangeControl(this, typeof(SearchBookDriver));
        }

        private void BtnCancelTravel_Click(object sender, EventArgs e)
        {
            ControlUtil.ChangeControl(this, typeof(CancelTrip));
        }
    }
}
