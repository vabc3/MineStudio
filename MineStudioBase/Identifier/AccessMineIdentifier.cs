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


            return true;
        }

        public bool GetCellInfo(System.Drawing.Bitmap data, out CellStatus status, int n)
        {
            throw new NotImplementedException();
        }
    }
}
