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

namespace _1021
{
    public partial class OrderForm : Form
    {
        List<MenuItem> menuItems;
        List<MenuItem> orderItems;
        String account;
        String conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\ireen\\Desktop\\TCU\\大四上\\視窗程式\\1021\\1021\\Database1.mdf;Integrated Security=True";
        SqlConnection conn;
        public OrderForm(String acc)
        {
            InitializeComponent();
            account = acc;
            menuItems = new List<MenuItem>();
            orderItems = new List<MenuItem>();
            try
            {
                /*StreamReader sr = new StreamReader("menu.txt");
                String line = sr.ReadLine();
                while (line != null)
                {
                    String[] s = line.Split(',');
                    int price = int.Parse(s[2]);
                    int amount = int.Parse(s[3]);
                    MenuItem tmp = new MenuItem(s[0], s[1], price, amount);
                    menuItems.Add(tmp);
                    line = sr.ReadLine();
                }
                addToPanel();
                sr.Close();*/
                using (conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String query = "SELECT name, image, price, amount FROM Menu";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = reader["name"].ToString();
                        string image = reader["image"].ToString();
                        int price = int.Parse(reader["Price"].ToString());
                        int amount = int.Parse(reader["amount"].ToString());

                        MenuItem tmp = new MenuItem(name, image, price, amount);
                        menuItems.Add(tmp);
                        addToPanel();
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addToPanel()
        {
            foreach (MenuItem x in menuItems)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Location = new Point(0, 0);
                pictureBox.Size = new Size(240, 240);
                try
                {
                    pictureBox.Load(x.fname);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Label label2 = new Label();
                label2.Text = "售 價: " + x.price + "元";
                label2.Location = new Point(0, 300);
                label2.AutoSize = true;
                Panel panel = new Panel();
                panel.Size = new Size(330, 450);
                panel.Controls.Add(pictureBox);
                panel.Controls.Add(x.cb);
                panel.Controls.Add(label2);
                panel.Controls.Add(x.nm);
                flowLayoutPanel1.Controls.Add(panel);
                panel.Update();
            }
            flowLayoutPanel1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (MenuItem x in menuItems)
            {
                if (x.cb.Checked)
                {
                    orderItems.Add(x);
                }
            }
            MessageBox.Show("加入點餐清單完成!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListOrders lo = new ListOrders(orderItems, account);
            lo.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
