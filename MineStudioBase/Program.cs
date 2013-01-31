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
            
            Bitmap img = new Bitmap(Prefix + "5-3.png");
            MineScanner im = new MineScanner();
            CellStatus s;
            int n;
            var b=im.GetCellInfo(img,out s,out n);

            int v = 2;
        }
    }
}
