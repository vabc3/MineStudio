using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MineStudio
{
    interface IMineIdentifier
    {
        bool GetTableInfo(Bitmap data, out int width, out int height);
        bool GetCellInfo(Bitmap data, out CellStatus status, int n);
    }

    public class W7MineIdentifier : IMineIdentifier
    {

        private static bool ColorNear(Color a, Color b)
        {
            int gap = 15;
            return (Math.Abs(a.R - b.R) < gap && Math.Abs(a.G - b.G) < gap && Math.Abs(a.B - b.B) < gap);
        }

        private static bool ValueNear(double target, double value, double percent=.11)
        {
            return Math.Abs((target - value)/target) < percent;
        }

        private delegate void PointDealer(ref Point point);

        private static List<Point> FindMutation(Bitmap data, Point startPoint, PointDealer pd, Predicate<Point> pred)
        {
            List<Point> rt = new List<Point>();

            Point p = startPoint;
            if (pred(p))
            {
                Color c = data.GetPixel(p.X, p.Y);
                Color oldC = c;
                pd(ref p);
                while (pred(p))
                {
                    c = data.GetPixel(p.X, p.Y);
                    if (!ColorNear(oldC, c))
                    {
                        rt.Add(p);
                        // data.SetPixel(p.X, p.Y, Color.Red);
                    }
                    oldC = c;
                    pd(ref p);
                }
            }

            return rt;
        }

        private int Pgap(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public bool GetTableInfo(Bitmap data, out int width, out int height)
        {
            int iwidth = data.Width;
            int iheight = data.Height;
            height = -1;
            width = -1;
            List<int> toj = new List<int>();
            for (var i = iwidth/2; i < iwidth; i+=3)
            {
                var l1=FindMutation(data, new Point(i, 0), (ref Point point) => { point.Y++; }, (Point t) => t.Y < iheight);
                var l2=from item in l1 where ColorNear(Color.Black, data.GetPixel(item.X, item.Y)) select item;
                if (l2.Count() > 9)
                {
                    List<int> gaps = new List<int>();
                    var pre = l2.First();

                    foreach (var item in l2.Skip(1))
                    {
                        gaps.Add(Pgap(pre, item));
                        pre=item;
                    }
                    var pj = gaps.Average();
                    var gap2=from item in gaps where ValueNear(pj, item) select item;
                    if (gap2.Count()<9) continue;

                    toj.Add(gap2.Count());
                    //Console.WriteLine("{0}->{1}", i, gap2.Count());
                }
            }

            List<int> toy = new List<int>();
            for (var i = iheight/2; i < iheight; i+=2)
            {
                var l1=FindMutation(data, new Point(0, i), (ref Point point) => { point.X++; }, (Point t) => t.X < iwidth);
                var l2=from item in l1 where ColorNear(Color.Black, data.GetPixel(item.X, item.Y)) select item;
                if (l2.Count() > 9)
                {
                    List<int> gaps = new List<int>();
                    var pre = l2.First();

                    foreach (var item in l2.Skip(1))
                    {
                        gaps.Add(Pgap(pre, item));
                        pre=item;
                    }
                    var pj = gaps.Average();
                    var gap2=from item in gaps where ValueNear(pj, item) select item;
                    if (gap2.Count()<9) continue;

                    toy.Add(gap2.Count());
                    Console.WriteLine("{0}->{1}", i, gap2.Count());
                }
            }

            if (!toj.Any())
                return false;
            var r1 = toj.GroupBy(i => i).OrderBy(x => x.Count()).Reverse();
            height = r1.First().Key;
            if (!toy.Any())
                return false;
            var r2 = toy.GroupBy(i => i).OrderBy(x => x.Count()).Reverse();
            width = r2.First().Key;
            return true;
        }

        public bool GetCellInfo(Bitmap data, out CellStatus status, int n)
        {
            status = CellStatus.Ground;
            n = 6;
            return true;
        }
    }
}
