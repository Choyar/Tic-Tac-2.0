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
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //plate size
            hScrollBar1.Value = Form1.instance.sizex;
            textBox1.Text = hScrollBar1.Value.ToString();
            hScrollBar2.Value = Form1.instance.sizey;
            textBox2.Text = hScrollBar2.Value.ToString();
            //Quantities of pieces
            hScrollBar3.Value = Form1.instance.initialQuantity[0];
            textBox3.Text = hScrollBar3.Value.ToString();
            hScrollBar4.Value = Form1.instance.initialQuantity[1];
            textBox4.Text = hScrollBar4.Value.ToString();
            hScrollBar5.Value = Form1.instance.initialQuantity[2];
            textBox5.Text = hScrollBar5.Value.ToString();
            hScrollBar6.Value = Form1.instance.initialQuantity[3];
            textBox6.Text = hScrollBar6.Value.ToString();
            hScrollBar7.Value = Form1.instance.initialQuantity[4];
            textBox7.Text = hScrollBar7.Value.ToString();
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

        private void HScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.initialQuantity[0] = hScrollBar3.Value;
            textBox3.Text = hScrollBar3.Value.ToString();
        }

        private void HScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.initialQuantity[1] = hScrollBar4.Value;
            textBox4.Text = hScrollBar4.Value.ToString();
        }

        private void HScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.initialQuantity[2] = hScrollBar5.Value;
            textBox5.Text = hScrollBar5.Value.ToString();
        }

        private void HScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.initialQuantity[3] = hScrollBar6.Value;
            textBox6.Text = hScrollBar6.Value.ToString();
        }

        private void HScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            Form1.instance.initialQuantity[4] = hScrollBar7.Value;
            textBox7.Text = hScrollBar7.Value.ToString();
        }
    }
}
