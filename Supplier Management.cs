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

namespace Dairy_Managment_System
{
    public partial class Supplier_Management : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string

        public Supplier_Management()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_Management F = new User_Management();
            F.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product_Management F = new Product_Management();
            F.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void LoadSuppliers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SupplierID, SupplierName, ContactNumber, Email, Address FROM Suppliers";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewSuppliers.DataSource = dt;

                    // Optional: If you don't want to display SupplierID
                    // dataGridViewSuppliers.Columns["SupplierID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Suppliers (SupplierName, ContactNumber, Email, Address) VALUES (@SupplierName, @ContactNumber, @Email, @Address)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                    cmd.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers(); // Refresh the grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    

        private void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
                if (dataGridViewSuppliers.CurrentRow != null && !string.IsNullOrEmpty(txtSupplierID.Text))
                {
                    int supplierId = Convert.ToInt32(txtSupplierID.Text);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "UPDATE Suppliers SET SupplierName = @SupplierName, ContactNumber = @ContactNumber, Email = @Email, Address = @Address WHERE SupplierID = @SupplierID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                            cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                            cmd.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadSuppliers();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error updating supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
                if (dataGridViewSuppliers.CurrentRow != null && !string.IsNullOrEmpty(txtSupplierID.Text))
                {
                    int supplierId = Convert.ToInt32(txtSupplierID.Text);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadSuppliers();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void Supplier_Management_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void dataGridViewSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridViewSuppliers.Rows[e.RowIndex];
                    txtSupplierID.Text = row.Cells["SupplierID"].Value.ToString(); // Assuming you have a txtSupplierID TextBox
                    txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                    txtContactNumber.Text = row.Cells["ContactNumber"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                    txtAddress.Text = row.Cells["Address"].Value.ToString();
                }
            }

        }
    }
    
    
    