using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace MineStudio
{
    public enum CellStatus
    {
        Unknow, Mine, Ground
    }

    public class MineCell : INotifyPropertyChanged
    {
        public MineCell(int x, int y)
        {
            X           = x;
            Y           = y;
            MineCount   = -1;
            UnknowCount = -1;
            Covered     = true;
            Status      = CellStatus.Unknow;
        }

        public readonly int X;
        public readonly int Y;
        private int _mineCount;

        /// <summary>
        /// Number Of Mines Near
        /// </summary>
        public int MineCount
        {
            get { return _mineCount; }
            set { _mineCount = value;
            OnPropertyChanged();
            }
        }

        public int UnknowCount { get; set; }
        public bool Covered { get; private set; }
        public CellStatus Status { get; private set; }
        public IEnumerable<MineCell> NearCells { get; set; }

        public bool SetStatusMine()
        {
            Status  = CellStatus.Mine;
            Covered = false;
            return true;
        }

        public bool SetStatusGround(int num=-1)
        {
            if (num >= -1 && num < 9)
            {
                Status = CellStatus.Ground;
                if (num != -1)
                {
                    MineCount       = num;
                    Covered = false;
                }
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName="")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine("Debug!!!");
        }
    }

    public class MineTable
    {
        public readonly int Height;
        public readonly int Width;
        public readonly int MineCount;
        public int Current { get; private set; }
        public MineCell[] Table { get; private set; }

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
                it.NearCells=(from a in dire where IndexValid(it.X+a[0], it.Y+a[1]) select Table[GetIndex(it.X+a[0], it.Y+a[1])]).ToList();
        }

        private bool IndexValid(int x, int y)
        {
            return (x >= 0 && x < Width && y >= 0 && y < Height);
        }

        private int GetIndex(int x, int y)
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

        private readonly List<int[]> dire=new List<int[]> 
        {
           new[]{1,0},new[]{1,1},new[]{0,1},new[]{-1,1},
           new[]{-1,0},new[]{-1,-1},new[]{0,-1},new[]{1,-1}
        };


        public void Conduce()
        {
            UpdateCount();
            if (Current==MineCount)
            {
                Console.WriteLine("Can !");
                for (var i=0; i<Width*Height; i++)
                    if (CellStatus.Unknow==Table[i].Status)
                        Table[i].SetStatusGround(Table[i].MineCount);
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
                            if (Table[idx].Covered)
                                Console.Write("C");
                            else
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
