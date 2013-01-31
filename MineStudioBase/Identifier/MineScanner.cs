using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenSURFcs;

namespace MineStudio.Identifier
{
    class MineScanner
    {
        public bool GetTableInfo(Bitmap data, out int width, out int height, out int ileft, out int itop, out int iwidth, out int iheight)
        {
            width = height = ileft = itop = iwidth = iheight = 0;
            Point p = data.FindProperPoint();
            var list = IdentifierHelper.MakeLine(new Point(0, p.Y), new Point(data.Width-1, p.Y));
            var l3 = data.GetCord(data.FindFeaturedPoints(list).Where(data.IsSep).ToList());
            width = l3.Count()-1;
            if (width<0) return false;
            var listA = IdentifierHelper.MakeLine(new Point(p.X, 0), new Point(p.X, data.Height-1));
            var l4 = data.GetCord(data.FindFeaturedPoints(listA).Where(data.IsSep).ToList());
            height = l4.Count() - 1;
            if (height<0) return false;

            ileft = l3.First().X;
            itop = l4.First().Y;
            iwidth = l3.Last().X - ileft;
            iheight = l4.Last().Y - itop;
            return true;
        }

        public bool GetCellInfo(Bitmap data, out CellStatus status, out int n)
        {
            status = CellStatus.Undef;
            n = -3;
            BitmapWrapper bw = new BitmapWrapper(data);


            Color t = bw.AvgColor;
            double d = bw.Factor;
            Console.WriteLine(t);
            Console.WriteLine(d);
            Console.WriteLine("Byway");
            //data.Save("d:\\gc.png");
            
            foreach (var item in IdentifyData.IdentifierList)
                if (item.Deal(bw, out status, out n)) return true;

            status = CellStatus.Undef;
            n = -3;

            return false;;
        }
    }
}
