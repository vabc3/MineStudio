using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using OpenSURFcs;

namespace MineStudio.Identifier
{
    public class BitmapWrapper
    {
        internal BitmapWrapper(Bitmap data)
        {
            img=data;
            IntegralImage iimg = IntegralImage.FromImage(data);
            List= FastHessian.getIpoints(0.0002f, 5, 2, iimg);
            SurfDescriptor.DecribeInterestPoints(List, false, false, iimg);
            List<Color> cdata = new List<Color>();

            int w = data.Width;
            int h = data.Height;
            //for (var i = 0; i < h; i++)
            //    for (var j = 0; j < w; j++) {
            //        var c=data.GetPixel(j, i);
            //        if (!(_pcolorlist.Any(color => color.Near(c, 64))))
            //            cdata.Add(c);
            //        else
            //            data.SetPixel(j, i, Color.Black);
            //    }


            var image=img;
            BitmapData dataIn = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe {
                byte* pIn = (byte*)(dataIn.Scan0.ToPointer());
                for (int y = 0; y < dataIn.Height; y++) {
                    for (int x = 0; x < dataIn.Width; x++) {
                        int cb = (byte)(pIn[0]);
                        int cg = (byte)(pIn[1]);
                        int cr = (byte)(pIn[2]);
                        Color c = Color.FromArgb(cr, cg, cb);
                        if (!(_pcolorlist.Any(color => color.Near(c, 64))))
                            cdata.Add(c);
                        else 
                            *pIn = *(pIn + 1) = *(pIn + 2) = 0;

                        pIn += 3;
                    }
                    pIn += dataIn.Stride - dataIn.Width * 3;
                }
            }
            image.UnlockBits(dataIn);









            if (cdata.Any() && cdata.Count>100)
                AvgColor = Color.FromArgb((int)cdata.Average(p => p.R), (int)cdata.Average(p => p.G),
                                   (int)cdata.Average(p => p.B));
            else
                AvgColor = Color.Black;

            Factor = 1.0*cdata.Count()/h/w;

        }

        public readonly Bitmap img;
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