using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THE_READ_ROOM
{
    public partial class Edit_authors: Form
    {
        private int author_ID;
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public Edit_authors(int id, string first_name, string middle_initial, string last_name)
        {
            InitializeComponent();
            author_ID = id;

            textBox1.Text = first_name;
            textBox2.Text = middle_initial;
            textBox3.Text = last_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string first_name = textBox1.Text;
            string middle_initial = textBox2.Text;
            string last_name = textBox3.Text;

            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            using (MySqlConnection conn = new MySqlConnection(conString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE authors SET first_name = @firstName, middle_initial = @middleInitial, last_name = @lastName WHERE author_ID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", author_ID);
                        cmd.Parameters.AddWithValue("@firstName", first_name);
                        cmd.Parameters.AddWithValue("@middleInitial", middle_initial);
                        cmd.Parameters.AddWithValue("@lastName", last_name);
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
