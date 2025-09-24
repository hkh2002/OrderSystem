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

namespace _1021
{
    public partial class Form2 : Form
    {
        String conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\ireen\\Desktop\\TCU\\大四上\\視窗程式\\1021\\1021\\Database1.mdf;Integrated Security=True";
        SqlConnection conn;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String u = textBox1.Text;
            String p = textBox2.Text;
            if (u == "" || p == "")
            {
                MessageBox.Show("請填入資料");
                return;
            }
            CPass pass = new CPass(u, p);
            if (pass.writePass())
            {
                MessageBox.Show("新增成功");
                textBox1.Text = "";
                textBox2.Text = "";
                panel1.Visible = false;
            }
            else
            {
                MessageBox.Show("新增失敗");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            String u = textBox3.Text;
            if (u == "")
            {
                MessageBox.Show("請填入資料");
                return;
            }
            using (conn = new SqlConnection(conString))
            {
                conn.Open();
                String query = "DELETE FROM [Account] WHERE account = @uac";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@uac", u);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            MessageBox.Show("刪除成功");
            textBox3.Text = "";
            panel2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label8.Text = Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String input = textBox6.Text;
            int price;
            if (textBox5.Text == "" || textBox6.Text == "" || label8.Text == "")
            {
                MessageBox.Show("請填入資料");
                return;
            }
            if (!int.TryParse(input, out price))
            {
                MessageBox.Show("價格請輸入整數");
                return;
            }
            using (conn = new SqlConnection(conString))
            {
                conn.Open();
                String query = "INSERT INTO [Menu] (name, image, price) VALUES (@name, @image, @price)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", textBox5.Text);
                cmd.Parameters.AddWithValue("@image", openFileDialog1.FileName);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            MessageBox.Show("新增成功");
            textBox5.Text = "";
            textBox6.Text = "";
            label8.Text = "";
            panel3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            String name = textBox7.Text;
            if (name == "")
            {
                MessageBox.Show("請填入資料");
                return;
            }
            using (conn = new SqlConnection(conString))
            {
                conn.Open();
                String query = "DELETE FROM [Menu] WHERE name = @name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            MessageBox.Show("刪除成功");
            textBox7.Text = "";
            panel4.Visible = false;
        }
    }
}
