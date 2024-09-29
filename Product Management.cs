using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dairy_Managment_System
{
    public partial class Product_Management : Form
    {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string

        public Product_Management()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_Management F = new User_Management();
            F.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User_Management F = new User_Management();
            F.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

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

        private void Logout_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateProductForm()
        {
            // Check if Product Name is empty
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            // Check if Price is a valid decimal
            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            // Check if Category is empty
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                MessageBox.Show("Category is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return false;
            }

            // Check if an image is selected (optional, if needed)
            if (picBoxProductImage.Image == null)
            {
                MessageBox.Show("Please select an image for the product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Optional: Check length constraints for Product Name and Category
            if (txtProductName.Text.Length > 50)
            {
                MessageBox.Show("Product Name should not exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            if (txtCategory.Text.Length > 50)
            {
                MessageBox.Show("Category should not exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return false;
            }

            return true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string imagePath = openFileDialog.FileName;
                    picBoxProductImage.Image = Image.FromFile(imagePath);

                    // Convert the image to binary data
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    SaveImageToDatabase(imageBytes);
                }
            }
        }

        private void SaveImageToDatabase(byte[] imageBytes)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Remove SupplierID from the insert query
                string query = "INSERT INTO Products (ProductName, Price, Category, Image) VALUES (@ProductName, @Price, @Category, @Image)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@Image", imageBytes);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void LoadProductData(int productId)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ProductName, Price, Category,  Image FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtProductName.Text = reader["ProductName"].ToString();
                            txtPrice.Text = reader["Price"].ToString();
                            txtCategory.Text = reader["Category"].ToString();

                            // Convert binary data to an image
                            byte[] imageData = (byte[])reader["Image"];
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                picBoxProductImage.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
        }



        private void button7_Click(object sender, EventArgs e)
        {

            if (!ValidateProductForm()) return; // Run validation first

            byte[] imageBytes = null;
            if (picBoxProductImage.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    picBoxProductImage.Image.Save(ms, picBoxProductImage.Image.RawFormat);
                    imageBytes = ms.ToArray();
                }
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Products (ProductName, Price, Category, Image) VALUES (@ProductName, @Price, @Category, @Image)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@Image", imageBytes);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Product added successfully.");
            ClearForm(); // Clear the form fields after adding
            LoadProducts(); // Reload product list

        }






        private void LoadProducts()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ProductID, ProductName, Price, Category FROM Products";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridViewProducts.DataSource = dt;
                }
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadProducts(); // Call this in the Form Load event to load products
        }

        private void button8_Click(object sender, EventArgs e)
        {
            {
                if (!ValidateProductForm()) return; // Run validation first

                byte[] imageBytes = null;

                // Ensure the picture box contains an image
                if (picBoxProductImage.Image != null)
                {
                    try
                    {
                        // Use a memory stream to save the image
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Clone the image to avoid GDI+ issues
                            Image imageClone = (Image)picBoxProductImage.Image.Clone();

                            // Save the image in PNG format to memory stream
                            imageClone.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                            // Convert the image in memory stream to a byte array
                            imageBytes = ms.ToArray();
                        }
                    }
                    catch (ExternalException ex)
                    {
                        MessageBox.Show("Error processing image: " + ex.Message, "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Database connection and update logic
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Products SET ProductName = @ProductName, Price = @Price, Category = @Category, Image = @Image WHERE ProductID = @ProductID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId);
                            cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                            cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                            cmd.Parameters.AddWithValue("@Image", imageBytes ?? (object)DBNull.Value); // Handle null image

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Product updated successfully.");
                        ClearForm(); // Clear form after updating
                        LoadProducts(); // Reload product list
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


        }



        private void button9_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(productId.Text));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product deleted successfully.");
                ClearForm();
                LoadProducts(); // Reload the product list
            }
        private bool IsProductNameUnique(string productName, int productId = 0)
        {
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Products WHERE ProductName = @ProductName AND ProductID != @ProductID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@ProductID", productId); // Use 0 for new products (add), or current productId for update
                    int count = (int)cmd.ExecuteScalar();
                    return count == 0; // Return true if the product name is unique
                }
            }
        }

        private void ClearForm()
        {
            productId.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
            picBoxProductImage.Image = null;
        }

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridViewProducts.Rows[e.RowIndex];
                    productId.Text = row.Cells["ProductID"].Value.ToString();
                    txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                    txtPrice.Text = row.Cells["Price"].Value.ToString();
                    txtCategory.Text = row.Cells["Category"].Value.ToString();

                // Load the image from the database
                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\VIDYA SAGAR YADAV\\OneDrive\\Documents\\dairy management system.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=False"; // Update with your connection string
                using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT Image FROM Products WHERE ProductID = @ProductID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(productId.Text));
                            byte[] imageBytes = (byte[])cmd.ExecuteScalar();
                            if (imageBytes != null)
                            {
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    picBoxProductImage.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                picBoxProductImage.Image = null;
                            }
                        }
                    }
                }
            }

        private void button10_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }

    


