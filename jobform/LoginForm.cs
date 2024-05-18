using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jobform
{
    public partial class LoginForm : Form
    {
        MainForm mf;
        public LoginForm()
        {
            InitializeComponent();
            mf = new MainForm();
        }

        private void logBtn_Click(object sender, EventArgs e)
        {
            if(loginTxt.Text == "admin1" && passTxt.Text == "1111")
            {
                mf.Show();
            }
            else
            {
                loginTxt.Text = ""; passTxt.Text = "";
                erLbl.Visible = true;
            }
        }


    }
}
