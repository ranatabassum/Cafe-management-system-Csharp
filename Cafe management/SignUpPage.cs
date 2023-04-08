using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cafe_management
{
    public partial class SignUpPage : Form
    {
        private DataAccess Da { get; set; }
        public SignUpPage()
        {
            InitializeComponent();
            this.Da = new DataAccess();
        }

        private void SignUpPage_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(5, 0, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {

            try
            {
                string sql = "insert into Users  values('" + this.txtId.Text + "', '" + this.txtName.Text + "', '" + this.txtEmail.Text + "', '" + this.txtPassword.Text + "'); ";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1 && this.txtPassword.Text == this.txtConfirmPassword.Text)
                    MessageBox.Show("Registration Complete");
                else
                    MessageBox.Show("Registration Failed");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
            this.ClearContent();
        }
        private void ClearContent()
        {
            this.txtId.Clear();
            this.txtName.Clear();
            this.txtPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new WelcomePage().Show();
            this.Hide();
        }
    }
}
