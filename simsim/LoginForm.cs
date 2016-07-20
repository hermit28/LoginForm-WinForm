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
    public partial class LoginForm : Form
    {
        internal DB db;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            db = new DB();

            db.DBConnection();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please enter your username or password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(db.SearchUserInfo(txtUsername.Text, txtPassword.Text))
            {
                MessageBox.Show("Successfully login", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect your username or password.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUpForm signupForm = new SignUpForm();

            signupForm.Show();
        }
    }
}
