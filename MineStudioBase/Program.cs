using System;
using System.Collections.Generic;
using System.Drawing;
using MineStudio.Identifier;
using System.Linq;
using OpenSURFcs;

namespace MineStudio
{
    class Program
    {
        private const String Prefix = @"..\..\..\MineStudioTest\ImageCases\";

        private readonly static List<Color> _pcolorlist = new List<Color>()
            {
                Color.Black,
                Color.FromArgb(193, 201, 224),
                Color.FromArgb(168,171,174),
                Color.FromArgb(221,227,232),
                Color.FromArgb(132,132,149),
                Color.FromArgb(73 ,73 ,73 ),
                Color.FromArgb(41 ,41 ,42  )
            };

        private static void purify(Bitmap img)
        {
            var h = img.Height;
            var w = img.Width;
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++) {
                    var c=img.GetPixel(j, i);
                    if ((_pcolorlist.Any(color => color.Near(c, 64))))
                        img.SetPixel(j, i, Color.Black);
                }
        }

        static void Main(string[] args)
        {
            Bitmap img = new Bitmap(Prefix + "u-3.png");
            IMineIdentifier im = MineIdentifierFactory.GetDefaultIdentifier();
            CellStatus s;
            int n;
            im.GetCellInfo(img,out s,out n);
        }

        private static void D2()
        {
            Bitmap img = new Bitmap(Prefix + "3-3.png");
            IntegralImage iimg = IntegralImage.FromImage(img);
            var ipts = FastHessian.getIpoints(0.0002f, 5, 2, iimg);
            SurfDescriptor.DecribeInterestPoints(ipts, false, false, iimg);
            Console.WriteLine(ipts.Count);
            var l = ipts.First().descriptor;
            for (var i = 0; i < 64; i++) Console.Write("{0}f,", l[i]);
            //ipts.ForEach(p=>Console.WriteLine(p.IsNear(n7)));
        }
    }
}
