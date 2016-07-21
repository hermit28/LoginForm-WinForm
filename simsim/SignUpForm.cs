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

                    //db.InsertUserInfo(txtUsername.Text, txtPassword.Text, txtPhoneNumber.Text, dtpBirth.Text, gender);
                    MessageBox.Show("Successfully sign up", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("failed sign up", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(Char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '-'))
            {
                e.Handled = true;
            }
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
            int year = DateTime.Now.Year;

            for (int i = 0; i < 120; i++)
            {
                cboYear.Items.Add(year--);
            }

            for(int i = 1; i <= 12; i++)
            {
                cboMonth.Items.Add(i);
            }

            for (int i = 1; i <= 31; i++)
            {
                cboDay.Items.Add(i);
            }
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectYear = (int)cboYear.SelectedItem;
            int selectMonth = (int)cboMonth.SelectedItem;
            bool leapYear = false;

            if ((selectYear % 4 == 0 && selectYear % 100 != 0) || selectYear % 400 == 0)
            {
                leapYear = true;
            }

            int day = 1;

            switch (selectMonth)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    day = 31;
                    break;

                case 4:
                case 6:
                case 9:
                case 11:
                    day = 30;
                    break;

                case 2:
                    if(leapYear)
                    {
                        day = 29;
                    }
                    else
                    {
                        day = 28;
                    }
                    break;
            }

            cboDay.Items.Clear();

            for(int i = 1; i <= day; i++)
            {
                cboDay.Items.Add(i);
            }
        }
    }
}