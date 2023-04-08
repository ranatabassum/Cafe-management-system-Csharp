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
    public partial class AdminHomePage : Form
    {
        private WelcomePage F1 { get; set; }
        public AdminHomePage()
        {
            InitializeComponent();
            pnlPlaceOrderA.Visible = true;
            pnlEmployeeInfo.Visible = false;
            pnlFoodItems.Visible = false;
            //pnlOrderDetails.Visible = false;
            userControlFoodInfo1.Hide();
            userControlOrderInfo1.Hide();
            userControlEmployeeInfo1.Hide();
            userControlPlaceOrder1.Show();
            userControlPlaceOrder1.BringToFront();
        }

        public AdminHomePage(string userid, WelcomePage f1) : this()
        {
            this.F1 = f1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            pnlPlaceOrderA.Visible = true;
            pnlEmployeeInfo.Visible = false;
            pnlFoodItems.Visible = false;
           // pnlOrderDetails.Visible = false;
            userControlFoodInfo1.Hide();
            userControlOrderInfo1.Hide();
            userControlEmployeeInfo1.Hide();
            userControlPlaceOrder1.Show();
            userControlPlaceOrder1.BringToFront();
        }

        private void btnFoodInfo_Click(object sender, EventArgs e)
        {
            pnlPlaceOrderA.Visible = false;
            pnlEmployeeInfo.Visible = false;
            pnlFoodItems.Visible = true;
            //pnlOrderDetails.Visible = false;
            userControlOrderInfo1.Hide();
            userControlEmployeeInfo1.Hide();
            userControlPlaceOrder1.Hide();
            userControlFoodInfo1.Show();
            userControlFoodInfo1.BringToFront();
        }

        private void btnEmployeeInfo_Click(object sender, EventArgs e)
        {
            pnlPlaceOrderA.Visible = false;
            pnlEmployeeInfo.Visible = true;
            pnlFoodItems.Visible = false;
            //pnlOrderDetails.Visible = false;
            userControlOrderInfo1.Hide();
            userControlPlaceOrder1.Hide();
            userControlFoodInfo1.Hide();
            userControlEmployeeInfo1.Show();
            userControlEmployeeInfo1.BringToFront();
        }

        private void btnOrderInfo_Click(object sender, EventArgs e)
        {

         
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //this.Hide();
            MessageBox.Show("Logout Complete");
            this.Hide();
            this.F1.Show();
        }
    }
}
