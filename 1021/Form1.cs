namespace _1021
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String u = textBox1.Text;
            String p = textBox2.Text;
            CPass pass = new CPass(u, p);
            if (button1.Text == "建立")
            {
                if (pass.writePass())
                {
                    MessageBox.Show("建立成功");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    button1.Text = "登入";
                    linkLabel1.Visible = true;
                }
                else
                {
                    MessageBox.Show("建立失敗");
                }
            }
            else
            {
                if (pass.compare())
                {
                    if (u == "root")
                    {
                        MessageBox.Show("管理員登入");
                        Form2 adminForm = new Form2();
                        adminForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("登入 OK");
                        OrderForm orderForm = new OrderForm(u);
                        orderForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("登入失敗!");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button1.Text = "建立";
            linkLabel1.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
