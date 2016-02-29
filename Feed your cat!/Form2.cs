using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feed_your_cat_
{
    public partial class Form2 : Form
    {
        public string catName1;
        public string catName2;
        public string catName3;
        public string catName4;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            catName1 = textBox1.Text;
            catName2 = textBox2.Text;
            catName3 = textBox3.Text;
            catName4 = textBox4.Text;
            this.Close();
        }
    }
}
