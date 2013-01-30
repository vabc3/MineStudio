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
            Bitmap img = new Bitmap(Prefix + "u-1.png");
            IMineIdentifier im = MineIdentifierFactory.GetDefaultIdentifier();
            CellStatus s;
            int n;
            im.GetCellInfo(img,out s,out n);
        }
    }
}
