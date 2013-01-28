using System;
using System.Linq;

namespace MineStudio
{
  

    public class MineTable
    {
        public readonly int Height;
        public readonly int Width;
        public readonly int MineCount;
        public int Current { get; private set; }
        public MineCell[] Table { get; private set; }

        private readonly int[][] _dir=
        {
          new[]{1,0},new[]{1,1},new[]{0,1},new[]{-1,1},
          new[]{-1,0},new[]{-1,-1},new[]{0,-1},new[]{1,-1}
        };

        public MineTable(int height, int width, int mineCount)
        {
            Height      = height;
            Width       = width;
            MineCount   = mineCount;
            Current     = 0;
            Table       = new MineCell[Height*Width];
            for (var j=0; j<Height; j++)
                for (var i = 0; i < Width; i++)
                    Table[GetIndex(i, j)] = new MineCell(i, j);
            foreach (var it in Table)
                it.NearCells=(from a in _dir where IndexValid(it.X+a[0], it.Y+a[1]) select Table[GetIndex(it.X+a[0], it.Y+a[1])]).ToList();
        }

        public bool IndexValid(int x, int y)
        {
            return (x >= 0 && x < Width && y >= 0 && y < Height);
        }

        public int GetIndex(int x, int y)
        {
            return y * Width+x;
        }

        public bool SetStatus(int x, int y, CellStatus status, int num=-1)
        {
            if (!IndexValid(x, y))
                return false;
            int index = GetIndex(x, y);
            switch (status)
            {
                case CellStatus.Unknow:
                    break;
                case CellStatus.Mine:
                    return Table[index].SetStatusMine();
                    break;
                case CellStatus.Ground:
                    return Table[index].SetStatusGround(num);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
            return true;
        }


        public void UpdateCount()
        {
            Current = 0;
            foreach (var mineCell in Table)
            {
                if (CellStatus.Mine == mineCell.Status)
                    Current++;
                int mc = 0;
                int uc = 0;
                foreach (var it in mineCell.NearCells)
                {
                    switch (it.Status)
                    {
                        case CellStatus.Unknow:
                            uc++;
                            break;
                        case CellStatus.Mine:
                            mc++;
                            break;
                        case CellStatus.Ground:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                mineCell.MineCount=mc;
                mineCell.UnknowCount=uc;
            }
        }

        public void FullDe()
        {
            if (Current==MineCount)
            {
                Console.WriteLine("Can !");
                for (var i=0; i<Width*Height; i++)
                    if (CellStatus.Unknow==Table[i].Status)
                    {
                        Table[i].SetStatusGround(Table[i].MineCount);
                        Table[i].OnPropertyChanged();
                    }
            }
        }

        public void Deduce()
        {
            UpdateCount();
            foreach (var item in Table)
                item.CleanPred();
            foreach (var item in Table)
            {
                if (CellStatus.Ground != item.Status || 0==item.UnknowCount)
                    continue;

                var unknows = from it in item.NearCells where it.Status==CellStatus.Unknow && it.PredStatus==CellStatus.Unknow select it;
                if (item.N == item.MineCount)
                {
                    foreach (var it in unknows)
                    {
                        it.PredStatus = CellStatus.Ground;
                        it.PredReason = string.Format("R1:Grid {0} has all mines found.",it.Location);
                    }
                }
                else if (item.N > item.MineCount)
                {
                    var delta = item.N - item.MineCount;
                    if (delta == item.UnknowCount)
                    {
                        foreach (var it in unknows)
                        {
                            it.PredStatus = CellStatus.Mine;
                            it.PredReason = string.Format("R2:Grid {0} has only mines remain.",it.Location);
                        }
                    }
                }
                else
                    throw new Exception("Illeage");
            }
        }

        public void Show()
        {
            for (int y=0; y<Height; y++)
            {
                for (int x=0; x<Width; x++)
                {
                    int idx=GetIndex(x, y);
                    var st=Table[idx].Status;
                    switch (st)
                    {
                        case CellStatus.Unknow:
                            Console.Write('U');
                            break;
                        case CellStatus.Mine:
                            Console.Write('M');
                            break;
                        case CellStatus.Ground:

                            Console.Write(Table[idx].MineCount);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
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
