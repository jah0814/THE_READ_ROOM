using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace THE_READ_ROOM
{
    public partial class login: Form
    {
        private const string PreferredUsername = "Jamesman";
        private const string PreferredPassword = "walaakomaisipman";

        public static login instance;
        public login()
        {
            InitializeComponent();
            instance = this;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == PreferredUsername && textBox2.Text == PreferredPassword)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                member form = new member();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
                panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }
    }
}
