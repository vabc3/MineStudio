using System.Drawing;

namespace MineStudio.Identifier
{
    public interface IMineIdentifier
    {
        bool GetTableInfo(Bitmap data, out int width, out int height,out int ileft,out int itop,out int iwidth,out int iheight);
        bool GetCellInfo(Bitmap data, out CellStatus status,out int n);
    }
}
