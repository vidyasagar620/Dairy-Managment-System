using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dairy_Managment_System
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_Management F = new User_Management();
            F.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Supplier_Management F = new Supplier_Management();
            F.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product_Management F = new Product_Management();
            F.Show();
            this.Hide();
        }

        private void Logout_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
