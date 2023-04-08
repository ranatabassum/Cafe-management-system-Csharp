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
    public partial class UserControlStaffProfile : UserControl
    {
        public UserControlStaffProfile()
        {
            InitializeComponent();
        }

        private void UserControlStaffProfile_Load(object sender, EventArgs e)
        {
            lblId.Text = WelcomePage.id;
            lblName.Text = WelcomePage.name;
            lblSalary.Text = WelcomePage.salary+" Tk";
            lblGender.Text = WelcomePage.gender;
            lblJoiningDate.Text = WelcomePage.joiningDate;
            lblAddress.Text = WelcomePage.address;

                
        }

        private void txtChangePaaword_Click(object sender, EventArgs e)
        {
            new PasswordChange().Show();
        }
    }
}
