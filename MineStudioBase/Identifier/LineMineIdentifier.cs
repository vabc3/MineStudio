using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MineStudio.Identifier
{
    class LineMineIdentifier : IMineIdentifier
    {
        class Line
        {
            public Point P1 { get; set; }
            public Point P2 { get; set; }
            public double Len { get; set; }
            public Color Color { get; set; }
        }

        private delegate bool ActAndPred(ref Point pt);

        private Line GetLongest(Bitmap pic, Point start, ActAndPred pred)
        {
            Point p1=start;
            Point p2=p1;
            Color c = pic.GetPixel(p1.X, p1.Y);
            Line line = new Line { P1 = p1, P2=p2, Len=p1.Dist(p2), Color=c };
            Point p3=p1;
            while (pred(ref p3))
            {
                Color c3 = pic.GetPixel(p3.X, p3.Y);
                if (!c3.Near(c))
                {
                    double l = p1.Dist(p2);
                    if (l > line.Len)
                    {
                        line.P1=p1;
                        line.P2=p2;
                        line.Len=l;
                        line.Color=c;
                    }
                    p1=p3;
                }
                c=c3;
                p2=p3;
            }

            return line;
        }


        public bool GetTableInfo(System.Drawing.Bitmap data, out int width, out int height, out int ileft, out int itop, out int iwidth, out int iheight)
        {
            width = height = ileft = itop = iwidth = iheight = 0;
            int dwidth = data.Width;
            int dheight = data.Height;

            List<Line> ll = new List<Line>();
            for (var i = 0; i < dwidth; i++)
            {
                Line a = GetLongest(data, new Point(i, 0), (ref Point p) => ++p.Y < dheight);
                ll.Add(a);
                Console.WriteLine("{0}:len->{1}", i, a.Len);
            }
            var list =from item in ll where item.Color.Near(Color.Black) select item;
            foreach (var item in list)
            {
                data.SetPixel(item.P1.X, item.P1.Y, Color.Blue);
                data.SetPixel(item.P2.X, item.P2.Y, Color.Red);
            }

            data.Save("qq.png");
            return true;
        }
        
        public bool GetCellInfo(Bitmap data, out CellStatus status, out int n)
        {
            throw new NotImplementedException();
        }
    }
}
