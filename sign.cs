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
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\VIDYA SAGAR YADAV\OneDrive\Documents\dairy management system.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=True"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Signup] (Fname, Email, Password ,Cpassword) VALUES (@Value1, @Value2, @Value3 ,@Value4)", conn);
                    cmd.Parameters.AddWithValue("@Value3", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Value1", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Value2", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Value3", textBox4.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record inserted successfully");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }

            }
        }
    }
}
