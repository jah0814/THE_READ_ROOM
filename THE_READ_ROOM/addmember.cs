using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THE_READ_ROOM
{
    public partial class addmember : Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public addmember()
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

               
                if (!Regex.IsMatch(textBox2.Text, @"^[a-zA-Z\s]+$") ||
                    !Regex.IsMatch(textBox3.Text, @"^[a-zA-Z]?$") || 
                    !Regex.IsMatch(textBox4.Text, @"^[a-zA-Z\s]+$"))
                {
                    MessageBox.Show("Names should only contain letters.");
                    return;
                }

                if (!Regex.IsMatch(textBox5.Text, @"^\d+$"))
                {
                    MessageBox.Show("Phone number should contain only digits.");
                    return;
                }

                string insertQuery = "INSERT INTO Members (member_ID, first_name, middle_initial, last_name, phone_number) " +
                                     "VALUES (@memberID, @firstName, @middleInitial, @lastName, @phoneNumber)";

                MySqlCommand cmd = new MySqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@memberID", textBox1.Text);
                cmd.Parameters.AddWithValue("@firstName", textBox2.Text);
                cmd.Parameters.AddWithValue("@middleInitial", textBox3.Text);
                cmd.Parameters.AddWithValue("@lastName", textBox4.Text);
                cmd.Parameters.AddWithValue("@phoneNumber", textBox5.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Successfully Inserted into members");
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

        private void addmember_Load(object sender, EventArgs e)
        {

        }
    }
}
