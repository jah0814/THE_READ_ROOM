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
    public partial class addbook: Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public addbook()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            MySqlConnection con = new MySqlConnection(conString);

            try
            {
                con.Open();

                
                if (!Regex.IsMatch(textBox2.Text, @"^[a-zA-Z\s]+$")) // Title
                {
                    MessageBox.Show("Title should contain only letters and spaces.");
                    return;
                }

                if (!Regex.IsMatch(textBox3.Text, @"^[a-zA-Z\s]+$")) // Genre
                {
                    MessageBox.Show("Genre should contain only letters and spaces.");
                    return;
                }

                if (!Regex.IsMatch(textBox6.Text, @"^[a-zA-Z\s]+$")) // Publisher Name
                {
                    MessageBox.Show("Publisher name should contain only letters and spaces.");
                    return;
                }

                if (!Regex.IsMatch(textBox4.Text, @"^\d+$")) // ISBN (Numbers Only)
                {
                    MessageBox.Show("ISBN should contain only numbers.");
                    return;
                }

                if (!Regex.IsMatch(textBox5.Text, @"^\d{4}$")) // Year Published (4-digit year)
                {
                    MessageBox.Show("Year Published should be a 4-digit number.");
                    return;
                }

                if (!Regex.IsMatch(textBox7.Text, @"^\d+$")) // Author ID (Numbers Only)
                {
                    MessageBox.Show("Author ID should contain only numbers.");
                    return;
                }

                string checkAuthorQuery = "SELECT COUNT(*) FROM authors WHERE author_ID = @authorID";
                MySqlCommand checkAuthorCmd = new MySqlCommand(checkAuthorQuery, con);
                checkAuthorCmd.Parameters.AddWithValue("@authorID", textBox7.Text);

                int authorCount = Convert.ToInt32(checkAuthorCmd.ExecuteScalar());

                if (authorCount > 0)
                {
                    string insertQuery = "INSERT INTO Books (book_ID, title, genre, ISBN, year_published, publisher_name, author_ID) " +
                                         "VALUES (@bookID, @title, @genre, @ISBN, @year_published, @publisher_name, @authorID)";

                    MySqlCommand cmd = new MySqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@bookID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@title", textBox2.Text);
                    cmd.Parameters.AddWithValue("@genre", textBox3.Text);
                    cmd.Parameters.AddWithValue("@ISBN", textBox4.Text);
                    cmd.Parameters.AddWithValue("@year_published", textBox5.Text);
                    cmd.Parameters.AddWithValue("@publisher_name", textBox6.Text);
                    cmd.Parameters.AddWithValue("@authorID", textBox7.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully Inserted into books.");
                    }
                    else
                    {
                        MessageBox.Show("No rows affected.");
                    }
                }
                else
                {
                    MessageBox.Show("The specified author_ID does not exist in the authors table.");
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void addbook_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
