using System;
using System.Collections.Generic;
using System.Drawing;
using MineStudio.Identifier;
using System.Linq;

namespace MineStudio
{
    class Program
    {
        private const String Prefix = @"..\..\..\MineStudioTest\ImageCases\";

        private static List<int> r = new List<int>();
        private static List<int> g = new List<int>();
        private static List<int> b = new List<int>();
        private static void tongji(Color c)
        {
            if (c.R < 50) r.Add(c.R);
            if (c.G < 50) g.Add(c.G);
            if (c.B < 50) b.Add(c.B);
        }

        static void Main(string[] args)
        {
            IMineIdentifier target = MineIdentifierFactory.GetDefaultIdentifier();
            Bitmap data = new Bitmap(Prefix+"Table3.png");

            //Console.WriteLine(data.IsOnVerticalLine(new Point(640,436)));

            Point p = data.FindProperPoint();
            //var list = IdentifierHelper.MakeLine(new Point(0, p.Y), new Point(data.Width-1, p.Y));
            var list = IdentifierHelper.MakeLine(new Point(p.X, 0), new Point(p.X, data.Height-1));
            var l1 = data.FindFeaturedPoints(list);

            //var l2 = l1.Where(vp => data.IsOnHorizontalLine(vp) && data.isSep(vp)).ToList();
            //var l2 = l1.Where(vp=>data.IsOnVerticalLine(vp)).ToList();
            var l2 = l1.Where(data.isSep).ToList();
            
            var l3 = data.GetCord(l2);
            Console.WriteLine("L1:{0}", l1.Count());
            Console.WriteLine(l2.Count()); 
            Console.WriteLine(l3.Count());

            //foreach (var item in l1)
            //    tongji(data.GetPixel(item));
            //Console.WriteLine("{0},{1},{2}",r.Average(),g.Average(),b.Average());


            data.DrawPoints(l1, Color.Blue);
            data.DrawPoints(l2, Color.Red);
            data.DrawPoints(l3, Color.Green);

            //IList<Point> list = IdentifierHelper.MakeLine(new Point(139, 50), new Point(139, 800));
            //139, 50 is on vLine
            //int id=data.FindEndPoint(list, 0);

            //Console.WriteLine(data.IsOnVerticalLine(new Point(136, 50)));
            //Console.WriteLine(data.IsOnVerticalLine(new Point(137, 50)));
            //Console.WriteLine(data.IsOnVerticalLine(new Point(138, 50)));
            //Console.WriteLine(data.IsOnVerticalLine(new Point(139, 50)));
            //Console.WriteLine(data.IsOnVerticalLine(new Point(140, 50)));
            //Console.WriteLine(data.IsOnVerticalLine(new Point(141, 50)));
            //Console.WriteLine(data.IsOnVerticalLine(new Point(142, 50)));
            //data.DrawLine(list[0],list[id],Color.Red);
            data.Save("qq.png");
            //int width,height;
            //int a, b, c, d;
            //bool actual = target.GetTableInfo(data, out width, out height,out a,out b,out c,out d);
            //Console.WriteLine(height);
        }
    }
}
