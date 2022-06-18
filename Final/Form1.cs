using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final

{
    public partial class Manager : System.Windows.Forms.Form
    {

        class Figura
        {
            public Figura()
            {

            }

            public virtual void daLungime()
            {

            }

            public virtual void Deseneaza(Image g)
            {

            }
        }

        class Elipsa : Figura
        {
            int x;
            int y;
            int height;
            int width;
            //x,y sunt coordonatele centrului;
            public Elipsa()

            {

            }
            public Elipsa(int x, int y, int height, int width)
            {
                this.x = x;
                this.y = y;
                this.height = height;
                this.width = width;
            }     

            public override void daLungime()
            {

            }

            public override void Deseneaza(Image g)
            {
                using Graphics h = Graphics.FromImage(g);
                h.DrawEllipse(new Pen(Color.Black, 3), x, y, height, width);
            }

        }

        class Dreptunghi : Figura
        {
            int x;
            int y;
            int height;
            int width;
            //x,y sunt coordonatele centrului;
            public Dreptunghi()

            {

            }
            public Dreptunghi(int x, int y, int height, int width)
            {
                this.x = x;
                this.y = y;
                this.height = height;
                this.width = width;
            }

           
          public override void daLungime()
            {
                MessageBox.Show("Permitetrul este :"+this.height * this.width);
            }

            public override void Deseneaza(Image g)
            {
                using Graphics h = Graphics.FromImage(g);
                h.DrawRectangle(new Pen(Color.Black, 3), x, y, height, width);
            }

        }

        class Linie: Figura
        {
            int x1,x2;
            int y1,y2;

           
            public Linie()

            {

            }
            public Linie(int x1,int y1,int x2,int y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }


            public override void daLungime()
            {
                MessageBox.Show("Lungimea este : " + Math.Sqrt(Math.Abs((x2-x1)*(x2-x1)-(y2-y1)*(y2-y1))));
            }

            public override void Deseneaza(Image g)
            {
                using Graphics h = Graphics.FromImage(g);
                h.DrawLine(new Pen(Color.Black, 3),  x1, y1, x2,y2) ; 
            }

        }

        class Bezier: Figura
        {
            Point p1 = new Point();
            Point p2 = new Point();
            Point p3 = new Point();
            Point p4 = new Point();

            
            public Bezier()
            {

            }
            
            public Bezier(Point p1, Point p2, Point p3, Point p4)
            {
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;
                this.p4 = p4;

            }


            public override void daLungime()
            {
                MessageBox.Show("Lungimea este : ....");
            }

            public override void Deseneaza(Image g)
            {
                using Graphics h = Graphics.FromImage(g);
                h.DrawBezier(new Pen(Color.Black, 3),p1,p2,p3,p4);
            }

        }


        public Manager()
        {
            InitializeComponent();
        }
        Bitmap bmp;
        Graphics g;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void FloodFill(Bitmap bmp, Point pt  ,Color replacementColor)
        {
           Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }

            Stack<Point> pixels = new Stack<Point>();

            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                int y1 = temp.Y;
                while (y1 >= 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);

                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }
               
            }
            Poza.Refresh();

        }
       
        static int k = 0;
       
        private void button1_Click(object sender, EventArgs e)
        { 
            bmp = new Bitmap(Poza.Width, Poza.Height);
            g = Graphics.FromImage(bmp);

            g.Clear(Color.White);
            Poza.Image = bmp.Clone(new Rectangle(0, 0, Poza.Width, Poza.Height), System.Drawing.Imaging.PixelFormat.DontCare);

           

            if(checkBox1.Checked)
            for (int i = 1; i <= 17; i++)
            {
                Random rnd = new Random();
                int r1 = rnd.Next(0, 300);
                int r2 = rnd.Next(0, 300);
                int H = rnd.Next(50, 200);
                int W = rnd.Next(50, 150);

                Elipsa a = new Elipsa(r1, r2, H, W);
                a.Deseneaza(Poza.Image);
            }
            
            if (checkBox2.Checked)
                for (int i = 1; i <= 17; i++)
                {
                    Random rnd = new Random();
                    int r1 = rnd.Next(0, 300);
                    int r2 = rnd.Next(0, 300);
                    int H = rnd.Next(50, 200);
                    int W = rnd.Next(50, 200);

                    Dreptunghi a = new Dreptunghi(r1, r2, H, W);
                    a.Deseneaza(Poza.Image);
                }

            if (checkBox3.Checked)
                for (int i = 1; i <= 17; i++)
                {
                    Random rnd = new Random();
                    int x1 = rnd.Next(0, 400);
                    int y1 = rnd.Next(0, 400);
                    int x2= rnd.Next(0, 400);
                    int y2 = rnd.Next(0, 400);

                    Linie a = new Linie(x1,y1,x2,y2);
                    a.Deseneaza(Poza.Image);
                }

            if (checkBox10.Checked)
                for (int i = 1; i <= 17; i++)
                {
                    Random rnd = new Random();
                    int x1= rnd.Next(0, 300);
                    int x2 = rnd.Next(0, 300);
                    int x3 = rnd.Next(50, 600);
                    int x4 = rnd.Next(50, 600);
                    int y1 = rnd.Next(0, 300);
                    int y2 = rnd.Next(0, 300);
                    int y3 = rnd.Next(50, 600);
                    int y4 = rnd.Next(50, 600);

                    Point p1 = new Point(x1, y1);
                    Point p2 = new Point(x2, y2);
                    Point p3 = new Point(x3, y3);
                    Point p4 = new Point(x4, y4);

                    Bezier a = new Bezier(p1,p2,p3,p4);

                    a.Deseneaza(Poza.Image);
                }

            Poza.Image.Save(@"C:\Final\Final\Generator_History\Image" + k + ".jpg");   
        }
        
        public bool Verific()
        {
            int k = 0;
            if (checkBox4.Checked) k++;
            if (checkBox5.Checked) k++;
            if (checkBox6.Checked) k++;
            if (checkBox7.Checked) k++;
            if (checkBox8.Checked) k++;
            if (checkBox9.Checked) k++;
            if (k != 1) return false;
            return true;
        }


        private void Poza_Click(object sender, EventArgs e)
        { 
            
             bmp = new Bitmap(@"C:\Final\Final\Generator_History\Image" + k + ".jpg");

            k++;


                g = Graphics.FromImage(bmp);
                Poza.Image = bmp.Clone(new Rectangle(0, 0, Poza.Width, Poza.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                g.DrawImage(bmp, 0, 0);




                var mouseEventArgs = e as MouseEventArgs;
                int x = mouseEventArgs.X;
                int y = mouseEventArgs.Y;

                Point P = new Point(x, y);
                Color C = new Color();
            C = Color.White;
            if (checkBox4.Checked) C = Color.Red;
            if (checkBox5.Checked) C = Color.Green;
            if (checkBox6.Checked) C = Color.Yellow;
            if (checkBox7.Checked) C = Color.Blue;
            if (checkBox8.Checked) C = Color.Orange;
            if (checkBox9.Checked) C = Color.Purple;

            if (!Verific()) MessageBox.Show("Atentie la selectarea culorilor ! ");

            FloodFill(bmp, P, C);

                g = Graphics.FromImage(bmp);
                Poza.Image = bmp.Clone(new Rectangle(0, 0, Poza.Width, Poza.Height), System.Drawing.Imaging.PixelFormat.DontCare);
                g.DrawImage(bmp, 0, 0);
                Poza.Image.Save(@"C:\Final\Final\Generator_History\Image" + k + ".jpg");
        }

        private void Poza_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawImage(bmp, 0, 0);
        }
       
        void salveaza()
        {
              
            
           
            g.DrawImage(bmp, 0, 0, Poza.Width, Poza.Height);

            try
            {
                sf.DefaultExt = "png";
                sf.FileName = "Figura.png";
                sf.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
                sf.FilterIndex = 1;
                sf.RestoreDirectory = true;
                sf.ShowDialog();
                bmp.Save(sf.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            salveaza();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Imaginea se printeza ! ...");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Just a Happy Face ");
        }
    }

    }

