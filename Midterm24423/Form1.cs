using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Midterm24423
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static SqlConnection con = new SqlConnection("Data Source=WILLIAM\\SQLEXPRESS;Initial Catalog=CITIZEN_DOCS_P_A;Integrated Security=True");

        private string encryptPwd(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {

            if (textBoxPassword.Text.Equals(textBoxConfirmP.Text))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO citizens (citizen_nat_id, fullname,address, phone, email, password) VALUES(@v_fullname,@v_email, @v_op, @v_oop,@v_ooz, @v_password )", con);
                    cmd.Parameters.AddWithValue("@v_fullname", textBoxNatId.Text);
                    cmd.Parameters.AddWithValue("@v_email", textBoxNames.Text);
                    cmd.Parameters.AddWithValue("@v_op", textBoxAddress.Text);
                    cmd.Parameters.AddWithValue("@v_oop", textBoxPhone.Text);
                    cmd.Parameters.AddWithValue("@v_ooz", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@v_password", encryptPwd(textBoxPassword.Text));

                    con.Close();  // If the connection was open it will be closed
                    con.Open(); // And we will open it again .It's just for having one connectio running

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    if (rowsAffected >= 1)
                    {
                        MessageBox.Show("Account successfully created");
                        Login loginForm = new Login();
                        loginForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Account not created");
                    }



                }
                catch (SqlException ex)
                {
                    MessageBox.Show("There is an error, pleease check your inputs " + ex.Message);
                } // the above ex.Message just give us the error's guide
            }
            else
            {
                MessageBox.Show("Password don't match");
            }
        }
    }
}
