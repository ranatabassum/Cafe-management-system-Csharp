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
    public partial class UserControlFoodInfo : UserControl
    {
        private DataAccess Da { get; set; }
        public UserControlFoodInfo()
        {
            InitializeComponent();
            this.Da = new DataAccess();
            string sql = "select * from FoodItems;";
            this.PopulateGridView(sql);
        }

        private void PopulateGridView(string sql = "select * from FoodItems;")
        {
            DataSet ds = this.Da.ExecuteQuery(sql);

            this.dgvFoodInfo.AutoGenerateColumns = false;
            this.dgvFoodInfo.DataSource = ds.Tables[0];
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            
            this.PopulateGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select * from FoodItems WHERE Name = '"+this.txtSearch.Text.ToLower()+"';";
            this.PopulateGridView(sql);
        }

        private void txtSave_Click(object sender, EventArgs e)
        {

            if (!this.IsValidToSave())
            {
                MessageBox.Show("Missing or Empty Box");
                return;
            }

            try
            {

                var txt = "select * from FoodItems where Number = '" + this.txtNumber.Text + "';";
                var dt = this.Da.ExecuteQueryTable(txt);
                if (dt.Rows.Count == 1)
                {
                    //update
                    var sql = @"update FoodItems
                               set Name = '"+this.txtName.Text+@"',
                               Category = '"+this.cmbCategory.Text+@"',
                               Price = "+this.txtPrice.Text+@"
                               where Number = "+this.txtNumber.Text+"; ";

                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data Update complete");
                    else
                        MessageBox.Show("Data Update failed");
                }
                else
                {
                    string sql = "insert into FoodItems values(" + this.txtNumber.Text + ", '" + this.txtName.Text + "', '" + this.cmbCategory.Text + "', " + this.txtPrice.Text + "); ";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data insertion complete");
                    else
                        MessageBox.Show("Data insertion failed");
                }

                this.PopulateGridView();
                this.ClearContent();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private bool IsValidToSave()
        {
            if (String.IsNullOrEmpty(this.txtNumber.Text) || String.IsNullOrWhiteSpace(this.txtNumber.Text) ||
                String.IsNullOrEmpty(this.txtName.Text) || String.IsNullOrWhiteSpace(this.txtName.Text) ||
                String.IsNullOrEmpty(this.cmbCategory.Text) || String.IsNullOrWhiteSpace(this.cmbCategory.Text) ||
                String.IsNullOrEmpty(this.txtPrice.Text) || String.IsNullOrWhiteSpace(this.txtPrice.Text)
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
            this.txtNumber.Clear();
            this.cmbCategory.SelectedIndex = -1;
            this.txtPrice.Clear();
            this.txtSearch.Clear();
            this.dgvFoodInfo.ClearSelection();
        }

        private void txtDelete_Click(object sender, EventArgs e)
        {

            if (this.dgvFoodInfo.SelectedRows.Count < 0)
            {
                MessageBox.Show("Please select a Row first");
                return;
            }
            var Number = this.dgvFoodInfo.CurrentRow.Cells[0].Value.ToString();
            //var title = this.dgvEmployeeInfo.CurrentRow.Cells[1].Value.ToString();

            try
            {
                var sql = "delete from FoodItems where Number = '" + Number + "';";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                    MessageBox.Show(Number + " Remove Complete");
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

        private void dgvFoodInfo_DoubleClick(object sender, EventArgs e)
        {
            this.txtNumber.Text = this.dgvFoodInfo.CurrentRow.Cells[0].Value.ToString();
            this.txtName.Text = this.dgvFoodInfo.CurrentRow.Cells["Column1"].Value.ToString();
            this.cmbCategory.Text = this.dgvFoodInfo.CurrentRow.Cells["Category"].Value.ToString();
            this.txtPrice.Text = this.dgvFoodInfo.CurrentRow.Cells["Price"].Value.ToString();
            
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }



            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * from FoodItems where Name like '"+this.txtSearch.Text.ToLower()+"%';";
            this.PopulateGridView(sql);
        }
    }
}
