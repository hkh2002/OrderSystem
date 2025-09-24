using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _1021
{
    public partial class ListOrders : Form
    {
        List<MenuItem> items;
        String account;
        String conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\ireen\\Desktop\\TCU\\大四上\\視窗程式\\1021\\1021\\Database1.mdf;Integrated Security=True";
        SqlConnection conn;
        public ListOrders(List<MenuItem> items, String acc)
        {
            InitializeComponent();
            account = acc;
            this.items = items;
            String[] orders = new String[items.Count + 1];
            int i = 1;
            int total = 0;
            orders[0] = "桌次" + account + "的訂單如下:";
            foreach (MenuItem item in items)
            {
                orders[i] = item.name + " " + item.price + " " + item.amount + "份";
                total = total + item.price * item.amount;
                i++;
            }
            label2.Text = label2.Text + total;
            listBox1.Items.AddRange(orders);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //File.Delete(account + ".txt");
            using (conn = new SqlConnection(conString))
            {
                conn.Open();
                String query = "DELETE FROM [Order] WHERE [table] = @table";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@table", account);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            foreach (MenuItem x in items)
            {
                try
                {
                    /*StreamWriter sw = new StreamWriter(account + ".txt", true);
                    sw.WriteLine(x.name + "," + x.price + "," + x.amount);
                    sw.Close();*/
                    using (conn = new SqlConnection(conString))
                    {
                        conn.Open();
                        String query = "INSERT INTO [Order] ([table], price, amount, name) VALUES (@table, @price, @amount, @name)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@table", account);
                        cmd.Parameters.AddWithValue("@price", x.price);
                        cmd.Parameters.AddWithValue("@amount", x.amount);
                        cmd.Parameters.AddWithValue("@name", x.name);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            MessageBox.Show("已送出訂單!");
            items.Clear();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
