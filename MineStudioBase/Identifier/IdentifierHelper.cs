using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace MineStudio.Identifier
{
    public static class IdentifierHelper
    {

        public static Color GetPixel(this Bitmap data, Point p)
        {
            return data.GetPixel(p.X, p.Y);
        }

        public static bool isSep(this Bitmap data,Point p)
        {
            Color lt = Color.FromArgb(22, 26, 36);
            //33 40 43
            return Color.Black.Near(data.GetPixel(p)) || lt.Near(data.GetPixel(p));
        }

        private const int Gap = 32;
        public static bool Near(this Color a, Color b)
        {
            //Console.WriteLine("Compare {0} with {1}", a, b);

            return (Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) +Math.Abs(a.B - b.B) <= Gap);
        }

        public static double Dist(this Point a, Point b)
        {
            if (a.X == b.X) return Math.Abs(a.Y - b.Y);
            if (a.Y == b.Y) return Math.Abs(a.X - b.X);
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }


        public static void DrawPoints(this Bitmap data, IEnumerable<Point> list, Color color)
        {
            foreach (var item in list)
                data.SetPixel(item.X, item.Y, color);
        }


        public static void DrawLine(this Bitmap data, Point a, Point b, Color color)
        {
            data.DrawPoints(MakeLine(a, b), color);
        }

        public static IList<Point> MakeLine(Point a, Point b)
        {
            List<Point> rt = new List<Point>();
            if (a.X == b.X) {
                if (a.Y==b.Y) {
                    rt.Add(a);
                } else if (b.Y > a.Y)
                    for (var i = a.Y; i <= b.Y; i++)
                        rt.Add(new Point(a.X, i));
                else for (var i = a.Y; i >= b.Y; i--)
                        rt.Add(new Point(a.X, i));

            } else {
                double k = 1.0*(b.Y - a.Y)/(b.X - a.X);
                if (k>=-1 && k<=1) {
                    if (b.X>a.X)
                        for (var i = a.X; i <= b.X; i++)
                            rt.Add(new Point(i, a.Y+(int)((i-a.X)*k)));
                    else
                        for (var i = a.X; i >= b.X; i--)
                            rt.Add(new Point(i, a.Y+(int)((i-a.X)*k)));
                } else {
                    if (b.Y>a.Y)
                        for (var i = a.Y; i <= b.Y; i++)
                            rt.Add(new Point((int)(a.X+(i-a.Y)/k), i));
                    else
                        for (var i = a.Y; i >= b.Y; i--)
                            rt.Add(new Point((int)(a.X+(i-a.Y)/k), i));
                }
            }
            return rt;
        }

        public static int FindEndPoint(this Bitmap data, IList<Point> list, int idx, int tolerance=5)
        {
            Point p = list[idx];
            Color o = data.GetPixel(p.X, p.Y);
            int len = list.Count;
            int u=++idx;
            int t = 0;
            int m=u;
            while (u < len) {
                p = list[u];
                Color c = data.GetPixel(p.X, p.Y);
                if (!c.Near(o)) {
                    if (t == 0) m=u;
                    if (t >= tolerance) {
                        u = m;
                        break;
                    }
                    t++;
                } else {
                    t = 0;
                    o = c;
                }
                u++;
            }

            return u-1;
        }

        private const int MinLineLen = 200;
        private const int LineTolerance = 6;
        public static bool IsOnVerticalLine(this Bitmap data, Point p)
        {
            IList<Point> l1 = MakeLine(p, new Point(p.X, 0));
            int d1=data.FindEndPoint(l1, 0,LineTolerance);
            IList<Point> l2 = MakeLine(p, new Point(p.X, data.Height-1));
            int d2=data.FindEndPoint(l2, 0,LineTolerance);
            Console.WriteLine("len:{0}", l1[d1].Dist(l2[d2]));
            return (l1[d1].Dist(l2[d2]) > MinLineLen);
        }

        private const int FeaturedTolerance = 1;
        public static IList<Point> FindFeaturedPoints(this Bitmap data, IList<Point> list)
        {
            List<Point> l = new List<Point>();
            int len = list.Count;
            int p = 0;
            int q=data.FindEndPoint(list, 0, FeaturedTolerance);
            while (q < len - 1) {
                p = q + 1;
                l.Add(list[p]);
                q=data.FindEndPoint(list, p, FeaturedTolerance);
            }
            if (p == len - 1) l.Add(list[len-1]);

            return l;
        }
       
        private static bool ValueNear(double a, double b, double per = .11)
        {
            return Math.Abs((a - b)/a) < per;
        }

        public static IList<Point> GetCord(this Bitmap data, IList<Point> list,double per=.5)
        {
            List<Point> cord=new List<Point>();
            int len = list.Count;
            if (len<=1) return cord;
            List<double> li = new List<double>();
            for (var i = 1; i < len; i++) 
                li.Add(list[i].Dist(list[i-1]));

            double ave = li.Average();
            var l2=from item in li where ValueNear(ave,item) select item ;
            if (l2.Count()<9) return cord;

            ave = l2.Average();

            for (var i = 0; i < len; i++) {
                cord.Add(list[i]);
                int p=i;
                for (var j = i + 1; j < len; j++) {
                    if (ValueNear(ave, list[j].Dist(list[p]))) {
                        cord.Add(list[j]);
                        p=j;
                    }
                }

                if (cord.Count >= list.Count*per)
                    break;
                else
                    cord.Clear();
            }

            return cord;
        }
    }
}