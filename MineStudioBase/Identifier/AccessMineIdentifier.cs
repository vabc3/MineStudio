using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenSURFcs;

namespace MineStudio.Identifier
{
    class AccessMineIdentifier : IMineIdentifier
    {
        public bool GetTableInfo(Bitmap data, out int width, out int height, out int ileft, out int itop, out int iwidth, out int iheight)
        {
            width = height = ileft = itop = iwidth = iheight = 0;
            Point p = data.FindProperPoint();
            var list = IdentifierHelper.MakeLine(new Point(0, p.Y), new Point(data.Width-1, p.Y));
            var l1=data.FindFeaturedPoints(list);
            //Console.WriteLine("L1:{0}", l1.Count());
            //var l2 = l1.Where(data.IsOnVerticalLine).ToList();
            //var l2 = l1.Where(vp => data.IsOnVerticalLine(vp)).ToList();
            var l2 = l1.Where(data.isSep).ToList();
            //Console.WriteLine(l2.Count());
            var l3 = data.GetCord(l2);
            //Console.WriteLine(l3.Count());
            width = l3.Count()-1;
            if (width<0) return false;
            var listA = IdentifierHelper.MakeLine(new Point(p.X, 0), new Point(p.X, data.Height-1));
            //height = data.GetCord(data.FindFeaturedPoints(listA).Where(vp => data.IsOnHorizontalLine(vp)).ToList()).Count() - 1;
            var l4 = data.GetCord(data.FindFeaturedPoints(listA).Where(data.isSep).ToList());
            height = l4.Count() - 1;
            if (height<0) return false;

            ileft = l3.First().X;
            itop = l4.First().Y;
            iwidth = l3.Last().X - ileft;
            iheight = l4.Last().Y - itop;
            return true;
        }

        private void Purify(Bitmap data, IList<Color> list)
        {
            int w = data.Width;
            int h = data.Height;
            for (var i=0; i<h; i++)
                for (var j = 0; j < w; j++)
                    if (list.Any(color => color.Near(data.GetPixel(j, i))))
                        data.SetPixel(j, i, Color.Black);
                    else {
                    }
        }

        private readonly List<Color> _pcolorlist = new List<Color>()
            {
                Color.Black,
                Color.FromArgb(193, 201, 224),
                Color.FromArgb(168,171,174),
                Color.FromArgb(221,227,232),
                Color.FromArgb(132,132,149),
                Color.FromArgb(73 ,73 ,73 ),
                Color.FromArgb(41 ,41 ,42  )
            };

        private Dictionary<Color, int> patterns = new Dictionary<Color, int>()
            {
                //8753!!!
                {Color.FromArgb(0,0,0),0},//Ground
                {Color.FromArgb(71,87,190),1},
                {Color.FromArgb(40,110,18),2},
                {Color.FromArgb(171,27,31),3},//  3-2
                //{Color.FromArgb(139,27,30),3},//0.198275862068966
                {Color.FromArgb(24,26,137),4},
                {Color.FromArgb(24,128,134),6},
                {Color.FromArgb(174,28,31),8},//0.275555555555556
                //{Color.FromArgb(88,127,223),-1},//unknow
                //{Color.FromArgb(141,200,249),-1},//unknow
                {Color.FromArgb(59,78,195),-1},//UNCOV
                {Color.FromArgb(100,146,233),-1},//UNCOV
                {Color.FromArgb(152,208,251),-1},//UNCOV
                //{Color.FromArgb(79,103,156),-2},//mine
            };

        private Dictionary<int, double> fills = new Dictionary<int, double>()
            {
            {3, (0.218888888888889+0.197988505747126)/2},
             //{8, 0.275555555555556},
            };

