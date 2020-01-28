using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Многоугольники
{
    [Serializable]

    abstract class Shape
    {
        static protected int r;
        static protected Color color;
        protected int x;
        protected int y;
        protected int x0;
        protected int y0;
        protected bool flag, hull;
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            flag = false;
            hull = false;
        }
        static Shape()
        {
            r = 20;
            color = Color.Green;
        }
        public abstract void Draw(Graphics g);
        public abstract bool Popaly(int mousex, int mousey);
        static public List<Shape> HULL(List<Shape> List)
        {
            Shape mbo;
            int x = 0;
            int y = 0;
            int left = 0;
            double oldcos = 2;
            double cos = 0;
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].GetX < List[left].GetX)
                { left = i; }
            }
            List.Add(List[left]);
            mbo = List[left];
            List.RemoveAt(left);
            List[List.Count - 1].GetH = true;
            int count = List.Count - 1;
            x = List[0].GetX;
            y = List[0].GetY;
            for (int i = 0; i <= count; i++)
            {
                cos = ((x - List[count].GetX) * (List[i].GetX - List[count].GetX) + (y - List[count].GetY) * (List[i].GetY - List[count].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(x - List[count].GetX, 2) + Math.Pow(y - List[count].GetY, 2))) * Math.Sqrt(Math.Pow(List[i].GetX - List[count].GetX, 2) + Math.Pow(List[i].GetY - List[count].GetY, 2)));
                if (cos < oldcos)
                {
                    oldcos = cos;
                    left = i;
                }
            }
            List.Add(List[left]);
            List.RemoveAt(left);
            List[List.Count - 1].GetH = true;
            count--;
            do
            {
                oldcos = 2;
                for (int i = 0; i < List.Count; i++)
                {
                    cos = ((List[List.Count - 2].GetX - List[List.Count - 1].GetX) * (List[i].GetX - List[List.Count - 1].GetX) + (List[List.Count - 2].GetY - List[List.Count - 1].GetY) * (List[i].GetY - List[List.Count - 1].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(List[List.Count - 2].GetX - List[List.Count - 1].GetX, 2) + Math.Pow(List[List.Count - 2].GetY - List[List.Count - 1].GetY, 2))) * Math.Sqrt(Math.Pow(List[i].GetX - List[List.Count - 1].GetX, 2) + Math.Pow(List[i].GetY - List[List.Count - 1].GetY, 2)));
                    if (cos < oldcos)
                    {
                        oldcos = cos;
                        left = i;
                    }
                }
                List.Add(List[left]);
                List.RemoveAt(left);
                List[List.Count - 1].GetH = true;
                count--;
            }
            while (List[List.Count - 1] != mbo);
            List.RemoveRange(0, count + 1);
            return List;
        }
        public static bool HULLPop(List<Shape> List, Point mouse)
        {
            bool p = true;
            int i = 0;
            double cos, cos1;
            cos = ((List[1].GetX - List[0].GetX) * (List[List.Count - 1].GetX - List[0].GetX) + (List[1].GetY - List[0].GetY) * (List[List.Count - 1].GetY - List[0].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(List[1].GetX - List[0].GetX, 2) + Math.Pow(List[1].GetY - List[0].GetY, 2))) * Math.Sqrt(Math.Pow(List[List.Count - 1].GetX - List[0].GetX, 2) + Math.Pow(List[List.Count - 1].GetY - List[0].GetY, 2)));
            cos1 = ((mouse.X - List[0].GetX) * (List[List.Count - 1].GetX - List[0].GetX) + (mouse.Y - List[0].GetY) * (List[List.Count - 1].GetY - List[0].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(mouse.X - List[0].GetX, 2) + Math.Pow(mouse.Y - List[0].GetY, 2))) * Math.Sqrt(Math.Pow(List[List.Count - 1].GetX - List[0].GetX, 2) + Math.Pow(List[List.Count - 1].GetY - List[0].GetY, 2)));
            if (cos < cos1)
            {
                cos = ((List[0].GetX - List[List.Count - 1].GetX) * (List[List.Count - 2].GetX - List[List.Count - 1].GetX) + (List[0].GetY - List[List.Count - 1].GetY) * (List[List.Count - 2].GetY - List[List.Count - 1].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(List[0].GetY - List[List.Count - 1].GetX, 2) + Math.Pow(List[0].GetY - List[List.Count - 1].GetY, 2))) * Math.Sqrt(Math.Pow(List[List.Count - 2].GetX - List[List.Count - 1].GetX, 2) + Math.Pow(List[List.Count - 2].GetY - List[List.Count - 1].GetY, 2)));
                cos1 = ((mouse.X - List[List.Count - 1].GetX) * (List[List.Count - 2].GetX - List[List.Count - 1].GetX) + (mouse.Y - List[List.Count - 1].GetY) * (List[List.Count - 2].GetY - List[List.Count - 1].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(mouse.X - List[List.Count - 1].GetX, 2) + Math.Pow(mouse.Y - List[List.Count - 1].GetY, 2))) * Math.Sqrt(Math.Pow(List[List.Count - 2].GetX - List[List.Count - 1].GetX, 2) + Math.Pow(List[List.Count - 2].GetY - List[List.Count - 1].GetY, 2)));
                if (cos < cos1)
                {
                    do
                    {
                        i++;
                        cos = ((List[i + 1].GetX - List[i].GetX) * (List[i - 1].GetX - List[i].GetX) + (List[i + 1].GetY - List[i].GetY) * (List[i - 1].GetY - List[i].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(List[i + 1].GetX - List[i].GetX, 2) + Math.Pow(List[i + 1].GetY - List[i].GetY, 2))) * Math.Sqrt(Math.Pow(List[i - 1].GetX - List[i].GetX, 2) + Math.Pow(List[i - 1].GetY - List[i].GetY, 2)));
                        cos1 = ((mouse.X - List[i].GetX) * (List[i - 1].GetX - List[i].GetX) + (mouse.Y - List[i].GetY) * (List[i - 1].GetY - List[i].GetY)) / 1.0 / (Math.Sqrt((Math.Pow(mouse.X - List[i].GetX, 2) + Math.Pow(mouse.Y - List[i].GetY, 2))) * Math.Sqrt(Math.Pow(List[i - 1].GetX - List[i].GetX, 2) + Math.Pow(List[i - 1].GetY - List[i].GetY, 2)));
                        if (cos1 < cos)
                        {
                            p = false;
                            break;
                        }
                    }
                    while (i != List.Count - 2);
                }
                else
                { p = false; }
            }
            else
            { p = false; }
            return p;
        }
        public abstract int GetX
        {
            get;
            set;
        }
        public abstract int GetY
        {
            get;
            set;
        }
        public static int GetR
        {
            get
            { return r; }
            set
            { r = value; }
        }
        public static Color GetC
        {
            get
            { return color; }
            set
            { color = value; }
        }
        public abstract int GetX0
        {
            get;
            set;
        }
        public abstract int GetY0
        {
            get;
            set;
        }
        public abstract bool GetF
        {
            get;
            set;
        }
        public abstract bool GetH
        {
            get;
            set;
        }
    }

    [Serializable]

    class Circle : Shape
    {
        public Circle(int x, int y) : base(x, y)
        { }
        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), x - r, y - r, 2 * r, 2 * r);
        }
        public override bool Popaly(int mousex, int mousey)
        {
            if (((mousex - x) * (mousex - x) + (mousey - y) * (mousey - y)) <= r * r)
            { return true; }
            else
            { return false; }
        }
        public override int GetX
        {
            get { return x; }
            set { x = value; }
        }
        public override int GetY
        {
            get { return y; }
            set { y = value; }
        }
        public override int GetX0
        {
            get { return x0; }
            set { x0 = value; }
        }
        public override int GetY0
        {
            get { return y0; }
            set { y0 = value; }
        }
        public override bool GetF
        {
            get { return flag; }
            set {flag = value; }
        }
        public override bool GetH
        {
            get { return hull; }
            set { hull = value; }
        }
    }

    [Serializable]

    class Triangle : Shape
    {
        Point[] Points;
        public Triangle(int x, int y) : base(x, y)
        {
            Points = new Point[3]
            {
                new Point(x, y - r),
                new Point((int)(x  + r * Math.Sqrt(3)/2), y + r/2),
                new Point((int)(x - r * Math.Sqrt(3)/2), y + r/2),
            };
        }
        public override void Draw(Graphics g)
        {
            g.FillPolygon(new SolidBrush(color), Points = new Point[3]
            {
                new Point(x, y - r),
                new Point((int)(x  + r * Math.Sqrt(3)/2), y + r/2),
                new Point((int)(x - r * Math.Sqrt(3)/2), y + r/2),
            });
        }
        public override bool Popaly(int mousex, int mousey)
        {
            int a = (Points[0].X - mousex) * (Points[1].Y - Points[0].Y) - (Points[1].X - Points[0].X) * (Points[0].Y - mousey);
            int b = (Points[1].X - mousex) * (Points[2].Y - Points[1].Y) - (Points[2].X - Points[1].X) * (Points[1].Y - mousey);
            int c = (Points[2].X - mousex) * (Points[0].Y - Points[2].Y) - (Points[0].X - Points[2].X) * (Points[2].Y - mousey);

            if ((a >= 0 && b >= 0 && c >= 0) || (a <= 0 && b <= 0 && c <= 0))
            { return true; }
            else
            { return false; }
        }
        public override int GetX
        {
            get { return x; }
            set { x = value; }
        }
        public override int GetY
        {
            get { return y; }
            set { y = value; }
        }
        public override int GetX0
        {
            get { return x0; }
            set { x0 = value; }
        }
        public override int GetY0
        {
            get { return y0; }
            set { y0 = value; }
        }
        public override bool GetF
        {
            get { return flag; }
            set { flag = value; }
        }
        public override bool GetH
        {
            get { return hull; }
            set { hull = value; }
        }
    }

    [Serializable]

    class Square : Shape
    {
        public Square(int x, int y) : base(x, y)
        {

        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), new Rectangle((int)(x - (r / Math.Sqrt(2))), (int)(y - (r / Math.Sqrt(2))), (int)(2 * (r / Math.Sqrt(2))), (int)(2 * (r / Math.Sqrt(2)))));
        }
        public override bool Popaly(int mousex, int mousey)
        {
            if (mousex <= x + (r / Math.Sqrt(2)) && mousey <= y + (r / Math.Sqrt(2)) && mousex >= x - (r / Math.Sqrt(2)) && mousey >= y - (r / Math.Sqrt(2)))
            { return true; }
            else
            { return false; }
        }
        public override int GetX
        {
            get { return x; }
            set { x = value; }
        }
        public override int GetY
        {
            get { return y; }
            set { y = value; }
        }
        public override int GetX0
        {
            get { return x0; }
            set { x0 = value; }
        }
        public override int GetY0
        {
            get { return y0; }
            set { y0 = value; }
        }
        public override bool GetF
        {
            get { return flag; }
            set { flag = value; }
        }
        public override bool GetH
        {
            get { return hull; }
            set { hull = value; }
        }
    }
}
