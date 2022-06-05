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
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public Form1()
        {
            InitializeComponent();
            instance = this;
        }

        public class plate
        {
            public int turn = 2;
            int sizex, sizey;
            public int nowMoving = 0;
            public Bitmap nowima;
            public int[,] blocks;
            public PictureBox bg;
            public PictureBox winline;
            public PictureBox[,] plocks;

            public plate(int x,int y)
            {
                sizex = x;
                sizey = y;
                blocks = new int[x, y];
                for(int a=0;a<x;a++)
                {
                    for(int b=0;b<y;b++)
                    {
                        blocks[a, b] = 0;
                    }
                }

                plocks = new PictureBox[x, y];
            }
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
                for (int a = 0; a < sizey; a++)
                {
                    int wteam = blocks[0, a] % 10;
                    if(wteam!=0)
                    {
                        bool win = true;
                        for (int b = 1; b < sizex; b++)
                        {
                            if (blocks[b, a] % 10 != wteam)
                            {
                                win = false;
                            }
                        }
                        if(win)
                        {
                            drawwinline(0, a);
                            return wteam;
                        }
                    }
                }
                for (int a=0;a<sizex;a++)
                {
                    int wteam = blocks[a, 0] % 10;
                    if(wteam!=0)
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
                if(sizex==sizey)
                {
                    int wteam = blocks[0, 0] % 10;
                    if(wteam!=0)
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
        public plate p;
        public int[] initialQuantity;
        public player player1 =new player(1);
        public player player2 = new player(2);
        int winner = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            initialQuantity = new int[5] { 64, 10, 5, 2, 1 };
            Init();
        }

        //initialize
        private void Init()
        {
            winner = 0;
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
                foreach(PictureBox p in g.Controls.OfType<PictureBox>())
                {
                    p.Visible = true;
                }
            }
            ResetLabels();
            SetPlate();
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
        private void SetPlate()
        {
            //remove old plate
            try
            {
                Controls.Remove(p.winline);
            }
            catch { }
            try
            {
                foreach (PictureBox pic in p.plocks)
                {
                    Controls.Remove(pic);
                }
            }
            catch { }
            try
            {
                Controls.Remove(p.bg);
            }
            catch { }

            //set new plate
            p = new plate(sizex, sizex);
            p.bg = new PictureBox();
            p.bg.Size = new Size(360, 360);
            p.bg.Location = new Point(167, 28);
            for (int a = 0; a < sizex; a++)
            {
                for (int b = 0; b < sizex; b++)
                {
                    p.plocks[a, b] = new PictureBox();
                    p.plocks[a, b].Size = new Size((int)Math.Ceiling(((float)p.bg.Size.Width - (5 * (sizex - 1))) / sizex), (int)Math.Ceiling(((float)(p.bg.Size.Height - (5 * (sizey - 1))) / sizey)));
                    p.plocks[a, b].Location = new Point(p.bg.Location.X + (a * 5 + a * p.plocks[a, b].Size.Width), p.bg.Location.Y + (b * 5 + b * p.plocks[a, b].Size.Height));
                    p.plocks[a, b].BackColor = Color.White;
                    p.plocks[a, b].Name = a.ToString() + b.ToString();
                    p.plocks[a, b].SizeMode = PictureBoxSizeMode.Zoom;
                    p.plocks[a, b].Click += Picture_Click;
                }
            }
            foreach (PictureBox pic in p.plocks)
            {
                Controls.Add(pic);
                pic.BringToFront();
            }
            Controls.Add(p.bg);
        }

        private void nextturn()
        {
            winner = p.checkwin();
            if(winner!=0)
            {
                //Controls.Add(p.winline);
                //p.winline.BringToFront();
                MessageBox.Show( $"Player {winner} wins!","Finished!");
            }
            else
            {
                if (p.turn == 1)
                {
                    p.turn = 2;
                    //change background color to orange
                    Bitmap bpm = new Bitmap(p.bg.Width, p.bg.Height);
                    Graphics g = Graphics.FromImage(bpm);
                    g.FillRectangle(Brushes.Orange, 0, 0, p.bg.Width, p.bg.Height);
                    p.bg.Image = bpm;
                }
                else
                {
                    p.turn = 1;
                    //change background color ot blue
                    Bitmap bpm = new Bitmap(p.bg.Width, p.bg.Height);
                    Graphics g = Graphics.FromImage(bpm);
                    g.FillRectangle(Brushes.DodgerBlue, 0, 0, p.bg.Width, p.bg.Height);
                    p.bg.Image = bpm;
                }
            }
        }

        private bool checkrule(string loc)
        {
            //check if the move is within the rule
            if (p.nowMoving / 10 <= p.blocks[loc[0]-'0', loc[1]-'0']/10)
            {
                return false;
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
            if(p.nowMoving!=0)
            {
                string loc = (sender as PictureBox).Name;
                if (checkrule(loc))
                {
                    p.blocks[loc[0] - '0', loc[1] - '0'] = p.nowMoving;
                    (sender as PictureBox).Image = p.nowima;
                    string target = (sender as PictureBox).Name;

                    //take out the used piece
                    if (p.nowMoving % 10 == player1.team)
                    {
                        player1.remaining[p.nowMoving / 10 - 1] -= 1;
                    }
                    else
                    {
                        player2.remaining[p.nowMoving / 10 - 1] -= 1;
                    }
                    p.nowMoving = 0;
                    ResetLabels();
                    nextturn();
                }
            }
        }

        //select a piece
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if(p.turn==player1.team&&player1.remaining[0]>0)
            {
                ResetLabels();
                p.nowMoving = 11;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label1.BackColor = Color.DodgerBlue;
                label1.ForeColor = Color.White;
            }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            if (p.turn == player1.team && player1.remaining[1] > 0)
            {
                ResetLabels();
                p.nowMoving = 21;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label3.BackColor = Color.DodgerBlue;
                label3.ForeColor = Color.White;
            }
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            if (p.turn == player1.team && player1.remaining[2] > 0)
            {
                ResetLabels();
                p.nowMoving = 31;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label5.BackColor = Color.DodgerBlue;
                label5.ForeColor = Color.White;
            }
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            if (p.turn == player1.team && player1.remaining[3] > 0)
            {
                ResetLabels();
                p.nowMoving = 41;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label7.BackColor = Color.DodgerBlue;
                label7.ForeColor = Color.White;
            }
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            if (p.turn == player1.team && player1.remaining[4] > 0)
            {
                ResetLabels();
                p.nowMoving = 51;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label9.BackColor = Color.DodgerBlue;
                label9.ForeColor = Color.White;
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (p.turn == player2.team && player2.remaining[0] > 0)
            {
                ResetLabels();
                p.nowMoving = 12;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label2.BackColor = Color.Orange;
                label2.ForeColor = Color.White;
            }
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            if (p.turn == player2.team && player2.remaining[1] > 0)
            {
                ResetLabels();
                p.nowMoving = 22;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label4.BackColor = Color.Orange;
                label4.ForeColor = Color.White;
            }
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            if (p.turn == player2.team && player2.remaining[2] > 0)
            {
                ResetLabels();
                p.nowMoving = 32;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label6.BackColor = Color.Orange;
                label6.ForeColor = Color.White;
            }
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            if (p.turn == player2.team && player2.remaining[3] > 0)
            {
                ResetLabels();
                p.nowMoving = 42;
                p.nowima = new Bitmap((sender as PictureBox).Image);
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
                Init();
            }
        }

        private void PictureBox10_Click(object sender, EventArgs e)
        {
            if (p.turn == player2.team && player2.remaining[4] > 0)
            {
                ResetLabels();
                p.nowMoving = 52;
                p.nowima = new Bitmap((sender as PictureBox).Image);
                label10.BackColor = Color.Orange;
                label10.ForeColor = Color.White;
            }
        }

        
    }
}
