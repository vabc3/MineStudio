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

        public static bool isSep(this Bitmap data, Point p)
        {
            Color lt = Color.FromArgb(22, 26, 36);
            //33 40 43
            //11,14,26
            Color lt2 = Color.FromArgb(11, 14, 26);
            return Color.Black.Near(data.GetPixel(p)) || lt.Near(data.GetPixel(p)) || lt2.Near(data.GetPixel(p));
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

        private const int MinLineLen = 100;
        private const int LineTolerance = 6;
        private const int Sep = 15;
        public static bool IsOnVerticalLine(this Bitmap data, Point p,bool fullline=true)
        {
            if (!fullline) {
                IList<Point> l1 = MakeLine(p, new Point(p.X, 0));
                int d1 = data.FindEndPoint(l1, 0, LineTolerance);
                IList<Point> l2 = MakeLine(p, new Point(p.X, data.Height - 1));
                int d2 = data.FindEndPoint(l2, 0, LineTolerance);
                //Console.WriteLine("len:{0}", l1[d1].Dist(l2[d2]));
                return (l1[d1].Dist(l2[d2]) > MinLineLen);
            } else {
                for (var i = 1; i < Sep; i++) {
                    Point vp = new Point(p.X,data.Height * i / Sep);
                    if (data.IsOnVerticalLine(vp, false) ) return true;    //&& data.isSep(vp)
                }

                return false;
            }
        }


        public static bool IsOnHorizontalLine(this Bitmap data, Point p,bool fullline=true)
        {
            if (!fullline) {
                IList<Point> l1 = MakeLine(p, new Point(0, p.Y));
                int d1 = data.FindEndPoint(l1, 0, LineTolerance);
                IList<Point> l2 = MakeLine(p, new Point(data.Width - 1, p.Y));
                int d2 = data.FindEndPoint(l2, 0, LineTolerance);
                // Console.WriteLine("len:{0}", l1[d1].Dist(l2[d2]));
                return (l1[d1].Dist(l2[d2]) > MinLineLen);
            } else {
                for (var i = 1; i < Sep; i++) {
                    Point vp = new Point(data.Width*i/Sep, p.Y);
                    if (data.IsOnHorizontalLine(vp, false)) return true;
                }

                return false;
            }
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

        private const double TooNear = 5;
        private static IList<Point> DealNear(IList<Point> list, double near = TooNear)
        {
            
            List<Point> lp=new List<Point>();
            if (list.Count==0) return lp;
            lp.Add(list.First());
            Point o = list.First();
            foreach (var item in list.Skip(1)) {
                if (o.Dist(item) > TooNear)
                    lp.Add(item);
                o=item;
            }

            return lp;
        }

       
        private const int CanSkip = 4;
        public static IList<Point> GetCord(this Bitmap data, IList<Point> list, double per=.6, int skip=CanSkip)
        {

            list = DealNear(list);
            List<Point> cord=new List<Point>();
            int len = list.Count;
            if (len<=1) return cord;

            List<double> l0 = new List<double>();
            for (var i = 1; i < len; i++)
                l0.Add(list[i].Dist(list[i-1]));
            double ave = l0.Average();

            var l1 = (from item in l0
                      where ValueNear(ave, item, .4)
                      select item).ToList();
            ave = l1.Average();

            var l2=(from item in l1
                    where ValueNear(ave, item,.16)
                    select item).ToList();
            if (l2.Count()<9) return cord;

            ave = l2.Average();

            for (var i = 0; i < len; i++) {
                cord.Add(list[i]);
                //int p=i;
                Point p = list[i];
                for (var j = i + 1; j < len; j++) {
                    var dist = list[j].Dist(p);
                    if (ValueNear(ave, dist)) {
                        cord.Add(list[j]);
                        p = list[j];
                    } else if (dist>ave) {
                        for (var k = 2; k <= skip+1; k++)
                            if (ValueNear(ave*k, dist, .05)) {
                                var dx=1.0*(list[j].X-p.X)/k;
                                var dy=1.0*(list[j].Y-p.Y)/k;
                                for (var l = 1; l <= k; l++) {
                                    cord.Add(new Point((int)(p.X+l*dx), (int)(p.Y+l*dy)));
                                }
                                p=cord.Last();
                                break;
                            }
                    }
                }

                if (cord.Count >= list.Count*per)
                    break;
                else
                    cord.Clear();
            }

            return cord;
        }

        public delegate void PointMove(ref Point p);

        public static bool Try(this Bitmap data, Point p, Predicate<Point> pred, PointMove pm, ref int step)
        {
            while (step-- > 0 && !pred(p)) pm(ref p);
            return pred(p);
        }

        public delegate bool PointPut(Bitmap data, ref Point p);

        public static bool SmartPut(this Bitmap data, IList<PointPut> list, ref Point point, int steps=50)
        {
            int os;
            do {
                os=steps;
                foreach (var pu in list) {
                    while (!pu(data, ref point) && steps-- > 0) ;
                }
            } while (os >0 && os != steps);

            return steps >= 0 && os != 0;
        }

        public static Point FindProperPoint(this Bitmap data)
        {
            Point p = new Point(data.Width/2, data.Height/2);

            //p = new Point(640, 399);
            List<PointPut> lp = new List<PointPut>()
                {
                    delegate(Bitmap d1, ref Point p1)
                        {
                            bool hl = !(d1.IsOnHorizontalLine(p1)||d1.IsOnHorizontalLine(new Point(p1.X,p1.Y-1)));
                            if (!hl) p1.Y = p1.Y + 1;
                            return hl;
                        },
                        delegate(Bitmap d1, ref Point p1)
                        {
                            bool hl = !(d1.IsOnHorizontalLine(new Point(p1.X,p1.Y+1)));
                            if (!hl) p1.Y = p1.Y + 1;
                            return hl;
                        },

                        delegate(Bitmap d1, ref Point p1)
                        {
                            bool hl = !(d1.IsOnVerticalLine(p1)||d1.IsOnVerticalLine(new Point(p1.X-1,p1.Y)));
                            if (!hl) p1.X = p1.X + 1;
                            return hl;
                        },

                        delegate(Bitmap d1, ref Point p1)
                        {
                            bool hl = !(d1.IsOnVerticalLine(new Point(p1.X+1,p1.Y)));
                            if (!hl) p1.X = p1.X + 1;
                            return hl;
                        },
                };

            data.SmartPut(lp, ref p, 36);

            //TODO writeMore
            return p;
        }
    }
}