using System;
using System.Windows.Forms;
using Mortfors_buss.Lib;

namespace Mortfors_buss.UserControls
{
    public partial class RegisterCustomer : UserControl
    {
        public RegisterCustomer()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAddress.Text))
            {
                ErrorMessage.Show("Fyll i e-post, namn och adress");
                return;
            }

            try
            {
                if (MainForm.DataSource.RegisterCustomer(txtEmail.Text, txtName.Text, txtAddress.Text, txtPhone.Text))
                {
                    BtnBack_Click(null, null);
                    return;
                }
            }
            catch
            {
            }

            ErrorMessage.Show("Ogiltig värde");
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(MainMenu));
        }
    }
}
