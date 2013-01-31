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

    class DefaultIdentifier:BaseIdentifier
    {
        public DefaultIdentifier(int n) : base(n)
        {
        }

        public DefaultIdentifier(CellStatus status) : base(status)
        {
        }

        public override bool Deal(BitmapWrapper bw)
        {
            return true;
        }
    }

    class MineIdentifier:ICellIdentifier
    {
        public bool Deal(BitmapWrapper bw, out CellStatus status, out int n)
        {
            status = CellStatus.Mine;
            n = -3;
            var bm = bw.img;
            var w = bm.Width/2;
            var h = bm.Height/2;
            for(var i=0;i<h;i++)
                for (var j = 0; j < w; j++) {
                    if (bm.GetPixel(j, i).R>250) return true;
                }
            return false;
        }
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
        public SurfIndentifier(int n, IPoint fea)
            : base(n)
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

        public ColorIndentifier(int n, Color color, double factor=-1)
            : base(n)
        {
            _color=color;
            _factor=factor;
        }

        public ColorIndentifier(CellStatus status, Color color, double factor=-1)
            : base(status)
        {
            _color=color;
            _factor=factor;
        }

        public override bool Deal(BitmapWrapper bw)
        {
            if (_color.Near(bw.AvgColor, 30)) {
                return (_factor <= 0) ||bw.Factor<_factor;
            }
            return false;
        }
    }
}

