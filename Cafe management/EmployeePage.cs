using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe_management
{
    public partial class EmployeePage : Form
    {
        private WelcomePage F1 { get; set; }
        public EmployeePage()
        {
            InitializeComponent();
            pnlPlaceOrder.Visible = true;
            pnlProfile.Visible = false;

            userControlStaffProfile1.Hide();
            userControlPlaceOrder1.Show();
            userControlPlaceOrder1.BringToFront();
        }

        public EmployeePage(string userid, WelcomePage f1) : this()
        {
            this.F1 = f1;
            this.lblEmployeeId.Text = WelcomePage.name;
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            pnlPlaceOrder.Visible = true;
            pnlProfile.Visible = false;
            
            userControlStaffProfile1.Hide();
            userControlPlaceOrder1.Show();
            userControlPlaceOrder1.BringToFront();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            pnlPlaceOrder.Visible = false;
            pnlProfile.Visible = true;
            userControlPlaceOrder1.Hide();
            userControlStaffProfile1.Show();
            userControlStaffProfile1.BringToFront();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Logout Complete");
            this.Hide();
            this.F1.Show();
        }
    }
}
