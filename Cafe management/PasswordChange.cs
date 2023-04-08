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
    public partial class PasswordChange : Form
    {
        private DataAccess Da { get; set; }
        public PasswordChange()
        {
            InitializeComponent();
            this.Da = new DataAccess();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            //try
            //{ 

            var txt = "select * from Employee where Password ='"+txtOldPassword.Text+"';";
            var dt = this.Da.ExecuteQueryTable(txt);

            if (dt.Rows.Count.ToString() == "1")
            {
                if(txtNewPassword.Text == TxtConfirmPassword.Text)
                {
                    var sql = "update Employee set Password = '" + this.TxtConfirmPassword.Text + "' where Password = '" + this.txtOldPassword.Text + "'; ";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1 && this.txtOldPassword.Text == this.TxtConfirmPassword.Text )
                    {
                        MessageBox.Show("Password Changed complete");
                    }
                        
                    else
                    {
                        MessageBox.Show("Password Changed failed");
                    }
                       
                }
                   
            }
            else
            {
                MessageBox.Show("Incorret Old Password");
            }

            this.ClearContent();
        }
        private void ClearContent()
        {
            this.txtOldPassword.Clear();
            this.txtNewPassword.Clear();
            this.TxtConfirmPassword.Clear();
            
        }

    }
}
