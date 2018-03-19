using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mortfors_buss.Lib;

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
                else
                {
                    chkLessThan.Checked = false;
                    txtLessThan.Text = string.Empty;
                }
            }

            if (chkEqual.Checked)
            {
                if (int.TryParse(txtEqual.Text, out int result))
                {
                    equal = result;
                }
                else
                {
                    chkEqual.Checked = false;
                    txtEqual.Text = string.Empty;
                }
            }


            if (chkGreaterThan.Checked)
            {
                if (int.TryParse(txtGreaterThan.Text, out int result))
                {
                    greaterThan = result;
                }
                else
                {
                    chkGreaterThan.Checked = false;
                    txtGreaterThan.Text = string.Empty;
                }
            }

            DataSet dataSet = MainForm.DataSource.RetrieveCustomersNumberOfTrip(lessThan, equal, greaterThan);
            dgvCustomer.DataSource = dataSet.Tables[0];
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            ControlUtils.ChangeControl(this, typeof(MainMenu));
        }

        private void DgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                if (dgv.CurrentRow.Selected)
                {
                    string email = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();
                    DataSet dataSet = MainForm.DataSource.RetrieveCustomersTrip(email);
                    dgvTrip.DataSource = dataSet.Tables[0];
                }
            }
        }
    }
}
