using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Midterm24423
{
    public partial class Citizens : Form
    {
        public Citizens()
        {
            InitializeComponent();
            PopulateUserTypes();
            Populate();
        }

        static SqlConnection con = new SqlConnection("Data Source=WILLIAM\\SQLEXPRESS;Initial Catalog=CITIZEN_DOCS_P_A;Integrated Security=True");

        private void PopulateUserTypes()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT document_name FROM documents", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string userType = reader["document_name"].ToString();
                    comboBoxDoc.Items.Add(userType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void Populate()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT document_id FROM documents", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string userType = reader["document_id"].ToString();
                    comboBoxDocId.Items.Add(userType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void RequestBtn_Click(object sender, EventArgs e)
        {

            string query = "INSERT INTO requests ( citizen_nat_id, document_id, feedback) " +
                             "VALUES (@ProductID, @ProductName, @pro)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", textBox1.Text);
            cmd.Parameters.AddWithValue("@ProductName", comboBoxDocId.Text);
            cmd.Parameters.AddWithValue("@Pro", comboBoxFeed.Text);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully inserted", "Success");
            DisplayItems();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    textBox1.Text = row.Cells[0].Value.ToString();
                    comboBoxDocId.Text = row.Cells[1].Value.ToString();
                    comboBoxFeed.Text = row.Cells[2].Value.ToString();
                   // textBoxSales.Text = row.Cells[3].Value.ToString();
                    //textBoxStock.Text = row.Cells[4].Value.ToString();
                }
            }
        }

        private void Citizens_Load(object sender, EventArgs e)
        {
            DisplayItems();
        }

        private void DisplayItems()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM requests", con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "requests");
            dataGridView1.DataSource = ds.Tables["requests"];
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            string itemId = comboBoxDocId.Text;

            // Ensure that an item ID is provided
            if (string.IsNullOrEmpty(itemId))
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            // Confirm with the user before deleting
            DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Delete the item from the database
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM requests WHERE document_id = @itemid", con);
                    cmd.Parameters.AddWithValue("@itemid", itemId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item deleted successfully!");
                        DisplayItems(); // Refresh the DataGridView after deletion
                    }
                    else
                    {
                        MessageBox.Show("Item ID not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    

    }
}
