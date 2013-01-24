using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineStudio
{
    class MineTable
    {
        static readonly int UNKNOW = -2;
        static readonly int ISMINE = -1;

        public MineTable(int Height, int Width, int MineCount)
        {
            this.Height     = Height;
            this.Width      = Width;
            this.MineCount  = MineCount;
            this.Table      = new int[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j<Width; j++)
                    this.Table[i,j]=UNKNOW;
        }

        public int Height       { get; set; }
        public int Width        { get; set; }
        public int MineCount    { get; set; }
        private int[,]  Table;


        public override string ToString()
        {
            return String.Format("MineTable {2}@{0}x{1}", Height, Width, MineCount);
        }
    }
}
