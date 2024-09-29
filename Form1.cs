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

namespace Dairy_Managment_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadRoles();
        }
        private void LoadRoles()
        {
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Supplier");
            cmbRole.SelectedIndex = 0; // Default to Admin
        }

        // Method to hash the password using SHA-256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                // Validate if username and password are not empty
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL Query to retrieve the user role
                    string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text)); // Ensure passwords are hashed

                    string userRole = cmd.ExecuteScalar() as string;

                    if (userRole != null)
                    {
                        // User exists, now check the role
                        if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Welcome Admin!");
                            AdminDashboard adminDashboard = new AdminDashboard();
                            adminDashboard.Show();
                            this.Hide(); // Hide login form
                        }
                        else if (userRole.Equals("Supplier", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Welcome Supplier!");
                            SupplierDashboard supplierDashboard = new SupplierDashboard();
                            supplierDashboard.Show();
                            this.Hide(); // Hide login form
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            private void button2_Click(object sender, EventArgs e)
        {
            sign signUpForm = new sign();
            this.Hide(); // Hide the login form
            signUpForm.Show();
        }
    }
}



       