using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mortfors_buss.UserControls
{
    public partial class SearchCustomer : UserControl
    {
        public SearchCustomer()
        {
            InitializeComponent();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            int? lessThan = null;
            int? equal = null;
            int? greaterThan = null;

            if (chkLessThan.Checked)
            {
                if (int.TryParse(txtLessThan.Text, out int result))
                {
                    lessThan = result;
                }
            }

            if (chkEqual.Checked)
            {
                if (int.TryParse(txtEqual.Text, out int result))
                {
                    equal = result;
                }
            }


            if (chkGreaterThan.Checked)
            {
                if (int.TryParse(txtGreaterThan.Text, out int result))
                {
                    greaterThan = result;
                }
            }

            DataSet dataSet = MainForm.DataSource.RetrieveCustomerNumberOfTrip(lessThan, equal, greaterThan);
            dgvCustomer.DataSource = dataSet.Tables[0];
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Visible = false;
            ClearData();
            MainForm.UserControls[typeof(MainMenu)].Visible = true;
        }

        private void DgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                if (dgv.CurrentRow.Selected)
                {
                    string email = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();
                    DataSet dataSet = MainForm.DataSource.RetrieveCustomerBusTrip(email);
                    dgvBusTrip.DataSource = dataSet.Tables[0];
                }
            }
        }

        private void ClearData()
        {
            chkLessThan.Checked = false;
            chkEqual.Checked = false;
            chkGreaterThan.Checked = false;

            txtLessThan.Text = string.Empty;
            txtEqual.Text = string.Empty;
            txtGreaterThan.Text = string.Empty;

            dgvCustomer.DataSource = null;
            dgvCustomer.Rows.Clear();

            dgvBusTrip.DataSource = null;
            dgvBusTrip.Rows.Clear();
        }
    }
}
