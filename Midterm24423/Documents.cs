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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Midterm24423
{
    public partial class Documents : Form
    {
        public Documents()
        {
            InitializeComponent();
            PopulateUserTypes();
        }

        static SqlConnection con = new SqlConnection("Data Source=WILLIAM\\SQLEXPRESS;Initial Catalog=CITIZEN_DOCS_P_A;Integrated Security=True");


        private void PopulateUserTypes()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT staff_nat_id FROM staff", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string userType = reader["staff_nat_id"].ToString();
                    comboBox1.Items.Add(userType);
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

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO documents ( document_name, staff_nat_id) " +
                             "VALUES (@ProductID, @ProductName)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", textBoxDocName.Text);
            cmd.Parameters.AddWithValue("@ProductName", comboBox1.Text);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully inserted", "Success");
            //DisplayStudents();
        }
    }
}
