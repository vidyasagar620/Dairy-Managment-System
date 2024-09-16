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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;


namespace Dairy_Managment_System
{
    public partial class NewCustomer : Form
    {
        public NewCustomer()
        {
            InitializeComponent();
        }

        private void nEWCUSTOMERToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewCustomer n = new NewCustomer();
            n.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
                string Name = NameBox.Text;
                string Mobile = MobileBox.Text;
                string Email = EmailBox.Text;
                string Pincode = PinBox.Text;
                string Address = AddressBox.Text;

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\VIDYA SAGAR YADAV\OneDrive\Documents\dairy management system.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Insert query with parameters
                        string query = "INSERT INTO [NewCustomer] (Name, Mobile, Email, Pincode, Address) VALUES (@Name, @Mobile, @Email, @Pincode, @Address)";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        // Bind parameters
                        cmd.Parameters.AddWithValue("@Name", NameBox.Text);
                        cmd.Parameters.AddWithValue("@Mobile", MobileBox.Text);
                        cmd.Parameters.AddWithValue("@Email", EmailBox.Text);
                        cmd.Parameters.AddWithValue("@Pincode", PinBox.Text);
                        cmd.Parameters.AddWithValue("@Address", AddressBox.Text);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Save Data successful!");

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
            NameBox.Clear();
            MobileBox.Clear();
            EmailBox.Clear();
            PinBox.Clear();
            AddressBox.Clear();
        }

    }
}

