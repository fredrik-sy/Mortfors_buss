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
    public partial class SearchBookTrip : UserControl
    {
        private DataRow trip;
        private EnumerableRowCollection<DataRow> bookingCollection;
        private EnumerableRowCollection<DataRow> tripCollection;
        private int numberOfSeatsBooked;
        private int year;
        private int week;

        public SearchBookTrip()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            ClearControls();
            ControlUtils.ChangeControl(this, typeof(MainMenu));
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
                            bookingCollection = MainForm.DataSource.RetrieveBooking(year, week)
                                .Tables[0]
                                .AsEnumerable();

                            tripCollection = MainForm.DataSource.RetrieveTrip(year, week)
                                .Tables[0]
                                .AsEnumerable();

                            List<string> emailList = MainForm.DataSource.RetrieveCustomerEmail()
                                .Tables[0]
                                .AsEnumerable()
                                .Select(r => r.Field<string>("email"))
                                .ToList();

                            List<KeyValuePair<string, string>> departureList = tripCollection.Select(r => new KeyValuePair<string, string>(
                                        r.Field<string>("departurestop"),
                                        r.Field<string>("departurestop") + ", " +
                                        r.Field<string>("departurecountry") + ", " +
                                        r.Field<string>("departurestreet")))
                                .Distinct()
                                .ToList();

                            cmbFrom.Enabled = true;
                            cmbFrom.DataSource = new BindingSource(departureList, null);
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

            ErrorMessage.Show("Ogiltig år eller vecka");
            ClearControls();
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
                UpdatePrice();
            }
        }

        private void CmbTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tripCollection != null)
            {
                List<string> timeList = tripCollection.Where(r =>
                        r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key &&
                        r.Field<string>("arrivalstop") == ((KeyValuePair<string, string>)cmbTo.SelectedItem).Key)
                    .Select(r =>
                        Enum.GetName(typeof(DayOfWeek2), r.Field<int>("dayofweek")) + " " +
                        r.Field<TimeSpan>("departuretime").ToString(@"hh\:mm") + " - " +
                        r.Field<TimeSpan>("arrivaltime").ToString(@"hh\:mm"))
                    .ToList();

                cmbTime.DataSource = timeList;
                UpdatePrice();
            }
        }

        private void CmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bookingCollection != null && tripCollection != null)
            {
                trip = tripCollection.Where(r =>
                        r.Field<string>("departurestop") == ((KeyValuePair<string, string>)cmbFrom.SelectedItem).Key &&
                        r.Field<string>("arrivalstop") == ((KeyValuePair<string, string>)cmbTo.SelectedItem).Key)
                    .ElementAt(cmbTime.SelectedIndex);

                numberOfSeatsBooked = bookingCollection.Where(r =>
                        r.Field<int>("trip_id") == trip.Field<int>("id") &&
                        r.Field<int>("week") == week)
                    .Select(r => r.Field<int>("numberofseats"))
                    .Sum();

                int capacity = trip.Field<int>("capacity");

                txtCapacity.Text = Convert.ToString(capacity - numberOfSeatsBooked);
                UpdatePrice();
            }
        }

        private void TxtNumberOfSeats_TextChanged(object sender, EventArgs e)
        {
            btnBook.Enabled = int.TryParse(txtNumberOfSeats.Text, out int numberOfSeats);
            UpdatePrice();
        }

        private void BtnBook_Click(object sender, EventArgs e)
        {
            if (trip != null)
            {
                string customerId = cmbCustomer.Text;
                int numberOfSeats = int.Parse(txtNumberOfSeats.Text);
                int tripId = trip.Field<int>("id");
                int capacity = trip.Field<int>("capacity");

                if (numberOfSeatsBooked + numberOfSeats <= capacity)
                {
                    if (MainForm.DataSource.RegisterBookingSchedule(year, week, customerId, tripId, numberOfSeats))
                    {
                        BtnBack_Click(null, null);
                        return;
                    }
                }

                ErrorMessage.Show("Ogiltig antal");
            }
        }

        private void ClearControls()
        {
            bookingCollection = null;
            trip = null;
            tripCollection = null;

            btnBook.Enabled = false;
            cmbFrom.Enabled = false;
            cmbTo.Enabled = false;
            cmbTime.Enabled = false;
            cmbCustomer.Enabled = false;
            txtNumberOfSeats.Enabled = false;

            ControlUtils.ClearControls(Controls);
        }

        private void UpdatePrice()
        {
            if (trip != null)
            {
                if (int.TryParse(txtNumberOfSeats.Text, out int numberOfSeats))
                {
                    int price = trip.Field<int>("price");
                    txtPrice.Text = Convert.ToString(price * numberOfSeats);
                }
                else
                {
                    txtPrice.Text = string.Empty;
                }
            }
        }
    }
}
