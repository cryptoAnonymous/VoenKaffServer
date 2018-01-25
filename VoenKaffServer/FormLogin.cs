using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoenKaffServer
{
    public partial class FormLogin : Form
    {
        FormStart _formStart;
        public FormLogin()
        {
            InitializeComponent();
            _formStart = new FormStart(this);
        }

        private void buttonGoAdmin_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль");
                return;
            }
            else
            {
                if (textBoxLogin.Text == "admin" && textBoxPassword.Text == "admin")
                {
                    //MessageBox.Show("Login Successful!");
                    this.Hide();
                    _formStart.Show();
                    textBoxLogin.Text = "";
                    textBoxPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("Имя пользователя или пароль введены неверно. \nПовторите ввод снова");
                }
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
