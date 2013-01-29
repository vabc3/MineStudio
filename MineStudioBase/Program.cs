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
        static void Main(string[] args)
        {
            IMineIdentifier target = MineIdentifierFactory.GetDefaultIdentifier();
            Bitmap data = new Bitmap(Prefix+"Table5.png");
            IList<Point> list = IdentifierHelper.MakeLine(new Point(0, 163), new Point(1279, 163));
            var l1 = data.FindFeaturedPoints(list);
            Console.WriteLine("l1C:{0}",l1.Count());
            //var l2 = l1.Where(data.IsOnVerticalLine).ToList();
            var l2 = l1.Where(p=>Color.Black.Near(data.GetPixel(p.X,p.Y))).ToList();
            data.DrawPoints(l1, Color.Blue);
            data.DrawPoints(l2, Color.Red);
            Console.WriteLine(l2.Count());
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
