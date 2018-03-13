using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mortfors_buss.Lib;

namespace Mortfors_buss.UserControls
{
    public partial class SearchBookDriver : UserControl
    {
        private EnumerableRowCollection<DataRow> drivingScheduleCollection;
        private EnumerableRowCollection<DataRow> driverScheduleCollection;
        private EnumerableRowCollection<DataRow> driverCollection;
        private int weekNumber;

        public SearchBookDriver()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeekNumber.Text))
            {
                if (int.TryParse(txtWeekNumber.Text, out weekNumber))
                {
                    if (weekNumber > 0 && weekNumber < 54)
                    {
                        try
                        {
                            drivingScheduleCollection = MainForm.DataSource.RetrieveDrivingSchedule(weekNumber)
                                .Tables[0]
                                .AsEnumerable();

                            driverScheduleCollection = MainForm.DataSource.RetrieveDriverSchedule(weekNumber)
                                .Tables[0]
                                .AsEnumerable();

                            driverCollection = MainForm.DataSource.RetrieveDriver()
                                .Tables[0]
                                .AsEnumerable();

                            List<KeyValuePair<string, string>> departureStopList = drivingScheduleCollection.Select(r =>
                                    new KeyValuePair<string, string>(
                                        r.Field<string>("departurestop"),
                                        r.Field<string>("departurestop") + ", " +
                                        r.Field<string>("departurecountry") + ", " +
                                        r.Field<string>("departurestreet")))
                                .Distinct()
                                .ToList();
                            
                            btnBook.Enabled = true;
                            cmbFrom.Enabled = true;
                            cmbFrom.DataSource = new BindingSource(departureStopList, null);
                            cmbFrom.DisplayMember = "Value";
                            cmbFrom.ValueMember = "Key";
                            cmbTo.Enabled = true;
                            cmbTime.Enabled = true;
                            return;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            ShowErrorBox("Ogiltig veckonummer");
            ClearData();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Visible = false;
            ClearData();
            MainForm.UserControls[typeof(MainMenu)].Visible = true;
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (cmbTime.SelectedItem == null || cmbDriver.SelectedItem == null)
            {
                return;
            }

            KeyValuePair<DataRow, string> selectedTime = (KeyValuePair<DataRow, string>)cmbTime.SelectedItem;
            KeyValuePair<string, string> selectedDriver = (KeyValuePair<string, string>)cmbDriver.SelectedItem;

            if (MainForm.DataSource.RegisterDrivingSchedule(2018, weekNumber, selectedDriver.Key,
                selectedTime.Key.Field<int>("bustrip_id")))
            {
                BtnBack_Click(null, null);
                return;
            }

            ShowErrorBox("Fel uppstod vid kommunikation med databasen");
        }

        private void CmbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drivingScheduleCollection == null)
            {
                return;
            }

            List<KeyValuePair<string, string>> arrivalStopList = drivingScheduleCollection.Where(r =>
                    r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key)
                .Select(r => new KeyValuePair<string, string>(
                    r.Field<string>("arrivalstop"),
                    r.Field<string>("arrivalstop") + ", " +
                    r.Field<string>("arrivalcountry") + ", " +
                    r.Field<string>("arrivalstreet")))
                .Distinct()
                .ToList();

            cmbTo.DataSource = new BindingSource(arrivalStopList, null);
            cmbTo.DisplayMember = "Value";
            cmbTo.ValueMember = "Key";
        }

        private void CmbTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drivingScheduleCollection == null)
            {
                return;
            }

            Dictionary<DataRow, string> dayTimeDictionary = drivingScheduleCollection.Where(r =>
                    r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key &&
                    r.Field<string>("arrivalstop") == ((KeyValuePair<string, string>)cmbTo.SelectedItem).Key)
                .ToDictionary(
                    r => r,
                    r => Enum.GetName(typeof(DayOfVecka), r.Field<int>("dayofweek")) + " " +
                         r.Field<TimeSpan>("departuretime").ToString(@"hh\:mm") + " - " +
                         r.Field<TimeSpan>("arrivaltime").ToString(@"hh\:mm"));

            cmbTime.DataSource = new BindingSource(dayTimeDictionary, null);
            cmbTime.DisplayMember = "Value";
            cmbTime.ValueMember = "Key";
        }

        private void CmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drivingScheduleCollection == null)
            {
                return;
            }
            
            if (cmbTime.SelectedItem is KeyValuePair<DataRow, string> schedule)
            {
                TimeSpan scheduleTime1 = schedule.Key.Field<TimeSpan>("departuretime");
                TimeSpan scheduleTime2 = schedule.Key.Field<TimeSpan>("arrivaltime");

                Dictionary<string, string> driverDictionary = driverCollection.Where(r =>
                {
                    return !driverScheduleCollection.Any(n =>
                    {
                        if (r.Field<string>("personalnumber") == n.Field<string>("driver_id"))
                        {
                            if (schedule.Key.Field<int>("dayofweek") == n.Field<int>("dayofweek"))
                            {
                                TimeSpan time1 = n.Field<TimeSpan>("departuretime");
                                TimeSpan time2 = n.Field<TimeSpan>("arrivaltime");

                                if (time1.Ticks > scheduleTime1.Ticks && time1.Ticks < scheduleTime1.Ticks ||
                                    time2.Ticks > scheduleTime2.Ticks && time2.Ticks < scheduleTime2.Ticks)
                                {
                                    return true;
                                }
                            }
                        }

                        return false;
                    });
                }).ToDictionary(
                    r => r.Field<string>("personalnumber"),
                    r => r.Field<string>("personalnumber") + ", " +
                         r.Field<string>("name"));

                if (driverDictionary.Count > 0)
                {
                    cmbDriver.Enabled = true;
                    cmbDriver.DataSource = new BindingSource(driverDictionary, null);
                    cmbDriver.DisplayMember = "Value";
                    cmbDriver.ValueMember = "Key";
                }
            }
        }

        private void ClearData()
        {
            drivingScheduleCollection = null;

            cmbFrom.DataSource = null;
            cmbFrom.Enabled = false;
            cmbFrom.Text = string.Empty;

            cmbTo.DataSource = null;
            cmbTo.Enabled = false;
            cmbTo.Text = string.Empty;

            cmbTime.DataSource = null;
            cmbTime.Enabled = false;
            cmbTime.Text = string.Empty;

            cmbDriver.DataSource = null;
            cmbDriver.Enabled = false;
            cmbDriver.Text = string.Empty;

            txtWeekNumber.Clear();
            btnBook.Enabled = false;
        }

        private void ShowErrorBox(string text)
        {
            string caption = string.Empty;
            MessageBox.Show(text, caption, MessageBoxButtons.OK);
        }
    }
}
