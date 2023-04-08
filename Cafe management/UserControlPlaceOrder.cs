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
using DGVPrinterHelper;

namespace Cafe_management
{
   
    public partial class UserControlPlaceOrder : UserControl
    {
        //private DataAccess Da { get; set; }
        function fn = new function();
        string queery;
        
        public UserControlPlaceOrder()
        {
            InitializeComponent();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            string category = cmbCategory.Text;
            queery = "select Name from FoodItems where Category ='" +category + "';";
            this.ShowItemList(queery);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string category = cmbCategory.Text;
            queery = "select Name from FoodItems where Category ='"+category+"' and Name like '"+this.txtSearch.Text+"%';";
            this.ShowItemList(queery);
        }
        private void ShowItemList(string query)
        {
            listBox1.Items.Clear();
            DataSet ds = fn.getData(queery);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantityUpDown.ResetText();
            txtTotal.Clear();

            string text = listBox1.GetItemText(listBox1.SelectedItem);
            txtItemName.Text = text;

            queery = "select Price from FoodItems where Name = '"+text+"';";
            DataSet ds = fn.getData(queery);

            try
            {
                txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            catch { }

        }

        private void txtQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            Int64 quan = Int64.Parse(txtQuantityUpDown.Value.ToString());
            Int64 price = Int64.Parse(txtPrice.Text);
            txtTotal.Text = (quan * price).ToString();
        }



        int amount;
        private void dgvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                amount = int.Parse(dgvOrderList.Rows[e.RowIndex].Cells[3].Value.ToString());

            }
            catch { }
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Do You Want To Remove This Order", "Remove Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                   dgvOrderList.Rows.RemoveAt(this.dgvOrderList.SelectedRows[0].Index);
                }

                else
                {
                    MessageBox.Show("Order Not Removed", "Remove Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                // dgvOrderList.Rows.RemoveAt(this.dgvOrderList.SelectedRows[0].Index);

            }
            catch { }

            total -= amount;
            lblTotalAmout.Text = "TK. "+total;
        }
       

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Bill";
            printer.SubTitle = string.Format("Date and Time: {0}", DateTime.Now);
            //printer.SubTitle = string.Format("Time: {0}", DateTime.Now.tim);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Total Amount : " + lblTotalAmout.Text;
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(dgvOrderList);

            this.txtItemName.Clear();
            this.txtPrice.Clear();
            this.txtTotal.Clear();
            //this.txtQuantityUpDown.
            total = 0;
            dgvOrderList.Rows.Clear();
            lblTotalAmout.Text = "TK. " + total;
        }


        protected int n, total = 0;
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text != "0" && txtTotal.Text != "")
            {
                /* int rowcountlist = dgvOrderList.Rows.Count;
                 if (rowcountlist>0)
                 {
                      for (int i=0;i< rowcountlist; i++)
                     {
                         if (dgvOrderList.Rows[i].Cells[0].Value.ToString() == txtItemName.Text)
                         {

                             int oldquant = int.Parse(dgvOrderList.Rows[i].Cells[2].Value.ToString());
                             int newquant= int.Parse(txtQuantityUpDown.Text.ToString());
                             int nowquant = oldquant + newquant;
                             dgvOrderList.Rows[i].Cells[2].Value = nowquant.ToString();

                             break;
                         }

                         else
                         {
                             n = dgvOrderList.Rows.Add();
                             dgvOrderList.Rows[n].Cells[0].Value = txtItemName.Text;
                             dgvOrderList.Rows[n].Cells[1].Value = txtPrice.Text;
                             dgvOrderList.Rows[n].Cells[2].Value = txtQuantityUpDown.Text;
                             dgvOrderList.Rows[n].Cells[3].Value = txtTotal.Text;

                             total += int.Parse(txtTotal.Text);
                             lblTotalAmout.Text = "TK. " + total;
                         }




                     }
                     //if (dgvOrderList.Rows[n].Cells[0].Value.ToString() != txtItemName.Text)
                     //{

                     //    n = dgvOrderList.Rows.Add();
                     //    dgvOrderList.Rows[n].Cells[0].Value = txtItemName.Text;
                     //    dgvOrderList.Rows[n].Cells[1].Value = txtPrice.Text;
                     //    dgvOrderList.Rows[n].Cells[2].Value = txtQuantityUpDown.Text;
                     //    dgvOrderList.Rows[n].Cells[3].Value = txtTotal.Text;

                     //    total += int.Parse(txtTotal.Text);
                     //    lblTotalAmout.Text = "TK. " + total;


                     //}





                 }
                 if(rowcountlist==0)
                 {
                     n = dgvOrderList.Rows.Add();
                     dgvOrderList.Rows[n].Cells[0].Value = txtItemName.Text;
                     dgvOrderList.Rows[n].Cells[1].Value = txtPrice.Text;
                     dgvOrderList.Rows[n].Cells[2].Value = txtQuantityUpDown.Text;
                     dgvOrderList.Rows[n].Cells[3].Value = txtTotal.Text;

                     total += int.Parse(txtTotal.Text);
                     lblTotalAmout.Text = "TK. " + total;
                 }*/

                n = dgvOrderList.Rows.Add();
                dgvOrderList.Rows[n].Cells[0].Value = txtItemName.Text;
                dgvOrderList.Rows[n].Cells[1].Value = txtPrice.Text;
                dgvOrderList.Rows[n].Cells[2].Value = txtQuantityUpDown.Value;
                dgvOrderList.Rows[n].Cells[3].Value = txtTotal.Text;

                total += int.Parse(txtTotal.Text);
                lblTotalAmout.Text = "TK. " + total;




            }
            else
            {
                MessageBox.Show("Minimum Quantity need to be 1");
            }

        }

    }
}
