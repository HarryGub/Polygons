using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Многоугольники
{


    public partial class Form1 : Form
    {
        Graphics g;
        bool space;
        int shape, count, j;
        List<Shape> List;
        public Form2 F2;
        BinaryFormatter save;
        Random random;
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".mng";
            openFileDialog1.DefaultExt = ".mng";
            saveFileDialog1.Filter = "Poligons(*.mng)|*.mng";
            openFileDialog1.Filter = "Poligons(*.mng)|*.mng";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            space = false;
            shape = 1;
            List = new List<Shape>();
            F2 = new Form2();
            save = new BinaryFormatter();
            random = new Random();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if (List.Count >= 3)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (i < List.Count - 1)
                    {
                        g.DrawLine(new Pen(Color.Black), List[i].GetX, List[i].GetY, List[i + 1].GetX, List[i + 1].GetY);
                    }
                    else
                    {
                        g.DrawLine(new Pen(Color.Black), List[i].GetX, List[i].GetY, List[0].GetX, List[0].GetY);
                    }
                }
            }
            foreach (Shape a in List)
            {
                a.Draw(g);
            }
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            color.ShowDialog();
            Shape.GetC = color.Color;
            pictureBox1.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Shape elem in List)
            {
                elem.GetX += random.Next(-1, 2);
                elem.GetY += random.Next(-1, 2);
            }
            if (List.Count >= 3)
            { List = Shape.HULL(List); }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (count = List.Count; count > 0; count--)
                {
                    if (List[count - 1].Popaly(e.X, e.Y))
                    {
                        List.RemoveAt(count - 1);
                        count = 0;
                        pictureBox1.Invalidate();
                    }
                }
            }
            if(Form.ActiveForm==F2)
            { F2.Close(); }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                space = true;
                foreach (Shape elem in List)
                {
                    if (elem.Popaly(e.X, e.Y))
                    {
                        space = false;
                        elem.GetF = true;
                        elem.GetX0 = e.X - elem.GetX;
                        elem.GetY0 = e.Y - elem.GetY;
                    }
                }
                if (List.Count >= 3 && Shape.HULLPop(List, e.Location) && space)
                {
                    space = false;
                    foreach (Shape elem in List)
                    {
                        elem.GetF = true;
                        elem.GetX0 = e.X - elem.GetX;
                        elem.GetY0 = e.Y - elem.GetY;
                    }
                }
                if (space)
                {
                    switch (shape)
                    {
                        case 1: List.Add(new Circle(e.X, e.Y)); break;
                        case 2: List.Add(new Triangle(e.X, e.Y)); break;
                        case 3: List.Add(new Square(e.X, e.Y)); break;
                        default:; break;
                    }
                    pictureBox1.Invalidate();
                    foreach (Shape elem in List)
                    {
                        if (elem.Popaly(e.X, e.Y))
                        {
                            space = false;
                            elem.GetF = true;
                            elem.GetX0 = e.X - elem.GetX;
                            elem.GetY0 = e.Y - elem.GetY;
                        }
                    }
                }
                j = 1;
                foreach (Shape elem in List)
                {
                    if (List.Count >= 3 && Shape.HULLPop(List, e.Location) && space)
                    {
                        j++;
                    }
                }
                if (j == List.Count)
                {
                    foreach (Shape elem in List)
                    {
                        elem.GetF = true;
                        elem.GetX0 = e.X - elem.GetX;
                        elem.GetY0 = e.Y - elem.GetY;
                    }
                }
            }
            if (List.Count >= 3)
            { List = Shape.HULL(List); }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Shape elem in List)
            {
                elem.GetF = false;
                space = false;
            }
            if (List.Count >= 3)
            {
                List = Shape.HULL(List);
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].GetF)
                {
                    List[i].GetX = e.X - List[i].GetX0;
                    List[i].GetY = e.Y - List[i].GetY0;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shape = 1;
        }

        private void Radius_Change(object sender, REventArgs e)
        {
            Shape.GetR = e.Radius;
            pictureBox1.Invalidate();
        }

        private void radiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F2.Close();
            F2 = new Form2();
            F2.RChange += Radius_Change;
            F2.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            try
            {
                using (Stream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    save.Serialize(fs, List);
                    save.Serialize(fs, Shape.GetC);
                    save.Serialize(fs, Shape.GetR);
                }
            }
            catch
            { }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            try
            {
                using (Stream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    List = (List<Shape>)save.Deserialize(fs);
                }
                pictureBox1.Invalidate();
            }
            catch
            { }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (List.Count >= 3)
            { List = Shape.HULL(List); }
            pictureBox1.Invalidate();
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shape = 2;
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shape = 3;
        }
    }
    public class REventArgs : EventArgs
    {
        private readonly int radius;
        public REventArgs(int value)
        {
            radius = value;
        }

        public int Radius
        {
            get
            { return radius; }
        }
    }
}