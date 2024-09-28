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
            this.Hide();
        }

        private void LoadRoles()
        {
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Supplier");
            cmbRole.SelectedIndex = 0; // Default to Admin
        }

        private void button1_Click(object sender, EventArgs e)
        {
                if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text) || cmbRole.SelectedItem == null)
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                string hashedPassword = HashPassword(txtPassword.Text); // Hash the password

                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("spUserSignUp", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add the necessary parameters
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@Role", cmbRole.SelectedItem.ToString());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration successful.");

                        // Open the Admin Dashboard if the role is Admin
                        if (cmbRole.SelectedItem.ToString() == "Admin")
                        {
                            AdminDashboard adminDashboard = new AdminDashboard();
                            this.Hide(); // Hide the sign-up form
                            adminDashboard.Show(); // Show the Admin Dashboard
                        }
                        else
                        {
                            // Handle other roles (e.g., Supplier)
                            MessageBox.Show("You have been registered as a Supplier.");
                        Form1 s = new Form1();
                        s.Show();
                        this.Hide();
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
        

            private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var builder = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}