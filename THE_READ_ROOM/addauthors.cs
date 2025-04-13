using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace THE_READ_ROOM
{
    public partial class addauthors: Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public addauthors()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            MySqlConnection con = new MySqlConnection(conString);

            try
            {
                con.Open();

                // Input validation
                if (!Regex.IsMatch(textBox2.Text, @"^[a-zA-Z\s]+$")) // First Name
                {
                    MessageBox.Show("First name should contain only letters.");
                    return;
                }

                if (!Regex.IsMatch(textBox3.Text, @"^[a-zA-Z]?$")) // Middle Initial (Optional, Single Letter)
                {
                    MessageBox.Show("Middle initial should contain only one letter or be empty.");
                    return;
                }

                if (!Regex.IsMatch(textBox4.Text, @"^[a-zA-Z\s]+$")) // Last Name
                {
                    MessageBox.Show("Last name should contain only letters.");
                    return;
                }

                if (!Regex.IsMatch(textBox1.Text, @"^\d+$")) // Author ID (Numbers Only)
                {
                    MessageBox.Show("Author ID should contain only numbers.");
                    return;
                }

                string insertQuery = "INSERT INTO Authors (author_ID, first_name, middle_initial, last_name) " +
                                     "VALUES (@authorID, @firstname, @middleinitial, @lastname)";

                MySqlCommand cmd = new MySqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@authorID", textBox1.Text);
                cmd.Parameters.AddWithValue("@firstname", textBox2.Text);
                cmd.Parameters.AddWithValue("@middleinitial", textBox3.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox4.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully Inserted into authors");
                }
                else
                {
                    MessageBox.Show("No rows affected.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred while interacting with the database: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                    this.Hide();
                }
            }

        }
    }
}
