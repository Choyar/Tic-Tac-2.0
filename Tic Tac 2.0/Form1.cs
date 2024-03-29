﻿using System;
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
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Form1()
        {
            InitializeComponent();
            instance = this;
        }

        public class board
        {
            public int turn = 2;
            int sizex, sizey;
            public int nowMoving = 0;
            public Bitmap nowima;
            public int[,] blocks;
            public PictureBox bg;
            public int winline;
            public PictureBox[,] plocks;
            void drawwinline(int dir,int num)
            {
                //dir==0: horizontal; num: top to bottom 0,1,2,...
                //dir ==1: verticle; num: left to right 0,1,2,...
                //dir ==2: 45degree ; num: 0:\ ; 1:/ 
                //winline = new PictureBox();
                //winline.Size=bg.Size;
                //winline.Location = bg.Location;
                //winline.BackColor = Color.FromArgb(0,0,0,0);
                //if(dir==0)
                //{
                //    Bitmap bpm = new Bitmap(winline.Width, winline.Height);
                //    Graphics g = Graphics.FromImage(bpm);
                //    float offsetx = -winline.Location.X + plocks[0, 0].Width / 2;
                //    float offsety = -winline.Location.Y + plocks[0, 0].Height / 2;
                //    g.DrawLine(new Pen(Color.Red,5), 
                //        plocks[0, num].Location.X + offsetx,            plocks[0, num].Location.Y + offsety,
                //        plocks[sizex - 1, num].Location.X + offsetx,    plocks[sizex - 1, num].Location.Y + offsety);
                //    winline.Image = bpm;
                //}
                //else if (dir == 1)
                //{
                //    Bitmap bpm = new Bitmap(winline.Width, winline.Height);
                //    Graphics g = Graphics.FromImage(bpm);
                //    float offsetx = -winline.Location.X + plocks[0, 0].Width / 2;
                //    float offsety = -winline.Location.Y + plocks[0, 0].Height / 2;
                //    g.DrawLine(new Pen(Color.Red, 5),
                //        plocks[num, 0].Location.X + offsetx, plocks[num, 0].Location.Y + offsety,
                //        plocks[num, sizey - 1].Location.X + offsetx, plocks[num, sizey - 1].Location.Y + offsety);
                //    winline.Image = bpm;
                //}

            }
            public int checkwin()
            {
                if(winline==0)
                {
                    //horizontal lines
                    for (int a = 0; a < sizey; a++)
                    {
                        int wteam = blocks[0, a] % 10;
                        if (wteam != 0)
                        {
                            bool win = true;
                            for (int b = 1; b < sizex; b++)
                            {
                                if (blocks[b, a] % 10 != wteam)
                                {
                                    win = false;
                                }
                            }
                            if (win)
                            {
                                drawwinline(0, a);
                                return wteam;
                            }
                        }
                    }
                    //vertical lines
                    for (int a = 0; a < sizex; a++)
                    {
                        int wteam = blocks[a, 0] % 10;
                        if (wteam != 0)
                        {
                            bool win = true;
                            for (int b = 1; b < sizey; b++)
                            {
                                if (blocks[a, b] % 10 != wteam)
                                {
                                    win = false;
                                }
                            }
                            if (win)
                            {
                                drawwinline(1, a);
                                return wteam;
                            }
                        }
                    }
                    if (sizex == sizey)
                    {
                        //left high, right low
                        int wteam = blocks[0, 0] % 10;
                        if (wteam != 0)
                        {
                            bool win = true;
                            for (int a = 0; a < sizex; a++)
                            {
                                if (blocks[a, a] % 10 != wteam)
                                {
                                    win = false;
                                }
                            }
                            if (win)
                            {
                                drawwinline(2, 0);
                                return wteam;
                            }
                        }
                        //left low, right high
                        wteam = blocks[0, sizey - 1] % 10;
                        if (wteam != 0)
                        {
                            bool win = true;
                            for (int a = 0; a < sizex; a++)
                            {
                                if (blocks[a, sizey - 1 - a] % 10 != wteam)
                                {
                                    win = false;
                                }
                            }
                            if (win)
                            {
                                drawwinline(2, 1);
                                return wteam;
                            }
                        }
                    }
                    return 0;
                }
                else
                {
                    //horizontal lines
                    for (int a = 0; a < sizey; a++)
                    {
                        int wteam = blocks[0, a] % 10;
                        int win = 1;
                        for (int b = 1; b < sizex; b++)
                        {
                            win++;
                            if (blocks[b, a] % 10 != wteam)
                            {
                                wteam = blocks[b, a] % 10;
                                win = 1;
                            }
                            if (win == winline && wteam != 0) 
                            {
                                drawwinline(0, a);
                                return wteam;
                            }
                        }
                    }
                    //vertical lines
                    for (int a = 0; a < sizex; a++)
                    {
                        int wteam = blocks[a, 0] % 10;
                        int win = 1;
                        for (int b = 1; b < sizey; b++)
                        {
                            win++;
                            if (blocks[a, b] % 10 != wteam)
                            {
                                wteam = blocks[a, b] % 10;
                                win = 1;
                            }
                            if (win == winline && wteam != 0)
                            {
                                drawwinline(1, a);
                                return wteam;
                            }
                        }
                    }
                    //left high, right low
                    for (int a = 0; a < sizex; a++)
                    {
                        int wteam = blocks[a, 0] % 10;
                        int win = 1;
                        for (int b = 1; b + a < sizex && b < sizey; b++) 
                        {
                            win++;
                            if (blocks[a + b, b] % 10 != wteam)
                            {
                                wteam = blocks[a + b, b] % 10;
                                win = 1;
                            }
                            if (win == winline && wteam != 0)
                            {
                                drawwinline(2, 0);
                                return wteam;
                            }
                        }
                    }
                    for(int a=0;a<sizey;a++)
                    {
                        int wteam = blocks[0, a] % 10;
                        int win = 1;
                        for (int b = 1; b < sizex && a + b < sizey; b++)
                        {
                            win++;
                            if (blocks[b, a + b] % 10 != wteam)
                            {
                                wteam = blocks[b, a + b] % 10;
                                win = 1;
                            }
                            if (win == winline && wteam != 0)
                            {
                                drawwinline(2, 0);
                                return wteam;
                            }
                        }
                    }
                    //left low, right high
                    for (int a = 0; a < sizex; a++)
                    {
                        int wteam = blocks[sizex - 1 - a, 0] % 10;
                        int win = 1;
                        for (int b = 1; sizex - 1 - a - b >= 0 && b < sizey; b++) 
                        {
                            win++;
                            if (blocks[sizex - 1 - a - b, b] % 10 != wteam)
                            {
                                wteam = blocks[sizex - 1 - a - b, b] % 10;
                                win = 1;
                            }
                            if (win == winline && wteam != 0)
                            {
                                drawwinline(2, 1);
                                return wteam;
                            }
                        }
                    }
                    for(int a=0;a<sizey;a++)
                    {
                        int wteam = blocks[sizex - 1, a] % 10;
                        int win = 1;
                        for (int b = 1; sizex - 1 - b >= 0 && b + a < sizey; b++)
                        {
                            win++;
                            if (blocks[sizex - 1 - b, a + b] % 10 != wteam)
                            {
                                wteam = blocks[sizex - 1 - b, a + b] % 10;
                                win = 1;
                            }
                            if (win == winline && wteam != 0)
                            {
                                drawwinline(2, 1);
                                return wteam;
                            }
                        }
                    }
                    return 0;
                }
            }
            public void SetPlate(Form1 f,int x,int y,int locationx,int locationy,int height,int width)
            {
                //remove old plate
                try
                {
                    foreach (PictureBox pic in plocks)
                    {
                        f.Controls.Remove(pic);
                    }
                }
                catch { }
                try
                {
                    f.Controls.Remove(bg);
                }
                catch { }

                //set new plate
                sizex = x;
                sizey = y;
                blocks = new int[x, y];
                for (int a = 0; a < x; a++)
                {
                    for (int b = 0; b < y; b++)
                    {
                        blocks[a, b] = 0;
                    }
                }

                plocks = new PictureBox[x, y];
                bg = new PictureBox();
                bg.Size = new Size(width, height);
                bg.Location = new Point(167, 28);
                for (int a = 0; a < sizex; a++)
                {
                    for (int b = 0; b < sizey; b++)
                    {
                        plocks[a, b] = new PictureBox();
                        plocks[a, b].Size = new Size((int)Math.Ceiling(((float)bg.Size.Width - (5 * (sizex - 1))) / sizex), (int)Math.Ceiling(((float)(bg.Size.Height - (5 * (sizey - 1))) / sizey)));
                        plocks[a, b].Location = new Point(bg.Location.X + (a * 5 + a * plocks[a, b].Size.Width), bg.Location.Y + (b * 5 + b * plocks[a, b].Size.Height));
                        plocks[a, b].BackColor = Color.White;
                        plocks[a, b].Name = a.ToString() + b.ToString();
                        plocks[a, b].SizeMode = PictureBoxSizeMode.Zoom;
                        plocks[a, b].Click += f.Picture_Click;
                    }
                }
                foreach (PictureBox pic in plocks)
                {
                    f.Controls.Add(pic);
                    pic.BringToFront();
                }
                f.Controls.Add(bg);

            }
        }
        public class player
        {
            public int team;
            public int[] remaining = new int[5];
            public player(int i)
            {
                team = i;
                remaining = new int[] { 0, 0, 0, 0, 0 };
            }

            public void move(int i)
            {
                remaining[i]--;
            }
        }

        //default value
        public int sizex = 3, sizey = 3;
        public board b=new board();
        public int[] initialQuantity;
        public player player1 =new player(1);
        public player player2 = new player(2);
        int winner = 0;
        public int inarow = 0;
        public bool cannibalism = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            initialQuantity = new int[5] { 64, 10, 5, 2, 1 };
            Init();
        }

        //initialize
        private void Init()
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            winner = 0;
            b.turn = 2;
            b.nowMoving = 0;
            b.nowima = null;
            for(int a=0;a<5;a++)
            {
                player1.remaining[a] = initialQuantity[a];
                player2.remaining[a] = initialQuantity[a];
            }
            foreach (GroupBox g in Controls.OfType<GroupBox>())
            {
                foreach (Label l in g.Controls.OfType<Label>())
                {
                    l.Visible = true;
                }
                foreach(PictureBox pic in g.Controls.OfType<PictureBox>())
                {
                    pic.Visible = true;
                }
            }
            ResetLabels();
            b.SetPlate(this,sizex,sizey, 167, 28, 360, 360);
            b.winline = inarow;
            if (b.winline == 0)
            {
                label11.Text = "Win Requirement: \r\nSide to Side";
            }
            else
            {
                label11.Text = $"Win Requirement: \r\n{b.winline} in a row";
            }
            if(cannibalism==true)
            {
                pictureBox11.Visible = true;
            }
            else
            {
                pictureBox11.Visible = false;
            }
            nextturn();
        }
        private void ResetLabels()
        {
            //reset all label color
            foreach (GroupBox g in Controls.OfType<GroupBox>())
            {
                foreach (Label l in g.Controls.OfType<Label>())
                {
                    l.ForeColor = Color.Black;
                    l.BackColor = Color.White;
                }
            }
            //hide pieces with 0 remaining
            label1.Text = $"*{player1.remaining[0]}";
            if (player1.remaining[0] == 0)
            {
                label1.Visible = false;
                pictureBox1.Visible = false;
            }
            label2.Text = $"*{player2.remaining[0]}";
            if (player2.remaining[0] == 0)
            {
                label2.Visible = false;
                pictureBox2.Visible = false;
            }
            label3.Text = $"*{player1.remaining[1]}";
            if (player1.remaining[1] == 0)
            {
                label3.Visible = false;
                pictureBox3.Visible = false;
            }
            label4.Text = $"*{player2.remaining[1]}";
            if (player2.remaining[1] == 0)
            {
                label4.Visible = false;
                pictureBox4.Visible = false;
            }
            label5.Text = $"*{player1.remaining[2]}";
            if (player1.remaining[2] == 0)
            {
                label5.Visible = false;
                pictureBox5.Visible = false;
            }
            label6.Text = $"*{player2.remaining[2]}";
            if (player2.remaining[2] == 0)
            {
                label6.Visible = false;
                pictureBox6.Visible = false;
            }
            label7.Text = $"*{player1.remaining[3]}";
            if (player1.remaining[3] == 0)
            {
                label7.Visible = false;
                pictureBox7.Visible = false;
            }
            label8.Text = $"*{player2.remaining[3]}";
            if (player2.remaining[3] == 0)
            {
                label8.Visible = false;
                pictureBox8.Visible = false;
            }
            label9.Text = $"*{player1.remaining[4]}";
            if (player1.remaining[4] == 0)
            {
                label9.Visible = false;
                pictureBox9.Visible = false;
            }
            label10.Text = $"*{player2.remaining[4]}";
            if (player2.remaining[4] == 0)
            {
                label10.Visible = false;
                pictureBox10.Visible = false;
            }
        }
        

        private void nextturn()
        {
            winner = b.checkwin();
            if(winner!=0)
            {
                //Controls.Add(b.winline);
                //b.winline.BringToFront();
                MessageBox.Show( $"Player {winner} wins!","Finished!");
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
            }
            else
            {
                if (b.turn == 1)
                {
                    b.turn = 2;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = true;
                    //change background color to orange
                    Bitmap bpm = new Bitmap(b.bg.Width, b.bg.Height);
                    Graphics g = Graphics.FromImage(bpm);
                    g.FillRectangle(Brushes.Orange, 0, 0, b.bg.Width, b.bg.Height);
                    b.bg.Image = bpm;
                }
                else
                {
                    b.turn = 1;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = false;
                    //change background color ot blue
                    Bitmap bpm = new Bitmap(b.bg.Width, b.bg.Height);
                    Graphics g = Graphics.FromImage(bpm);
                    g.FillRectangle(Brushes.DodgerBlue, 0, 0, b.bg.Width, b.bg.Height);
                    b.bg.Image = bpm;
                }
            }
        }

        private bool checkrule(string loc)
        {
            //check if the move is within the rule
            if(cannibalism==false)
            {
                if (b.nowMoving / 10 <= b.blocks[loc[0] - '0', loc[1] - '0'] / 10)
                {
                    return false;
                }
            }
            else
            {
                if (b.nowMoving / 10 < b.blocks[loc[0] - '0', loc[1] - '0'] / 10)
                {
                    return false;
                }
            }
            
            if(winner!=0)
            {
                return false;
            }
            return true;
        }

        //put a piece into a block
        private void Picture_Click(object sender, EventArgs e)
        {
            if(b.nowMoving!=0)
            {
                string loc = (sender as PictureBox).Name;
                if (checkrule(loc))
                {
                    b.blocks[loc[0] - '0', loc[1] - '0'] = b.nowMoving;
                    (sender as PictureBox).Image = b.nowima;
                    string target = (sender as PictureBox).Name;

                    //take out the used piece
                    if (b.nowMoving % 10 == player1.team)
                    {
                        player1.remaining[b.nowMoving / 10 - 1] -= 1;
                    }
                    else
                    {
                        player2.remaining[b.nowMoving / 10 - 1] -= 1;
                    }
                    b.nowMoving = 0;
                    ResetLabels();
                    nextturn();
                }
            }
        }

        //select a piece
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if(b.turn==player1.team&&player1.remaining[0]>0)
            {
                ResetLabels();
                b.nowMoving = 11;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label1.BackColor = Color.DodgerBlue;
                label1.ForeColor = Color.White;
            }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            if (b.turn == player1.team && player1.remaining[1] > 0)
            {
                ResetLabels();
                b.nowMoving = 21;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label3.BackColor = Color.DodgerBlue;
                label3.ForeColor = Color.White;
            }
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            if (b.turn == player1.team && player1.remaining[2] > 0)
            {
                ResetLabels();
                b.nowMoving = 31;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label5.BackColor = Color.DodgerBlue;
                label5.ForeColor = Color.White;
            }
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            if (b.turn == player1.team && player1.remaining[3] > 0)
            {
                ResetLabels();
                b.nowMoving = 41;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label7.BackColor = Color.DodgerBlue;
                label7.ForeColor = Color.White;
            }
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            if (b.turn == player1.team && player1.remaining[4] > 0)
            {
                ResetLabels();
                b.nowMoving = 51;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label9.BackColor = Color.DodgerBlue;
                label9.ForeColor = Color.White;
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (b.turn == player2.team && player2.remaining[0] > 0)
            {
                ResetLabels();
                b.nowMoving = 12;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label2.BackColor = Color.Orange;
                label2.ForeColor = Color.White;
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            if (b.turn == player2.team && player2.remaining[1] > 0)
            {
                ResetLabels();
                b.nowMoving = 22;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label4.BackColor = Color.Orange;
                label4.ForeColor = Color.White;
            }
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            if (b.turn == player2.team && player2.remaining[2] > 0)
            {
                ResetLabels();
                b.nowMoving = 32;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label6.BackColor = Color.Orange;
                label6.ForeColor = Color.White;
            }
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            if (b.turn == player2.team && player2.remaining[3] > 0)
            {
                ResetLabels();
                b.nowMoving = 42;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label8.BackColor = Color.Orange;
                label8.ForeColor = Color.White;
            }
        }

        private void ToolStripLabel3_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void ToolStripLabel1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog(this);
            if(f2.DialogResult==DialogResult.OK)
            {
                sizex = f2.sizex;
                sizey = f2.sizey;
                for(int a=0;a<5;a++)
                {
                    initialQuantity[a] =f2.initialQuantity[a];
                }
                inarow=f2.inarow;
                cannibalism = f2.cannibalism;
                Init();
            }
        }

        private void ToolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {
            if (b.turn == player2.team && player2.remaining[4] > 0)
            {
                ResetLabels();
                b.nowMoving = 52;
                b.nowima = new Bitmap((sender as PictureBox).Image);
                label10.BackColor = Color.Orange;
                label10.ForeColor = Color.White;
            }
        }

        
    }
}
