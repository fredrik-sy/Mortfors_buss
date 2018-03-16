using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mortfors_buss.Lib;

namespace Mortfors_buss.UserControls
{
    public partial class CancelTrip : UserControl
    {
        private int year;
        private int week;
        private int id;

        public CancelTrip()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            btnCancelTrip.Enabled = false;
            ControlUtils.ChangeControl(this, typeof(MainMenu));
        }

        private void BtnCancelTrip_Click(object sender, EventArgs e)
        {
            if (MainForm.DataSource.RegisterCancelledTrip(year, week, id))
            {
                BtnBack_Click(null, null);
            }
            else
            {
                ErrorMessage.Show("Ett problem uppstod med databasen");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtYear.Text) || string.IsNullOrEmpty(txtWeek.Text)))
            {
                if (int.TryParse(txtYear.Text, out year) && int.TryParse(txtWeek.Text, out week))
                {
                    try
                    {
                        DataSet data = MainForm.DataSource.GetNonCancelledTrip(year, week);
                        DataTable table = data.Tables[0];
                        dgvNonCancelledTrip.DataSource = table;
                        return;
                    }
                    catch
                    {
                    }
                }
            }

            ErrorMessage.Show("Ogiltigt värde");
        }

        private void DgvNonCancelledTrip_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCancelTrip.Enabled = false;

            if (sender is DataGridView dgv)
            {
                if (dgv.CurrentRow.Selected)
                {
                    int selectedRow = dgv.SelectedRows[0].Index;
                    id = (int)dgv.Rows[selectedRow].Cells[0].Value;
                    btnCancelTrip.Enabled = true;
                }
            }
        }
    }
}
