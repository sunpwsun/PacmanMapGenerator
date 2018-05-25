using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman_Map_Generator
{
    public partial class Form1 : Form
    {

        private const int box_len = 25;
        private const int numX = 28;
        private const int numY = 31;
        private int sel_x = 0;
        private int sel_y = 0;
        public static int[,] map;
        public static int[,] path;

        private bool IsMap = true;


        private Image[] img;
        private Image[] dir;



        System.Drawing.Graphics g;
        System.Drawing.Pen p;

        public Form1()
        {
            InitializeComponent();

            /*
             *    : 0  (blank)
             * ⋅  : 1  (small dot)
             * ● : 2  (large dot)
             * 
             * ┏ : 4
             * ┓ : 5
             * ┗ : 6
             * ┛ : 	7
             * ━ : 8
             * ┃ : 9
             * ▣ : 11   (obstacle only for monsters)
            */
            img = new Image[10];

            img[0] = Image.FromFile("0.png");
            img[1] = Image.FromFile("1.png");
            img[2] = Image.FromFile("2.png");
            img[3] = Image.FromFile("0.png");
            img[4] = Image.FromFile("4.png");
            img[5] = Image.FromFile("5.png");
            img[6] = Image.FromFile("6.png");
            img[7] = Image.FromFile("7.png");
            img[8] = Image.FromFile("8.png");
            img[9] = Image.FromFile("9.png");




            // 0 : left
            // 1 : right
            // 2 : up
            // 3 : down  
            dir = new Image[4];
            dir[0] = Image.FromFile("left.png");
            dir[1] = Image.FromFile("right.png");
            dir[2] = Image.FromFile("up.png");
            dir[3] = Image.FromFile("down.png");








            map = new int[numX, numY];
            path = new int[numX, numY];


            //map = new int[numX, numY];
            for (int i = 0; i < numY; i++)
            {
                for (int j = 0; j < numX; j++)
                {
                    map[j, i] = 1;
                    path[j, i] = 1;
                }
            }



            // default map
            map[0, 0] = 4;
            map[10, 12] = 4;
            map[27, 0] = 5;
            map[17, 12] = 5;
            map[0, 30] = 6;
            map[10, 16] = 6;
            map[27, 30] = 7;
            map[17, 16] = 7;
            for( int i = 1; i<27; i++)
            {
                map[i, 0] = 8;
                map[i, 30] = 8;
            }
            for( int i = 1; i < 30; i++)
            {
                map[0, i] = 9;
                map[27, i] = 9;
            }
            for (int i = 13; i < 16; i++)
            {
                map[10, i] = 9;
                map[17, i] = 9;
            }
            for (int i = 11; i < 17; i++)
            {
                map[i,16] = 8;        
            }

            map[11,12] = 8;
            map[12, 12] = 8;
            map[15, 12] = 8;
            map[16, 12] = 8;




            // default path

            // right side
            for (int i = 14; i < 27; i++)
                for (int j = 1; j < 30; j++)
                    path[i, j] = 0;


            path[0, 0] = 4;
            path[10, 12] = 4;
            path[27, 0] = 5;
            path[17, 12] = 5;
            path[0, 30] = 6;
            path[10, 16] = 6;
            path[27, 30] = 7;
            path[17, 16] = 7;
            for (int i = 1; i < 27; i++)
            {
                path[i, 0] = 8;
                path[i, 30] = 8;
            }
            for (int i = 1; i < 30; i++)
            {
                path[0, i] = 9;
                path[27, i] = 9;
            }
            for (int i = 13; i < 16; i++)
            {
                path[10, i] = 9;
                path[17, i] = 9;
            }
            for (int i = 11; i < 17; i++)
            {
                path[i, 16] = 8;
            }

            path[11, 12] = 8;
            path[12, 12] = 8;
            path[15, 12] = 8;
            path[16, 12] = 8;




            g = this.CreateGraphics();
            p = new System.Drawing.Pen(System.Drawing.Color.Red);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            




            for (int y = 0; y < numY; y++)
            {
                for (int x = 0; x < numX; x++)
                {

                    e.Graphics.DrawImage(img[map[x, y]], new PointF(box_len * (x + 1), box_len * (y + 1)));

                }
            }

            for (int i = 0; i <= numY; i++)
            {
                g.DrawLine(p, box_len, box_len + (float)(i * box_len), box_len * (numX + 1), box_len + (float)(i * box_len));
            }

            for (int i = 0; i <= numX; i++)
            {
                g.DrawLine(p, box_len + (float)(i * box_len), box_len, box_len + (float)(i * box_len), box_len * (numY + 1));
            }


           // p.Dispose();
          //  g.Dispose();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (e.X - 26) / box_len;
            int y = (e.Y - 26) / box_len;


            if (x >= numX || y >= numY || x < 0 || y < 0)
                return;

            sel_x = x;
            sel_y = y;

            if (IsMap)
            {

                if (++map[x, y] > 9)
                    map[x, y] = 0;


                if (map[x, y] > 3)
                    path[x, y] = map[x, y];
                else
                    path[x, y] = 0;

                this.Invalidate();

            }
            else
            {
                if (path[sel_x, sel_y] > 3)
                    return;
               // MessageBox.Show("X=" + sel_x);
                if (++path[x, y] > 3)
                    path[x, y] = 0;

                this.Invalidate();
            }

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.Red);




            for (int y = 0; y < numY; y++)
            {
                for (int x = 0; x < numX; x++)
                {

                    if( IsMap )
                        e.Graphics.DrawImage(img[map[x, y]], new PointF(box_len * (x + 1), box_len * (y + 1)));
                    else
                    {

                        if(path[x, y] > 3)
                        {
                            e.Graphics.DrawImage(img[path[x, y]], new PointF(box_len * (x + 1), box_len * (y + 1)));
                        }
                        else
                        {
                            e.Graphics.DrawImage(dir[path[x, y]], new PointF(box_len * (x + 1), box_len * (y + 1)));
                        }
                    }

                }
            }





            for (int i = 0; i <= numY; i++)
            {
                g.DrawLine(p, box_len, box_len + (float)(i * box_len), box_len * (numX + 1), box_len + (float)(i * box_len));
            }

            for (int i = 0; i <= numX; i++)
            {
                g.DrawLine(p, box_len + (float)(i * box_len), box_len, box_len + (float)(i * box_len), box_len * (numY + 1));
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsMap)
            {
                try
                {
                    int num = int.Parse(e.KeyChar.ToString());

                    if (num >= 0 && num <= 9)
                    {
                        map[sel_x, sel_y] = num;



                        if (map[sel_x, sel_y] > 3)
                            path[sel_x, sel_y] = map[sel_x, sel_y];


                        this.Invalidate();
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            else
            {
                if (path[sel_x, sel_y] > 3)
                    return;

                try
                {
                    int num = int.Parse(e.KeyChar.ToString());

                    if (num >= 0 && num <= 3)
                    {
                        path[sel_x, sel_y] = num;

                        
                    }
                    else
                        path[sel_x, sel_y] = 0;


                    this.Invalidate();

                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Map mapForm = new Map();
            mapForm.ShowDialog();
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Map Files(*.map) | *.map";
            //open.InitialDirectory = @"c:\";
            open.Title = "Choose the map file";

            if (open.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(open.FileName))
                {
                    int index = 0;
                    while (!sr.EndOfStream)
                    {
                        int val = int.Parse(sr.ReadLine());

                        if (index < numX * numY)
                        {     
                            Form1.map[index % 28, index / 28] = val;
                        }
                        else
                            Form1.path[(index - numX * numY ) % 28, (index - numX * numY) / 28] = val;



                        index++;
                    }

                    
                }

            }

            this.Invalidate();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            IsMap = !IsMap;

            if(IsMap)
                label1.Text = "Map";
            else
                label1.Text = "Return Path";


            this.Invalidate();
        }
    }
}
