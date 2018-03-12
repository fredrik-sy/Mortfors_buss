using System;
using System.Windows.Forms;

namespace Mortfors_buss.UserControls
{
    public partial class CustomerRegistration : UserControl
    {
        public CustomerRegistration()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAddress.Text))
            {
                ShowErrorBox("Fyll i e-post, namn och adress");
                return;
            }
            
            if (MainForm.DataSource.RegisterCustomer(txtEmail.Text, txtName.Text, txtAddress.Text, txtPhone.Text))
            {
                ChangeUserControl(typeof(MainMenu));
            }
            else
            {
                ShowErrorBox("Ogiltig värde");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            ChangeUserControl(typeof(MainMenu));
        }

        private void ChangeUserControl(Type type)
        {
            Visible = false;
            MainForm.UserControls[type].Visible = true;
        }
        
        private void ShowErrorBox(string text)
        {
            string caption = string.Empty;
            MessageBox.Show(text, caption, MessageBoxButtons.OK);
        }
    }
}
