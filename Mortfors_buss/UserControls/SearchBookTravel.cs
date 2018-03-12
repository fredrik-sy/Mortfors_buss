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
    public partial class SearchBookTravel : UserControl
    {
        private DataRow busTrip;
        private EnumerableRowCollection<DataRow> bookingScheduleCollection;
        private EnumerableRowCollection<DataRow> busTripCollection;
        private int weekNumber;

        public SearchBookTravel()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Visible = false;
            ClearData();
            MainForm.UserControls[typeof(MainMenu)].Visible = true;
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
                            bookingScheduleCollection = MainForm.DataSource.RetrieveBookingSchedule(weekNumber)
                                .Tables[0]
                                .AsEnumerable();

                            busTripCollection = MainForm.DataSource.RetrieveBusTrip(weekNumber)
                                .Tables[0]
                                .AsEnumerable();

                            EnumerableRowCollection<DataRow> customerCollection = MainForm.DataSource.RetrieveCustomerEmail()
                                .Tables[0]
                                .AsEnumerable();

                            List<KeyValuePair<string, string>> departureStopList = busTripCollection.Select(r =>
                                    new KeyValuePair<string, string>(
                                        r.Field<string>("departurestop"),
                                        r.Field<string>("departurestop") + ", " +
                                        r.Field<string>("departurecountry") + ", " +
                                        r.Field<string>("departurestreet")))
                                .Distinct()
                                .ToList();

                            List<string> emailList = customerCollection.Select(r => r.Field<string>("email"))
                                .ToList();

                            cmbFrom.Enabled = true;
                            cmbFrom.DataSource = new BindingSource(departureStopList, null);
                            cmbFrom.DisplayMember = "Value";
                            cmbFrom.ValueMember = "Key";
                            cmbTo.Enabled = true;
                            cmbTime.Enabled = true;
                            cmbCustomer.Enabled = true;
                            cmbCustomer.DataSource = emailList;
                            txtNumberOfSeats.Enabled = true;
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

        private void CmbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (busTripCollection == null)
            {
                return;
            }

            List<KeyValuePair<string, string>> arrivalStopList = busTripCollection.Where(r =>
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
            UpdatePrice();
        }

        private void CmbTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (busTripCollection == null)
            {
                return;
            }

            List<string> dayTimeList = busTripCollection.Where(r =>
                    r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key &&
                    r.Field<string>("arrivalstop") == ((KeyValuePair<string, string>)cmbTo.SelectedItem).Key)
                .Select(r =>
                    Enum.GetName(typeof(DayOfVecka), r.Field<int>("dayofweek")) + " " +
                    r.Field<TimeSpan>("departuretime").ToString(@"hh\:mm") + " - " +
                    r.Field<TimeSpan>("arrivaltime").ToString(@"hh\:mm"))
                .ToList();

            cmbTime.DataSource = dayTimeList;
            UpdatePrice();
        }

        private void CmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bookingScheduleCollection == null && busTripCollection == null)
            {
                return;
            }

            busTrip = busTripCollection.Where(r =>
                    r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key &&
                    r.Field<string>("arrivalstop") == ((KeyValuePair<string, string>)cmbTo.SelectedItem).Key)
                .ElementAt(cmbTime.SelectedIndex);

            int bookingCount = bookingScheduleCollection.Where(r =>
                    r.Field<int>("bustrip_id") == busTrip.Field<int>("bustrip_id") &&
                    r.Field<int>("weeknumber") == weekNumber)
                .Select(r => r.Field<int>("numberofseats"))
                .Sum();

            int capacity = busTrip.Field<int>("capacity");

            txtCapacity.Text = (capacity - bookingCount).ToString();
            UpdatePrice();
        }

        private void TxtNumberOfSeats_TextChanged(object sender, EventArgs e)
        {
            btnBook.Enabled = int.TryParse(txtNumberOfSeats.Text, out int numberOfSeats);
            UpdatePrice();
        }

        private void ClearData()
        {
            busTrip = null;
            bookingScheduleCollection = null;
            busTripCollection = null;

            cmbFrom.DataSource = null;
            cmbFrom.Enabled = false;
            cmbFrom.Text = string.Empty;

            cmbTo.DataSource = null;
            cmbTo.Enabled = false;
            cmbTo.Text = string.Empty;

            cmbTime.DataSource = null;
            cmbTime.Enabled = false;
            cmbTime.Text = string.Empty;

            cmbCustomer.DataSource = null;
            cmbCustomer.Enabled = false;
            cmbCustomer.Text = string.Empty;

            txtWeekNumber.Clear();
            txtNumberOfSeats.Clear();
            txtNumberOfSeats.Enabled = false;
            txtCapacity.Clear();
            txtPrice.Clear();
            btnBook.Enabled = false;
        }

        private void ShowErrorBox(string text)
        {
            string caption = string.Empty;
            MessageBox.Show(text, caption, MessageBoxButtons.OK);
        }

        private void UpdatePrice()
        {
            if (busTrip == null)
            {
                return;
            }

            if (int.TryParse(txtNumberOfSeats.Text, out int numberOfSeats))
            {
                int price = busTrip.Field<int>("price");
                txtPrice.Text = (numberOfSeats * price).ToString();
            }
            else
            {
                txtPrice.Text = string.Empty;
            }
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (busTrip == null)
            {
                return;
            }

            int bookingCount = bookingScheduleCollection.Where(r =>
                    r.Field<int>("bustrip_id") == busTrip.Field<int>("bustrip_id") &&
                    r.Field<int>("weeknumber") == weekNumber)
                .Select(r => r.Field<int>("numberofseats"))
                .Sum();

            string customerId = cmbCustomer.Text;
            int numberOfSeats = int.Parse(txtNumberOfSeats.Text);
            int busTripId = busTrip.Field<int>("bustrip_id");
            int capacity = busTrip.Field<int>("capacity");

            if (bookingCount + numberOfSeats <= capacity)
            {
                if (MainForm.DataSource.RegisterBookingSchedule(weekNumber, customerId, busTripId, numberOfSeats))
                {
                    BtnBack_Click(null, null);
                    return;
                }
            }

            ShowErrorBox("Ogiltig nummer");
        }
    }
}
