using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace THE_READ_ROOM
{

    public partial class Edit_member: Form
    {
        private int member_ID;
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public Edit_member(int id, string first_name, string middle_initial, string last_name, string phone_number)
        {
            InitializeComponent();
            member_ID = id;

            textBox1.Text = first_name;
            textBox2.Text = middle_initial;
            textBox3.Text = last_name;
            textBox4.Text = phone_number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        string first_name = textBox1.Text;
        string middle_initial = textBox2.Text;
        string last_name = textBox3.Text;
        string phone_number = textBox4.Text;

            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            using (MySqlConnection conn = new MySqlConnection(conString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE members SET first_name = @firstName, middle_initial = @middleInitial, last_name = @lastName, phone_number = @phone WHERE member_ID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", member_ID);
                        cmd.Parameters.AddWithValue("@firstName", first_name);
                        cmd.Parameters.AddWithValue("@middleInitial", middle_initial);
                        cmd.Parameters.AddWithValue("@lastName", last_name);
                        cmd.Parameters.AddWithValue("@phone", phone_number);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Member updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
