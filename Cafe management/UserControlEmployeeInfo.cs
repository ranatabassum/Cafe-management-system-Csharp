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
    public partial class UserControlEmployeeInfo : UserControl
    {
        private DataAccess Da { get; set; }
        public UserControlEmployeeInfo()
        {
            InitializeComponent();
            this.Da = new DataAccess();
            string sql = "select * from Employee;";
            this.PopulateGridView(sql);
            this.AutoIdGenerator();
        }

        private void PopulateGridView(string sql = "select * from Employee;")
        {
            DataSet ds = this.Da.ExecuteQuery(sql);
            this.dgvEmployeeInfo.AutoGenerateColumns = false;
            this.dgvEmployeeInfo.DataSource = ds.Tables[0];
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            
            this.PopulateGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select * from Employee WHERE Name = '"+this.txtSearch.Text.ToLower()+"';";
            this.PopulateGridView(sql);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsValidToSave())
            {
                MessageBox.Show("Missing or Empty Box");
                return;
            }


            try
            {
                var txt = "select * from Employee where Id = '" + this.txtId.Text + "';";
                var dt = this.Da.ExecuteQueryTable(txt);
                if (dt.Rows.Count == 1)
                {
                    var sql = @"update Employee
                               set Name = '"+this.txtName.Text+@"',
                               Password = '"+this.txtPasswprd.Text+@"',
                               Salary = "+this.txtSalary.Text+@",
                               Gender = '"+this.cmbGender.Text+@"',
                               JoiningDate = '"+this.dtpJoiningDate.Text+@"',
                               Address = '"+this.txtAddress.Text+@"'
                               where Id = '"+this.txtId.Text+"'; ";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data Update complete");
                    else
                        MessageBox.Show("Data Update failed");

                }
                else
                {
                    //insert
                    string sql = "insert into Employee values('" + this.txtId.Text + "', '" + this.txtName.Text + "', '" + this.txtPasswprd.Text + "', " + this.txtSalary.Text + ", '" + this.cmbGender.Text + "', '" + this.dtpJoiningDate.Text + "', '" + this.txtAddress.Text + "'); ";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data insertion complete");
                    else
                        MessageBox.Show("Data insertion failed");
                }

                this.PopulateGridView();
                this.ClearContent();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }

        }

        private bool IsValidToSave()
        {
            if (String.IsNullOrEmpty(this.txtId.Text) || String.IsNullOrWhiteSpace(this.txtId.Text) ||
                String.IsNullOrEmpty(this.txtName.Text) || String.IsNullOrWhiteSpace(this.txtName.Text) ||
                String.IsNullOrEmpty(this.txtPasswprd.Text) || String.IsNullOrWhiteSpace(this.txtPasswprd.Text) ||
                String.IsNullOrEmpty(this.txtSalary.Text) || String.IsNullOrWhiteSpace(this.txtSalary.Text) ||
                String.IsNullOrEmpty(this.cmbGender.Text) || String.IsNullOrWhiteSpace(this.cmbGender.Text) ||
                String.IsNullOrEmpty(this.dtpJoiningDate.Text) || String.IsNullOrWhiteSpace(this.dtpJoiningDate.Text) ||
                String.IsNullOrEmpty(this.txtAddress.Text) || String.IsNullOrWhiteSpace(this.txtAddress.Text) 
                )

            {
                return false;
            }

            return true;
        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            this.ClearContent();
        }

        private void ClearContent()
        {
            this.txtName.Clear();
            this.txtId.Clear();
            this.txtPasswprd.Clear();
            this.txtSalary.Clear();
            this.cmbGender.SelectedIndex = -1;
            this.dtpJoiningDate.Text = "";
            this.txtAddress.Clear();
            this.txtSearch.Clear();
            this.dgvEmployeeInfo.ClearSelection();
            this.AutoIdGenerator();
        }

        private void txtDelete_Click(object sender, EventArgs e)
        {

            if (this.dgvEmployeeInfo.SelectedRows.Count < 0)
            {
                MessageBox.Show("Please select a Row first");
                return;
            }

            var id = this.dgvEmployeeInfo.CurrentRow.Cells[0].Value.ToString();
            //var title = this.dgvEmployeeInfo.CurrentRow.Cells[1].Value.ToString();

            try
            {
                var sql = "delete from Employee where id = '" + id + "';";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                    MessageBox.Show(id + " Remove Complete");
                else
                    MessageBox.Show("Data Remove Failed");

                this.PopulateGridView();
                this.ClearContent();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void dgvEmployeeInfo_DoubleClick(object sender, EventArgs e)
        {
            this.txtId.Text = this.dgvEmployeeInfo.CurrentRow.Cells[0].Value.ToString();
            this.txtName.Text = this.dgvEmployeeInfo.CurrentRow.Cells["Colum1"].Value.ToString();
            this.txtPasswprd.Text = this.dgvEmployeeInfo.CurrentRow.Cells["Password"].Value.ToString();
            this.txtSalary.Text = this.dgvEmployeeInfo.CurrentRow.Cells["Salary"].Value.ToString();
            this.cmbGender.Text = this.dgvEmployeeInfo.CurrentRow.Cells["Gender"].Value.ToString();
            this.dtpJoiningDate.Text = this.dgvEmployeeInfo.CurrentRow.Cells["JoiningDate"].Value.ToString();
            this.txtAddress.Text = this.dgvEmployeeInfo.CurrentRow.Cells["Address"].Value.ToString();

            this.txtId.ReadOnly = true;
        }

        private void AutoIdGenerator()
        {
            var sql = "select Id from Employee order by Id desc;";
            var dt = this.Da.ExecuteQueryTable(sql);
            string lastId = dt.Rows[0]["Id"].ToString();
            string[] data = lastId.Split('-');
            int temp = Convert.ToInt32(data[1]);
            string newId = "e-" + (++temp).ToString("d3");
            this.txtId.Text = newId;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * from Employee where Name like '" + this.txtSearch.Text.ToLower() + "%';";
            this.PopulateGridView(sql);
        }
    }
}
