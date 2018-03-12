using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mortfors_buss.Lib;
using Mortfors_buss.UserControls;
using MainMenu = Mortfors_buss.UserControls.MainMenu;

namespace Mortfors_buss
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeUserControls();
            InitializeDataSource();
        }

        public static DataSource DataSource { get; private set; }
        public static Dictionary<Type, UserControl> UserControls { get; private set; }

        private void InitializeDataSource()
        {
            DataSource = new DataSource();
            DataSource.Open();
        }

        private void InitializeUserControls()
        {
            UserControls = new Dictionary<Type, UserControl>
            {
                {typeof(MainMenu), mainMenu},
                {typeof(CustomerRegistration), customerRegistration},
                {typeof(SearchBookTravel), searchBookTravel},
                {typeof(SearchCustomer), searchCustomer},
                {typeof(SearchBookDriver), searchBookDriver}
            };
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSource?.Close();
        }
    }
}
