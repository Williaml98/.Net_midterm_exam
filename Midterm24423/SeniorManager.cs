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

namespace Midterm24423
{
    public partial class SeniorManager : Form
    {
        public SeniorManager()
        {
            InitializeComponent();
        }

        static SqlConnection con = new SqlConnection("Data Source=WILLIAM\\SQLEXPRESS;Initial Catalog=CITIZEN_DOCS_P_A;Integrated Security=True");


        private void StaffManager_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
