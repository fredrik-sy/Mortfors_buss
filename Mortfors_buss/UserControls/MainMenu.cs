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

        private void BtnRegisterCustomer_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(RegisterCustomer));
        }

        private void BtnSearchBookTrip_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(SearchBookTrip));
        }

        private void BtnSearchCustomer_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(SearchCustomer));
        }

        private void BtnSearchBookDriver_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(SearchBookDriver));
        }

        private void BtnCancelTrip_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(CancelTrip));
        }
    }
}
