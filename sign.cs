using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dairy_Managment_System
{
    public partial class sign : Form
    {
        public sign()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 F = new Form1();
            F.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname = textBoxFname.Text;
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;
            string confirmPassword = textBoxConfirmPassword.Text;

            // Validate inputs
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Database connection string (adjust as per your local setup)
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\VIDYA SAGAR YADAV\OneDrive\Documents\dairy management system.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Insert query with parameters
                    string query = "INSERT INTO [Signup] (Fname, Email, Password, Cpassword) VALUES (@fname, @Email, @Password, @Cpassword)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Bind parameters
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Cpassword", confirmPassword);

                    // Execute the query
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sign-Up successful!");

                    // Clear the form after successful submission
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Method to clear the form fields
        private void ClearForm()
        {
            textBoxFname.Clear();
            textBoxEmail.Clear();
            textBoxPassword.Clear();
            textBoxConfirmPassword.Clear();
        }
    }
}
        