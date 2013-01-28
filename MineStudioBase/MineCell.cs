using System.Collections.Generic;
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
            Status      = CellStatus.Unknow;
            PredStatus  = CellStatus.Unknow;
            PredReason   = "";
        }

        public readonly int X;
        public readonly int Y;

        private int _N;
        public int N { 
            get { return _N; }
            set {
                _N = value;
                OnPropertyChanged("N");
                Status = CellStatus.Ground;
            }
        }

        public string Location
        {
            get { return string.Format("({0},{1})", X, Y); }
        }

        private int _mineCount;
        public int MineCount {
            get { return _mineCount; }
            set { 
                _mineCount = value;
                OnPropertyChanged("MineCount");
            }
        }

        private int _unknowCount;
        public int UnknowCount {
            get { return _unknowCount; }
            set { 
                _unknowCount = value;
                OnPropertyChanged("UnknowCount");
            }
        }

        private CellStatus _status;
        public CellStatus Status {
            get { return _status; }
            set { 
                _status = value;
                CleanPred();
                OnPropertyChanged("Status");
            }
        }

        public void CleanPred()
        {
            PredStatus = CellStatus.Unknow;
            PredReason = "";
        }

        private CellStatus _predStatus;
        public CellStatus PredStatus {
            get { return _predStatus; }
            set
            {
                _predStatus=value;
                OnPropertyChanged("PredStatus");
            }
        }

        private string _predReason;
        public string PredReason {
            get { return _predReason; }
            set { 
                _predReason=value;
                OnPropertyChanged("PredReason");
            }
        }
        public IEnumerable<MineCell> NearCells { get; set; }

        public void SetPredict(CellStatus predictStatus, string predictReason)
        {
            PredStatus = predictStatus;
            PredReason = predictReason;
        }

        public bool SetStatusMine()
        {
            Status  = CellStatus.Mine;
            return true;
        }

        public bool SetStatusGround(int num=-1)
        {
            if (num >= -1 && num < 9)
            {
                Status = CellStatus.Ground;
                if (num != -1)
                {
                    N       = num;
                }
                return true;
            }
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName="")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}