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
    public partial class StaffPage : Form
    {
        public StaffPage()
        {
            InitializeComponent();
        }

        private void StaffPage_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(100, 0, 0, 0);
            panel4.BackColor = Color.FromArgb(120, 10, 50, 50);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            panelPlace.Visible = true;
            panelProfile.Visible = false;
            userControlStaffPlaceOrder1.Show();
            userControlStaffProfile1.Hide();
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            panelPlace.Visible = false;
            panelProfile.Visible = true;
            userControlStaffProfile1.Show();
            userControlStaffPlaceOrder1.Hide();
        }
    }
}
