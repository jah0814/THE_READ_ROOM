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
    public partial class books : Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public books()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            MySqlConnection con = new MySqlConnection(conString);
            con.Open();
            string createtable = "Select * from books";
            MySqlCommand cmd = new MySqlCommand(createtable, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addbook form = new addbook();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            authors form = new authors();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            member form = new member();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            records form = new records();
            form.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string connectionString = "server=localhost;database=librarymnsystem;user=root;password=jaaaahz023;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            if (!row.IsNewRow)
                            {
                                int id = Convert.ToInt32(row.Cells["book_ID"].Value);

                                string query = "DELETE FROM books WHERE book_ID = @id";
                                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", id);
                                    cmd.ExecuteNonQuery();
                                }

                                dataGridView1.Rows.Remove(row);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                int bookID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["book_ID"].Value);
                string title = dataGridView1.Rows[rowIndex].Cells["title"].Value.ToString();
                string genre = dataGridView1.Rows[rowIndex].Cells["genre"].Value.ToString();
                string ISBN = dataGridView1.Rows[rowIndex].Cells["ISBN"].Value.ToString();
                int year_published = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["year_published"].Value);
                string publisher_name = dataGridView1.Rows[rowIndex].Cells["publisher_name"].Value.ToString();


                Edit_books form = new Edit_books(bookID, title, genre, ISBN, year_published, publisher_name);
                form.ShowDialog();

                LoadMembers();
            }
            else
            {
                MessageBox.Show("Please select a row to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LoadMembers()
        {
            string connectionString = "server=localhost;database=librarymnsystem;user=root;password=jaaaahz023;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT book_ID, title, genre, ISBN, year_published, publisher_name FROM books";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

