using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MineStudio.Identifier
{
    class AccessMineIdentifier : IMineIdentifier
    {
        public bool GetTableInfo(Bitmap data, out int width, out int height, out int ileft, out int itop, out int iwidth, out int iheight)
        {
            width = height = ileft = itop = iwidth = iheight = 0;
            Point p = data.FindProperPoint();
            var list = IdentifierHelper.MakeLine(new Point(0, p.Y), new Point(data.Width-1, p.Y));
            var l1=data.FindFeaturedPoints(list);
            //Console.WriteLine("L1:{0}", l1.Count());
            //var l2 = l1.Where(data.IsOnVerticalLine).ToList();
            //var l2 = l1.Where(vp => data.IsOnVerticalLine(vp)).ToList();
            var l2 = l1.Where(data.isSep).ToList();
            //Console.WriteLine(l2.Count());
            var l3 = data.GetCord(l2);
            //Console.WriteLine(l3.Count());
            width = l3.Count()-1;
            if (width<0) return false;
            var listA = IdentifierHelper.MakeLine(new Point(p.X, 0), new Point(p.X, data.Height-1));
            //height = data.GetCord(data.FindFeaturedPoints(listA).Where(vp => data.IsOnHorizontalLine(vp)).ToList()).Count() - 1;
            var l4 = data.GetCord(data.FindFeaturedPoints(listA).Where(data.isSep).ToList());
            height = l4.Count() - 1;
            if (height<0) return false;

            ileft = l3.First().X;
            itop = l4.First().Y;
            iwidth = l3.Last().X - ileft;
            iheight = l4.Last().Y - itop;
            return true;
        }

        private void Purify(Bitmap data, IList<Color> list)
        {
            int w = data.Width;
            int h = data.Height;
            for (var i=0; i<h; i++)
                for (var j = 0; j < w; j++)
                    if (list.Any(color => color.Near(data.GetPixel(j, i))))
                        data.SetPixel(j, i, Color.Black);
                    else {
                    }
        }

        private readonly List<Color> _pcolorlist = new List<Color>()
            {
                Color.Black,
                Color.FromArgb(193, 201, 224),
                Color.FromArgb(168,171,174),
                Color.FromArgb(221,227,232),
                Color.FromArgb(132,132,149),
                Color.FromArgb(73 ,73 ,73 ),
                Color.FromArgb(41 ,41 ,42  )
            };

        private Dictionary<Color, int> patterns = new Dictionary<Color, int>()
            {
                //8753!!!
                {Color.FromArgb(0,0,0),0},//Ground
                {Color.FromArgb(71,87,190),1},
                {Color.FromArgb(40,110,18),2},
                {Color.FromArgb(171,27,31),3},//  3-2
                //{Color.FromArgb(139,27,30),3},//0.198275862068966
                {Color.FromArgb(24,26,137),4},
                {Color.FromArgb(131,20,22),5},//0.18
                {Color.FromArgb(24,128,134),6},
                {Color.FromArgb(170,40,35),7},//0.223851417399804
                {Color.FromArgb(174,28,31),8},//0.275555555555556
                {Color.FromArgb(88,127,223),-1},//unknow
                {Color.FromArgb(141,200,249),-1},//unknow
                //{Color.FromArgb(79,103,156),-2},//mine
            };

        private Dictionary<int, double> fills = new Dictionary<int, double>()
            {
                {3, (0.218888888888889+0.197988505747126)/2},
                {5, 0.184659090909091},
                {7, 0.192570869990225},
                {8, 0.275555555555556},
            };

        public bool GetCellInfo(Bitmap data, out CellStatus status, out int n)
        {
            status = CellStatus.Unknow;
            n = 4;
            int w = data.Width;
            int h = data.Height;

            List<Color> cdata = new List<Color>();

            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++) {
                    var c=data.GetPixel(j, i);
                    if (!(_pcolorlist.Any(color => color.Near(c,64))))
                        cdata.Add(c);
                    else
                        data.SetPixel(j,i,Color.Black);
                }

            Color t;
            if (cdata.Any() && cdata.Count>100)
                t = Color.FromArgb((int) cdata.Average(p => p.R), (int) cdata.Average(p => p.G),
                                   (int) cdata.Average(p => p.B));
            else
                t = Color.Black;

            double d = 1.0*cdata.Count()/h/w;
            Console.WriteLine(t);
            Console.WriteLine(d);
            Console.WriteLine("Byway");
            data.Save("d:\\gc.png");

            foreach (var item in patterns.Keys) {
                if (item.Near(t,16)) {
                    n = patterns[item];
                    if (fills.ContainsKey(n)) {
                        if (IdentifierHelper.ValueNear(fills[n], d,.06)) {
                            status = CellStatus.Ground;
                            return true;
                        }
                    } else {
                        status = CellStatus.Ground;
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}
