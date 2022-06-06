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
        public int sizex, sizey;
        public int[] initialQuantity;
        public int inarow;
        public bool cannibalism;
        private void Form2_Load(object sender, EventArgs e)
        {
            //plate size
            sizex = Form1.instance.sizex;
            hScrollBar1.Value = sizex;
            textBox1.Text = hScrollBar1.Value.ToString();
            sizey = Form1.instance.sizey;
            hScrollBar2.Value = sizey;
            textBox2.Text = hScrollBar2.Value.ToString();
            //Quantities of pieces
            initialQuantity = new int[5];
            for(int a=0;a<5;a++)
            {
                initialQuantity[a] = Form1.instance.initialQuantity[a];
            }

            hScrollBar3.Value = initialQuantity[0];
            textBox3.Text = hScrollBar3.Value.ToString();
            hScrollBar4.Value = initialQuantity[1];
            textBox4.Text = hScrollBar4.Value.ToString();
            hScrollBar5.Value = initialQuantity[2];
            textBox5.Text = hScrollBar5.Value.ToString();
            hScrollBar6.Value = initialQuantity[3];
            textBox6.Text = hScrollBar6.Value.ToString();
            hScrollBar7.Value = initialQuantity[4];
            textBox7.Text = hScrollBar7.Value.ToString();
            //win requirement
            hScrollBar8.Value = 2;
            hScrollBar8.Maximum = new int[2] { sizex, sizey }.Max();
            hScrollBar8.Value = Form1.instance.inarow == 0 ? hScrollBar8.Maximum : Form1.instance.inarow;
            if (Form1.instance.inarow == 0)
            {
                checkBox1.Checked = true;
                hScrollBar8.Enabled = false;
                textBox8.Text = "--";
            }
            else
            {
                checkBox1.Checked = false;
                hScrollBar8.Enabled = true;
                textBox8.Text = hScrollBar8.Value.ToString();
            }

            //spetial rules
            if(Form1.instance.cannibalism)
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            sizex = hScrollBar1.Value;
            textBox1.Text = hScrollBar1.Value.ToString();

            if (new int[] { hScrollBar1.Value, hScrollBar2.Value }.Max() < hScrollBar8.Value)
            {
                hScrollBar8.Value = new int[] { hScrollBar1.Value, hScrollBar2.Value }.Max();
            }
            hScrollBar8.Maximum = new int[] { hScrollBar1.Value, hScrollBar2.Value }.Max();
        }

        private void HScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            sizey=hScrollBar2.Value;
            textBox2.Text = hScrollBar2.Value.ToString();

            if (new int[] { hScrollBar1.Value, hScrollBar2.Value }.Max() < hScrollBar8.Value)
            {
                hScrollBar8.Value = new int[] { hScrollBar1.Value, hScrollBar2.Value }.Max();
            }
            hScrollBar8.Maximum = new int[] { hScrollBar1.Value, hScrollBar2.Value }.Max();
        }

        private void HScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            initialQuantity[0] = hScrollBar3.Value;
            textBox3.Text = hScrollBar3.Value.ToString();
        }

        private void HScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            initialQuantity[1] = hScrollBar4.Value;
            textBox4.Text = hScrollBar4.Value.ToString();
        }

        private void HScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            initialQuantity[2] = hScrollBar5.Value;
            textBox5.Text = hScrollBar5.Value.ToString();
        }

        private void HScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            initialQuantity[3] = hScrollBar6.Value;
            textBox6.Text = hScrollBar6.Value.ToString();
        }

        private void HScrollBar7_Scroll(object sender, ScrollEventArgs e)
        {
            initialQuantity[4] = hScrollBar7.Value;
            textBox7.Text = hScrollBar7.Value.ToString();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                hScrollBar8.Enabled = false;
                inarow = 0;
                textBox8.Text = "--";
            }
            else
            {
                hScrollBar8.Enabled = true;
                inarow = hScrollBar8.Value;
                textBox8.Text = hScrollBar8.Value.ToString();
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                cannibalism = true;
            }
            else
            {
                cannibalism = false;
            }
        }

        private void HScrollBar8_Scroll(object sender, ScrollEventArgs e)
        {
            inarow = hScrollBar8.Value;
            textBox8.Text = hScrollBar8.Value.ToString();
        }
    }
}
