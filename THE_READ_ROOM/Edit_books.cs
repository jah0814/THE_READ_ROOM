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
    public partial class Edit_books : Form
    {
        private int book_ID;
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public Edit_books(int id, string title, string genre, string ISBN, int year_published, string publisher_name)
        {
            InitializeComponent();
            book_ID = id;

            textBox1.Text = title;
            textBox2.Text = genre;
            textBox3.Text = ISBN;
            textBox4.Text = year_published.ToString();
            textBox5.Text = publisher_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            string genre = textBox2.Text;
            string ISBN = textBox3.Text;
            int year_published;
            if (!int.TryParse(textBox4.Text, out year_published)) 
            {
                MessageBox.Show("Invalid Year Published. Please enter a number.");
                return;
            }
            string publisher_name = textBox5.Text;

            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            using (MySqlConnection conn = new MySqlConnection(conString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE books SET title = @title, genre = @genre, ISBN = @ISBN, year_published = @year_published, publisher_name = @publisher_name WHERE book_ID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", book_ID);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@genre", genre);
                        cmd.Parameters.AddWithValue("@ISBN", ISBN);
                        cmd.Parameters.AddWithValue("@year_published", year_published);
                        cmd.Parameters.AddWithValue("@publisher_name", publisher_name);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
