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
            if (button1.Text == "�إ�")
            {
                if (pass.writePass())
                {
                    MessageBox.Show("�إߦ��\");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    button1.Text = "�n�J";
                    linkLabel1.Visible = true;
                }
                else
                {
                    MessageBox.Show("�إߥ���");
                }
            }
            else
            {
                if (pass.compare())
                {
                    if (u == "root")
                    {
                        MessageBox.Show("�޲z���n�J");
                        Form2 adminForm = new Form2();
                        adminForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("�n�J OK");
                        OrderForm orderForm = new OrderForm(u);
                        orderForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("�n�J����!");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button1.Text = "�إ�";
            linkLabel1.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
