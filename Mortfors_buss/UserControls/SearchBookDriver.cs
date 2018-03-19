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
        private EnumerableRowCollection<DataRow> tripCollection;
        private EnumerableRowCollection<DataRow> drivingCollection;
        private EnumerableRowCollection<DataRow> driverCollection;
        private int year;
        private int week;

        public SearchBookDriver()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtYear.Text) || string.IsNullOrEmpty(txtWeek.Text)))
            {
                if (int.TryParse(txtYear.Text, out year) && int.TryParse(txtWeek.Text, out week))
                {
                    if (year > 2016 && week > 0 && week < 54)
                    {
                        try
                        {
                            tripCollection = MainForm.DataSource.RetrieveDriversTrip(year, week)
                                .Tables[0]
                                .AsEnumerable();

                            drivingCollection = MainForm.DataSource.RetrieveDriversDriving(year, week)
                                .Tables[0]
                                .AsEnumerable();

                            driverCollection = MainForm.DataSource.RetrieveDriver()
                                .Tables[0]
                                .AsEnumerable();

                            List<KeyValuePair<string, string>> departureList = tripCollection.Select(r =>
                                    new KeyValuePair<string, string>(
                                        r.Field<string>("departurestop"),
                                        r.Field<string>("departurestop") + ", " +
                                        r.Field<string>("departurecountry") + ", " +
                                        r.Field<string>("departurestreet")))
                                .Distinct()
                                .ToList();
                            
                            btnBook.Enabled = true;
                            cmbFrom.Enabled = true;
                            cmbFrom.DataSource = new BindingSource(departureList, null);
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

            ErrorMessage.Show("Ogiltig år eller vecka");
            ClearControls();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            ControlUtils.ChangeControl(this, typeof(MainMenu));
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (cmbTime.SelectedItem == null || cmbDriver.SelectedItem == null)
            {
                return;
            }

            KeyValuePair<DataRow, string> selectedTime = (KeyValuePair<DataRow, string>)cmbTime.SelectedItem;
            KeyValuePair<string, string> selectedDriver = (KeyValuePair<string, string>)cmbDriver.SelectedItem;

            if (MainForm.DataSource.RegisterDriving(year, week, selectedDriver.Key,
                selectedTime.Key.Field<int>("id")))
            {
                BtnBack_Click(null, null);
                return;
            }

            ErrorMessage.Show("Fel uppstod vid kommunikation med databasen");
        }

        private void CmbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tripCollection != null)
            {
                List<KeyValuePair<string, string>> arrivalList = tripCollection.Where(r =>
                        r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key)
                    .Select(r => new KeyValuePair<string, string>(
                        r.Field<string>("arrivalstop"),
                        r.Field<string>("arrivalstop") + ", " +
                        r.Field<string>("arrivalcountry") + ", " +
                        r.Field<string>("arrivalstreet")))
                    .Distinct()
                    .ToList();

                cmbTo.DataSource = new BindingSource(arrivalList, null);
                cmbTo.DisplayMember = "Value";
                cmbTo.ValueMember = "Key";
            }
        }

        private void CmbTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tripCollection != null)
            {
                Dictionary<DataRow, string> timeDictionary = tripCollection.Where(r =>
                        r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key &&
                        r.Field<string>("arrivalstop") == ((KeyValuePair<string, string>)cmbTo.SelectedItem).Key)
                    .ToDictionary(
                        r => r,
                        r => Enum.GetName(typeof(DayOfWeek2), r.Field<int>("dayofweek")) + " " +
                             r.Field<TimeSpan>("departuretime").ToString(@"hh\:mm") + " - " +
                             r.Field<TimeSpan>("arrivaltime").ToString(@"hh\:mm"));

                cmbTime.DataSource = new BindingSource(timeDictionary, null);
                cmbTime.DisplayMember = "Value";
                cmbTime.ValueMember = "Key";
            }
        }

        private void CmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tripCollection != null)
            {
                if (cmbTime.SelectedItem is KeyValuePair<DataRow, string> time)
                {
                    TimeSpan departureTime1 = time.Key.Field<TimeSpan>("departuretime");
                    TimeSpan arrivalTime1 = time.Key.Field<TimeSpan>("arrivaltime");

                    // Filter driver since driver should not be able to register trips 
                    // that has conflict with the drivers drive time.
                    Dictionary<string, string> driverDictionary = driverCollection.Where(r =>
                    {
                        return !drivingCollection.Any(n =>
                        {
                            if (r.Field<string>("personalnumber") == n.Field<string>("driver_id"))
                            {
                                if (time.Key.Field<int>("dayofweek") == n.Field<int>("dayofweek"))
                                {
                                    TimeSpan departureTime2 = n.Field<TimeSpan>("departuretime");
                                    TimeSpan arrivalTime2 = n.Field<TimeSpan>("arrivaltime");

                                    if (departureTime2.Ticks > departureTime1.Ticks && departureTime2.Ticks < departureTime1.Ticks ||
                                        arrivalTime2.Ticks > arrivalTime1.Ticks && arrivalTime2.Ticks < arrivalTime1.Ticks)
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
        }

        private void ClearControls()
        {
            tripCollection = null;

            btnBook.Enabled = false;
            cmbFrom.Enabled = false;
            cmbTo.Enabled = false;
            cmbTime.Enabled = false;
            cmbDriver.Enabled = false;

            ControlUtils.ClearControls(Controls);
        }
    }
}
