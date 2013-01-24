using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineStudio
{
    class MineTable
    {
        public MineTable(int H)
        {

        }


        public int Height { get; set; }
        public int Width { get; set; }
        public int MineCount { get; set; }

        public override string ToString()
        {
            return "Mtab-le";
        }
    }
}
