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
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill in both username and password.");
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Get the selected role (Admin or Supplier)
                    string role = cmbRole.SelectedItem.ToString();

                    // Prepare SQL command based on the selected role
                    SqlCommand cmd = new SqlCommand("SELECT Username FROM Users WHERE Username = @Username AND Password = @Password AND Role = @Role", conn);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text)); // Hash the password before checking
                    cmd.Parameters.AddWithValue("@Role", role); // Role filter (Admin or Supplier)

                    conn.Open();
                    var result = cmd.ExecuteScalar(); // Execute the command and get the result

                    if (result != null)
                    {
                        MessageBox.Show($"Login successful! Welcome {result.ToString()}.");

                        // Hide the login form and show the appropriate dashboard
                        this.Hide();

                        if (role == "Admin")
                        {
                            AdminDashboard adminDashboard = new AdminDashboard();
                            adminDashboard.Show();
                        }
                        else if (role == "Supplier")
                        {
                            SupplierDashboard supplierDashboard = new SupplierDashboard(); // Assuming you have a SupplierDashboard form
                            supplierDashboard.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    conn.Close();
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



       