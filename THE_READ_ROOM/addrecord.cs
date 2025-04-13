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
    public partial class addrecord : Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public addrecord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = "server=localhost;uid=root;pwd=jaaaahz023;database=librarymnsystem";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    // Validate Borrowing ID, Member ID, and Book ID as numbers
                    if (!Regex.IsMatch(textBox1.Text, @"^\d+$") ||
                        !Regex.IsMatch(textBox2.Text, @"^\d+$") ||
                        !Regex.IsMatch(textBox3.Text, @"^\d+$"))
                    {
                        MessageBox.Show("Borrowing ID, Member ID, and Book ID must be numbers.");
                        return;
                    }

                    // Validate Dates
                    if (!DateTime.TryParse(textBox4.Text, out DateTime borrowingDate) ||
                        !DateTime.TryParse(textBox5.Text, out DateTime returnDate))
                    {
                        MessageBox.Show("Please enter valid dates.");
                        return;
                    }

                    if (returnDate < borrowingDate)
                    {
                        MessageBox.Show("Return date cannot be before borrowing date.");
                        return;
                    }

                    // Check if Member and Book exist
                    string checkMemberQuery = "SELECT COUNT(*) FROM members WHERE member_ID = @memberID";
                    string checkBookQuery = "SELECT COUNT(*) FROM books WHERE book_ID = @bookID";

                    using (MySqlCommand checkMemberCmd = new MySqlCommand(checkMemberQuery, con))
                    using (MySqlCommand checkBookCmd = new MySqlCommand(checkBookQuery, con))
                    {
                        checkMemberCmd.Parameters.AddWithValue("@memberID", textBox2.Text);
                        checkBookCmd.Parameters.AddWithValue("@bookID", textBox3.Text);

                        int memberCount = Convert.ToInt32(checkMemberCmd.ExecuteScalar());
                        int bookCount = Convert.ToInt32(checkBookCmd.ExecuteScalar());

                        if (memberCount == 0 || bookCount == 0)
                        {
                            MessageBox.Show("Invalid Member ID or Book ID.");
                            return;
                        }
                    }

                    // Insert record
                    string insertQuery = "INSERT INTO borrowingrecords (borrowing_ID, member_ID, book_ID, borrowing_date, return_date) " +
                                         "VALUES (@borrowingID, @memberID, @bookID, @dateBorrowed, @dateReturn)";

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("@borrowingID", textBox1.Text);
                        insertCmd.Parameters.AddWithValue("@memberID", textBox2.Text);
                        insertCmd.Parameters.AddWithValue("@bookID", textBox3.Text);
                        insertCmd.Parameters.AddWithValue("@dateBorrowed", borrowingDate.ToString("yyyy-MM-dd"));
                        insertCmd.Parameters.AddWithValue("@dateReturn", returnDate.ToString("yyyy-MM-dd"));

                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully inserted into borrowing records.");
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed.");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error: " + ex.Message);
                }
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
 }



