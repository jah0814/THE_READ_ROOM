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
    public partial class member: Form
    {
        string server = "localhost";
        string uid = "root";
        string password = "jaaaahz023";
        string database = "librarymnsystem";

        public static member instance;
        public member()
        {
            InitializeComponent();
                instance = this;
        }

        private void member_Load(object sender, EventArgs e)
        {
           
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            string conString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database;
            MySqlConnection con = new MySqlConnection(conString);
            con.Open();
            string createtable = "Select * from members";
            MySqlCommand cmd = new MySqlCommand(createtable, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addmember form = new addmember();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
                                int id = Convert.ToInt32(row.Cells["member_ID"].Value);

                                string query = "DELETE FROM members WHERE member_ID = @id";
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                int memberID = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["member_ID"].Value);
                string firstName = dataGridView1.Rows[rowIndex].Cells["first_name"].Value.ToString();
                string middleInitial = dataGridView1.Rows[rowIndex].Cells["middle_initial"].Value.ToString();
                string lastName = dataGridView1.Rows[rowIndex].Cells["last_name"].Value.ToString();
                string phone = dataGridView1.Rows[rowIndex].Cells["phone_number"].Value.ToString();

                
                Edit_member form = new Edit_member(memberID, firstName, middleInitial, lastName, phone);
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
                    string query = "SELECT member_ID, first_name, middle_initial, last_name, phone_number FROM members";
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
