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

        static void Main(string[] args)
        {
            Bitmap img = new Bitmap(Prefix + "5-2.png");
            // Create Integral Image
            IntegralImage iimg = IntegralImage.FromImage(img);

            // Extract the interest points
            var ipts = FastHessian.getIpoints(0.0002f, 5, 2, iimg);

            // Describe the interest points
            SurfDescriptor.DecribeInterestPoints(ipts, false, false, iimg);

        }

    }
}
