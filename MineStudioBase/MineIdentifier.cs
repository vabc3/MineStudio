using System.Drawing;

namespace MineStudio
{
    interface IMineIdentifier
    {
        bool GetTableInfo(Bitmap data, out int width, out int height);
        bool GetCellInfo(Bitmap data, out CellStatus status, int n);
    }

    public class W7MineIdentifier : IMineIdentifier
    {
        public bool GetTableInfo(Bitmap data, out int width, out int height)
        {
            width = 1;
            height = 1;
            return true;
        }

        public bool GetCellInfo(Bitmap data, out CellStatus status, int n)
        {
            status = CellStatus.Ground;
            n = 6;
            return true;
        }
    }
}
