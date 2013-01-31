using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenSURFcs;

namespace MineStudio.Identifier
{
    interface ICellIdentifier
    {
        bool Deal(BitmapWrapper bw, out CellStatus status, out int n);
    }

    internal abstract class BaseIdentifier : ICellIdentifier
    {
        protected readonly CellStatus Status;
        protected readonly int N;

        protected BaseIdentifier(int n)
        {
            Status=CellStatus.Ground;
            N=n;
        }

        protected BaseIdentifier(CellStatus status)
        {
            Status=status;
            N=-3;
        }

        public bool Deal(BitmapWrapper bw, out CellStatus status, out int n)
        {
            status=Status;
            n=N;
            return Deal(bw);
        }

        public abstract bool Deal(BitmapWrapper bw);
    }

    class SurfIndentifier : BaseIdentifier
    {
        private readonly IPoint _ipoint;
        public SurfIndentifier(IPoint fea, int n):base(n)
        {
            _ipoint=fea;
        }

        public override bool Deal(BitmapWrapper bw)
        {
            return bw.List.Any(_ipoint.IsNear);
        }
    }

    class ColorIndentifier : BaseIdentifier
    {
        private readonly Color _color;
        private readonly double _factor;

        public ColorIndentifier(Color color, double factor,  int n):base(n)
        {
            _color=color;
            _factor=factor;
        }

        public ColorIndentifier(Color color, double factor, CellStatus status,int n=-3):base(status)
        {
            _color=color;
            _factor=factor;
        }

        public override bool Deal(BitmapWrapper bw)
        {
            if (_color.Near(bw.AvgColor, 16)) {
                return (_factor <= 0) ||bw.Factor<_factor;
            }
            return false;
        }
    }
}

