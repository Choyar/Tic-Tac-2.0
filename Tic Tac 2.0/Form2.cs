using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_2._0
{
    public partial class Form2 : Form
    {
        public static Form2 instance;
        public Form2()
        {
            InitializeComponent();
            instance = this;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            hScrollBar1.Value = Form1.instance.sizex;
            textBox1.Text = hScrollBar1.Value.ToString();
            hScrollBar2.Value = Form1.instance.sizey;
            textBox2.Text = hScrollBar2.Value.ToString();
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.sizex = hScrollBar1.Value;
            textBox1.Text = hScrollBar1.Value.ToString();
        }

        private void HScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.sizey=hScrollBar2.Value;
            textBox2.Text = hScrollBar2.Value.ToString();
        }
    }
}
