using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1021
{
    public class MenuItem
    {
        public String name;
        public String fname;
        public int price;
        public int amount;
        public CheckBox cb;
        public NumericUpDown nm;
        public MenuItem(String n, String f, int p, int a)
        {
            name = n; fname = f; price = p; amount = a;
            cb = new CheckBox();
            cb.Text = n;
            cb.Location = new Point(0, 250);
            cb.AutoSize = true;

            nm = new NumericUpDown();
            nm.Value = a;
            nm.Enabled = true;
            nm.Location = new Point(0, 350);
            nm.AutoSize = true;
            nm.Maximum = 10;
            nm.ValueChanged += nm_ValueChanged;
        }
        private void nm_ValueChanged(object sender, EventArgs e)
        {
            if (cb.Checked)
            {
                amount = (int)nm.Value;
            }
        }
    }
}
