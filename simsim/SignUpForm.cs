using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simsim
{
    public partial class SignUpForm : Form
    {
        private DB db;

        public SignUpForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkMale_Click(object sender, EventArgs e)
        {
            chkFemale.Checked = false;
            chkOther.Checked = false;
        }

        private void chkFemale_Click(object sender, EventArgs e)
        {
            chkMale.Checked = false;
            chkOther.Checked = false;
        }

        private void chkOther_Click(object sender, EventArgs e)
        {
            chkMale.Checked = false;
            chkFemale.Checked = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" || txtPassword.Text == "" || txtPhoneNumber.Text == ""
                || (chkMale.Checked == false && chkFemale.Checked == false && chkOther.Checked == false))
            {
                MessageBox.Show("please fill out this form.", "Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                db = new simsim.DB();

                db.DBConnection();

                if (!db.SearchUserInfo(txtUsername.Text, txtPassword.Text))
                {
                    string gender;

                    if(chkMale.Checked == true)
                    {
                        gender = chkMale.Text;
                    }
                    else if(chkFemale.Checked == true)
                    {
                        gender = chkFemale.Text;
                    }
                    else if(chkOther.Checked == true)
                    {
                        gender = chkOther.Text;
                    }
                    else
                    {
                        gender = "";
                    }

                    MessageBox.Show(gender);
                    db.InsertUserInfo(txtUsername.Text, txtPassword.Text, txtPhoneNumber.Text, dtpBirth.Text, gender);
                    MessageBox.Show("Successfully sign up", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("failed sign up", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
