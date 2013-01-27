using System;

namespace MineStudio
{
    public enum MineStatus
    {
        ILLEGAL, UNKNOW, NOMINE, ISMINE
    }

    public class MineTable
    {

        private static readonly int UNKNOW = -2;
        private static readonly int ISMINE = -1;
        public int[,]  Table;
        private int Current;
        public int Height { get; set; }
        public int Width { get; set; }
        public int MineCount { get; set; }


        public MineTable(int Height, int Width, int MineCount)
        {
            this.Height     = Height;
            this.Width      = Width;
            this.MineCount  = MineCount;
            this.Table      = new int[Height, Width];
            this.Current    = 0;
            for (int i = 0; i < Height; i++)
                for (int j = 0; j<Width; j++)
                    this.Table[i, j]=UNKNOW;
        }

        public MineStatus SetStatus(int x, int y, MineStatus status, int Num=-1)
        {
            if ((x<0 || x>=Width || y<0 || y>=Height || UNKNOW!=Table[y, x]))
                return MineStatus.ILLEGAL;
            MineStatus rt=status;
            switch (status)
            {
                case MineStatus.NOMINE:
                    if (Num>=0 && Num<=8)
                        Table[y, x] = Num;
                    else
                        rt          = MineStatus.ILLEGAL;
                    break;
                case MineStatus.ISMINE:
                    if (Current<MineCount)
                    {
                        Table[y, x]     = ISMINE;
                        Current++;
                    }
                    break;
            }
            return rt;
        }

        public MineStatus GetStatus(int x, int y, out int Num)
        {
            Num = -1;

            if (x<0 || x>=Width || y<0 || y>=Height)
                return MineStatus.ILLEGAL;

            Num=Table[y, x];
            if (UNKNOW==Num)
                return MineStatus.UNKNOW;
            else if (ISMINE==Num)
                return MineStatus.ISMINE;

            return MineStatus.NOMINE;
        }

        private readonly int[,] Direct= { {1,0},{1,1},{0,1},{-1,1},
                                          {-1,0},{-1,-1},{0,-1},{1,-1}
                                        };

        private int IsMine(int x, int y)
        {
            if (x<0 || x>=Width || y<0 || y>=Height)
                return 0;
            else
                return (ISMINE==Table[x, y]?1:0);
        }

        private int Sum(int x, int y)
        {
            if (x<0 || x>=Width || y<0 || y>=Height)
                return -1;
            int count=0;

            for (int i=0; i<8; i++)
            {
                count+=IsMine(y+Direct[i, 0], x+Direct[i, 1]);
            }

            return count;
        }

        public void Conduce()
        {
            if (Current==MineCount)
            {
                Console.WriteLine("Can !");
                for (int j=0; j<Height; j++)
                    for (int i=0; i<Width; i++)
                        if (UNKNOW==Table[j, i])
                            Table[j, i]=Sum(i, j);
            }
        }

        public void Show()
        {
            for (int y=0; y<Height; y++)
            {
                for (int x=0; x<Width; x++)
                {
                    int st=Table[y, x];
                    if (st>=0)
                        Console.Write(st);
                    else if (st==UNKNOW)
                        Console.Write('U');
                    else
                        Console.Write('M');
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            return String.Format("MineTable {2}@{0}x{1}", Height, Width, MineCount);
        }
    }
}
