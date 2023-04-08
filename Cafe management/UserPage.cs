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
    public partial class UserPage : Form
    {
        private WelcomePage F1 { get; set; }
        public UserPage()
        {
            InitializeComponent();
            this.pnlPlaceOrder.Visible = true;
            userControlPlaceOrder1.Show();
            userControlPlaceOrder1.BringToFront();

        }
        public UserPage(string userid, WelcomePage f1) : this()
        {
            this.F1 = f1;
            this.lblName.Text = userid;
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