        private static readonly IPoint n3 = new IPoint
            {
                descriptor = new float[64]
                    {
                        0.04681531f, 0.03735068f, 0.05510309f, 0.04681495f, 0.06770136f, 0.05118809f, 0.1652367f,
                        0.1912215f, -0.09536166f, 0.1172694f,
                        0.1434319f, 0.1763384f, -0.02755679f, 0.03343868f, 0.02998384f, 0.03625181f, 0.06830626f,
                        0.07474465f, 0.2419186f,
                        0.1782453f, -0.02653753f, -0.002862814f, 0.2862876f, 0.2784799f, 0.05222468f, -0.04557018f,
                        0.194953f, 0.2148341f,
                        -0.128393f, 0.05230061f, 0.1691773f, 0.1622149f, 0.1004062f, -0.09296802f, 0.1332558f,
                        0.1403917f, -0.01668403f, 0.03669159f,
                        0.1737377f, 0.1708666f, -0.06098606f, 0.004688348f, 0.2773818f, 0.2353155f, -0.09350097f,
                        -0.08688719f, 0.1421477f,
                        0.1061522f, 0.03056811f, -0.03665952f, 0.03057013f, 0.03675315f, 0.08749636f, -0.1304503f,
                        0.1161509f, 0.1433895f, -0.09672495f,
                        -0.09320082f, 0.1342009f, 0.1273425f, -0.01673819f, -0.01366693f, 0.0184623f, 0.01532018f,
                    }
            };
        private static readonly  IPoint n5 = new IPoint
        {
            descriptor = new float[64]
                        {
                            0.03530638f, 0.05713842f, 0.0690887f, 0.1260714f, 0.0264945f, 0.04927357f, 0.07300954f,
                            0.2079632f, -0.09921744f,
                            0.01314095f, 0.1677074f, 0.127056f, -0.03074108f, 0.006876063f, 0.03772875f, 0.01124506f,
                            0.04604264f, -0.03890879f,
                            0.2899677f, 0.111809f, -0.01531229f, -9.397529E-06f, 0.3177872f, 0.3032062f, -0.06854086f,
                            0.02448252f, 0.3501409f, 0.2329254f
                            , -0.07093263f, 0.02616425f, 0.1153369f, 0.03467951f, 0.1110193f, -0.008172642f, 0.2492919f,
                            0.1147283f, -0.02617276f, -0.02448932f,
                            0.1518737f, 0.2562167f, -0.06455477f, -0.08419534f, 0.1332125f, 0.3040843f, -0.09866637f,
                            -0.02744199f, 0.1088211f, 0.07396138f, 0.02011743f, -0.04973738f, 0.038783f, 0.05214309f,
                            -0.02113469f, -0.108716f, 0.02743542f,
                            0.110308f, -0.01708332f, -0.07140402f, 0.01722135f, 0.07142317f, -0.007659893f, -0.01346057f
                            , 0.007659893f, 0.0140371f,

                        }
        };

        private static readonly IPoint n7 = new IPoint
        {
            descriptor = new float[64]
                        {
                             0.04716279f,0.05470352f,0.07610834f,0.06005406f,-0.05014946f,0.09032308f,0.1320693f,0.1697098f,-0.03780883f,
                             0.07674471f,0.05287393f,0.1027446f,-0.00350645f,0.008682714f,0.00350645f,0.008682714f,0.06512514f,0.05657578f,
                             0.1892256f,0.08679853f,-0.0184966f,0.1099915f,0.2934935f,0.2249953f,0.02222102f,0.2205252f,0.1942946f,0.3170939f
,-0.1076505f,0.05849154f,0.1254418f,0.1261269f,-0.01612678f,-0.009986572f,0.1542867f,0.08502701f,0.1370031f,0.03351792f,
0.3450895f,0.07160597f,-0.1011399f,-0.03752302f,0.1787003f,0.133546f,-0.1293214f,-0.06439282f,
0.131815f,0.07750108f,0.03392179f,-0.07503923f,0.07357085f,0.1045987f,0.1062317f,-0.1142621f,0.1669526f,0.2111712f,
-0.1406838f,-0.1000771f,0.1719687f,0.1077243f,-0.02333795f,-0.01302138f,0.02333795f,0.01302138f,
                        }
        };

        public bool GetCellInfo(Bitmap data, out CellStatus status, out int n)
        {
            status = CellStatus.Unknow;
            n = 4;
            
            IntegralImage iimg = IntegralImage.FromImage(data);
            var ipts = FastHessian.getIpoints(0.0002f, 5, 2, iimg);
            SurfDescriptor.DecribeInterestPoints(ipts, false, false, iimg);
            if (ipts.Any(n5.IsNear)) {
                status = CellStatus.Ground;
                n = 5;
                return true;
            } else if (ipts.Any(n7.IsNear)) {
                status = CellStatus.Ground;
                n = 7;
                return true;
            }
            if (ipts.Any(n3.IsNear)) {
                status = CellStatus.Ground;
                n = 3;
                return true;
            }


            int w = data.Width;
            int h = data.Height;

            List<Color> cdata = new List<Color>();

            for (var i = 0; i < h; i++)
                for (var j = 0; j < w; j++) {
                    var c=data.GetPixel(j, i);
                    if (!(_pcolorlist.Any(color => color.Near(c,64))))
                        cdata.Add(c);
                    else
                        data.SetPixel(j,i,Color.Black);
                }

            Color t;
            if (cdata.Any() && cdata.Count>100)
                t = Color.FromArgb((int) cdata.Average(p => p.R), (int) cdata.Average(p => p.G),
                                   (int) cdata.Average(p => p.B));
            else
                t = Color.Black;

            double d = 1.0*cdata.Count()/h/w;
            Console.WriteLine(t);
            Console.WriteLine(d);
            Console.WriteLine("Byway");
            data.Save("d:\\gc.png");
            bool rt=false;
            foreach (var item in patterns.Keys) {
                if (item.Near(t,16)) {
                    n = patterns[item];
                    if (fills.ContainsKey(n)) {
                        if (IdentifierHelper.ValueNear(fills[n], d,.06)) {
                            rt = true; break;
                        }
                    } else {
                        status = CellStatus.Ground;
                        rt = true; break;
                    }
                }
            }
            if (rt) {
                if (n >= 0)
                    status = CellStatus.Ground;
                else if (n == -1)
                    status = CellStatus.Unknow;
                else
                    status = CellStatus.Mine;
            }

            return rt;
        }
    }
}
