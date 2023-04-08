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
    public partial class WelcomePage : Form
    {
        private DataAccess Da { get; set; }
        public WelcomePage()
        {
            InitializeComponent();
            this.Da = new DataAccess();
        }
        public static string id;
        public static string name;
        public static string salary;
        public static string gender;
        public static string joiningDate;
        public static string address;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(5, 0, 0, 0);
            
        }

        private void txtLogin_Click(object sender, EventArgs e)
        {

            try
            {
                string sql = "select * from Employee where id = '" + this.txtUserid.Text + "' and Password = '" + this.txtPassword.Text + "'; ";
                string studentsql = "select * from  Users where Id = '" + this.txtUserid.Text + "' and Password = '" + this.txtPassword.Text + "'; ";

                DataSet ds = this.Da.ExecuteQuery(sql);
                DataSet studentds = this.Da.ExecuteQuery(studentsql);

                if (this.txtUserid.Text.ToLower() == "admin" && this.txtPassword.Text == "admin123")
                {
                    //this.Hide();
                    MessageBox.Show("Login valid");
                    this.Hide();
                    AdminHomePage f2 = new AdminHomePage(this.txtUserid.Text, this);
                    f2.Show();
                }



                else if (ds.Tables[0].Rows.Count == 1)
                {

                    id = ds.Tables[0].Rows[0]["Id"].ToString();
                    name = ds.Tables[0].Rows[0]["Name"].ToString();
                    salary = ds.Tables[0].Rows[0]["Salary"].ToString();
                    gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    joiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                    address = ds.Tables[0].Rows[0]["Address"].ToString();






                    /*
                    id = ds.Tables[0]["Id"].ToString();
                    name = ds.Rows[0]["Name"].ToString();
                    salary = ds.Rows[0]["Salary"].ToString();
                    gender = ds.Rows[0]["Gender"].ToString();
                    joiningDate = ds.Rows[0]["Joining Date"].ToString();
                    address = ds.Rows[0]["Address"].ToString();
                    */



                    MessageBox.Show("Login valid");
                    EmployeePage E1 = new EmployeePage(this.txtUserid.Text, this);
                    E1.Show();
                    this.Hide();
                }
                else if (studentds.Tables[0].Rows.Count == 1)
                {
                    MessageBox.Show("Login valid");
                    UserPage U1 = new UserPage(this.txtUserid.Text, this);
                    U1.Show();
                    this.Hide();

                }


                else
                    MessageBox.Show("Login Invalid");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }


            this.txtUserid.Clear();
            this.txtPassword.Clear();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            new SignUpPage().Show();
            this.Hide();
        }

        private void CheckBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(CheckBoxShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
