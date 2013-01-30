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
            Console.WriteLine("L1:{0}", l1.Count());
            //var l2 = l1.Where(data.IsOnVerticalLine).ToList();
            //var l2 = l1.Where(vp => data.IsOnVerticalLine(vp)).ToList();
            var l2 = l1.Where(data.isSep).ToList();
            Console.WriteLine(l2.Count());
            var l3 = data.GetCord(l2);
            Console.WriteLine(l3.Count());
            width = l3.Count()-1;
            if (width<0) return false;
            var listA = IdentifierHelper.MakeLine(new Point(p.X, 0), new Point(p.X,data.Height-1));
            //height = data.GetCord(data.FindFeaturedPoints(listA).Where(vp => data.IsOnHorizontalLine(vp)).ToList()).Count() - 1;
            height = data.GetCord(data.FindFeaturedPoints(listA).Where(data.isSep).ToList()).Count() - 1;
            if (height<0) return false;

            
            return true;
        }

        public bool GetCellInfo(System.Drawing.Bitmap data, out CellStatus status, int n)
        {
            throw new NotImplementedException();
        }
    }
}
