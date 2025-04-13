using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THE_READ_ROOM
{
    public partial class records: Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";
        public records()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addrecord form = new addrecord();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            member form = new member();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            books form = new books();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            authors form = new authors();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;

                using (MySqlConnection con = new MySqlConnection(conString))
                {
                   
                    con.Open();

                    string query = "SELECT * FROM borrowingrecords";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                    
                        DataTable dt = new DataTable();

                        dt.Load(reader);

                        dataGridView1.DataSource = dt;
                    }
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
                                int id = Convert.ToInt32(row.Cells["borrowing_ID"].Value);

                                string query = "DELETE FROM borrowingrecords WHERE borrowing_ID = @id";
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

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                int borrowingID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["borrowing_ID"].Value);
                DateTime dateBorrowed = Convert.ToDateTime(dataGridView1.Rows[rowIndex].Cells["borrowing_date"].Value);
                DateTime dateReturn = Convert.ToDateTime(dataGridView1.Rows[rowIndex].Cells["return_date"].Value);


                Edit_record form = new Edit_record(borrowingID, dateBorrowed, dateReturn);
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
                    string query = "SELECT borrowing_ID, borrowing_date, return_date FROM borrowingrecords";
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
