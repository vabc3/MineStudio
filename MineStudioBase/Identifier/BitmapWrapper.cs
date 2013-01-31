using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenSURFcs;

namespace MineStudio.Identifier
{
    public class BitmapWrapper
    {
        internal BitmapWrapper(Bitmap data)
        {
            IntegralImage iimg = IntegralImage.FromImage(data);
            List= FastHessian.getIpoints(0.0002f, 5, 2, iimg);
            SurfDescriptor.DecribeInterestPoints(List, false, false, iimg);

            int w = data.Width;
            int h = data.Height;
            List<Color> cdata = new List<Color>();
            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++) {
                    var c=data.GetPixel(j, i);
                    if (!(_pcolorlist.Any(color => color.Near(c, 64))))
                        cdata.Add(c);
                    else
                        data.SetPixel(j, i, Color.Black);
                }

            if (cdata.Any() && cdata.Count>100)
                AvgColor = Color.FromArgb((int)cdata.Average(p => p.R), (int)cdata.Average(p => p.G),
                                   (int)cdata.Average(p => p.B));
            else
                AvgColor = Color.Black;

            Factor = 1.0*cdata.Count()/h/w;

        }

        public readonly List<IPoint> List;
        public readonly Color AvgColor;
        public readonly double Factor;

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
    }
}