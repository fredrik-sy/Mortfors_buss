﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mortfors_buss.Lib
{
    internal static class ControlUtils
    {
        public static void ChangeControl(UserControl sender, Type controlType)
        {
            sender.Visible = false;
            MainForm.UserControls[controlType].Visible = true;
            Control.ControlCollection controlCollection = sender.Controls;
            ClearControls(controlCollection);
        }

        public static void ClearControls(Control.ControlCollection collection)
        {
            foreach (Control control in collection)
            {
                switch (control)
                {
                    case CheckBox checkBox:
                        checkBox.Checked = false;
                        break;
                    case ComboBox comboBox:
                        comboBox.DataSource = null;
                        comboBox.Items.Clear();
                        comboBox.ResetText();
                        break;
                    case GroupBox groupBox:
                        ClearControls(groupBox.Controls);
                        break;
                    case TextBox textBox:
                        textBox.Clear();
                        break;
                    case DataGridView dataGridView:
                        dataGridView.DataSource = null;
                        dataGridView.Rows.Clear();
                        dataGridView.Refresh();
                        break;
                }
            }
        }
    }
}
