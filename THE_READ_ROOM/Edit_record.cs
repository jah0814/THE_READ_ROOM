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
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace THE_READ_ROOM
{
    public partial class Edit_record: Form
    {
        private int borrowing_ID;
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public Edit_record(int id, DateTime dateBorrowed, DateTime dateReturn)
        {
            InitializeComponent();
            borrowing_ID = id;

            this.borrowing_ID = borrowing_ID; // Store borrowing_ID for updates but do not display it
            LoadBorrowingRecord(borrowing_ID);

        }
        private void LoadBorrowingRecord(int borrowingID)
        {
            string conString = "server=localhost;uid=root;pwd=jaaaahz023;database=librarymnsystem";

            using (MySqlConnection conn = new MySqlConnection(conString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT member_ID, book_ID, borrowing_date, return_date FROM borrowingrecords WHERE borrowing_ID = @borrowingID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@borrowingID", borrowingID);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["member_ID"].ToString();
                                textBox2.Text = reader["book_ID"].ToString();
                                textBox3.Text = Convert.ToDateTime(reader["borrowing_date"]).ToString("yyyy-MM-dd");
                                textBox4.Text = Convert.ToDateTime(reader["return_date"]).ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conString = "server=localhost;uid=root;pwd=jaaaahz023;database=librarymnsystem";

            using (MySqlConnection conn = new MySqlConnection(conString))
            {
                try
                {
                    conn.Open();

                    // Validate Member ID and Book ID as numbers
                    if (!Regex.IsMatch(textBox1.Text, @"^\d+$") || !Regex.IsMatch(textBox2.Text, @"^\d+$"))
                    {
                        MessageBox.Show("Member ID and Book ID must be numbers.");
                        return;
                    }

                    // Validate Dates
                    if (!DateTime.TryParse(textBox3.Text, out DateTime borrowingDate) ||
                        !DateTime.TryParse(textBox4.Text, out DateTime returnDate))
                    {
                        MessageBox.Show("Please enter valid dates.");
                        return;
                    }

                    if (returnDate < borrowingDate)
                    {
                        MessageBox.Show("Return date cannot be before borrowing date.");
                        return;
                    }

                    // Update the record in the database
                    string query = "UPDATE borrowingrecords SET member_ID = @memberID, book_ID = @bookID, " +
                                   "borrowing_date = @dateBorrowed, return_date = @dateReturn WHERE borrowing_ID = @borrowingID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@memberID", textBox1.Text);
                        cmd.Parameters.AddWithValue("@bookID", textBox2.Text);
                        cmd.Parameters.AddWithValue("@dateBorrowed", borrowingDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@dateReturn", returnDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@borrowingID", borrowing_ID); // Use stored borrowing_ID

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Close form after saving
                        }
                        else
                        {
                            MessageBox.Show("Update failed. No changes made.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        
    }
}
