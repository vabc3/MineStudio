using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenSURFcs;

namespace MineStudio.Identifier
{
    static class IdentifyData
    {
        private static readonly  IPoint N5 = new IPoint
        {
            descriptor = new float[64]
                        {
                            0.03530638f, 0.05713842f, 0.0690887f, 0.1260714f, 0.0264945f, 0.04927357f, 0.07300954f,
                            0.2079632f, -0.09921744f,0.01314095f, 0.1677074f, 0.127056f, -0.03074108f, 0.006876063f,
                            0.03772875f, 0.01124506f,0.04604264f, -0.03890879f,0.2899677f, 0.111809f, -0.01531229f, 
                            -9.397529E-06f, 0.3177872f, 0.3032062f, -0.06854086f,0.02448252f, 0.3501409f, 0.2329254f
                            , -0.07093263f, 0.02616425f, 0.1153369f, 0.03467951f, 0.1110193f, -0.008172642f, 0.2492919f,
                            0.1147283f, -0.02617276f, -0.02448932f,0.1518737f, 0.2562167f, -0.06455477f, -0.08419534f, 
                            0.1332125f, 0.3040843f, -0.09866637f,-0.02744199f, 0.1088211f, 0.07396138f, 0.02011743f, 
                            -0.04973738f, 0.038783f, 0.05214309f,-0.02113469f, -0.108716f, 0.02743542f,0.110308f, 
                            -0.01708332f, -0.07140402f, 0.01722135f, 0.07142317f, -0.007659893f, -0.01346057f,
                             0.007659893f, 0.0140371f,
                        }
        };

        private static readonly IPoint N7 = new IPoint
        {
            descriptor = new float[64]
                        {
                             0.04716279f,0.05470352f,0.07610834f,0.06005406f,-0.05014946f,0.09032308f,0.1320693f,0.1697098f,-0.03780883f,
                             0.07674471f,0.05287393f,0.1027446f,-0.00350645f,0.008682714f,0.00350645f,0.008682714f,0.06512514f,0.05657578f,
                             0.1892256f,0.08679853f,-0.0184966f,0.1099915f,0.2934935f,0.2249953f,0.02222102f,0.2205252f,0.1942946f,0.3170939f,
                            -0.1076505f,0.05849154f,0.1254418f,0.1261269f,-0.01612678f,-0.009986572f,0.1542867f,0.08502701f,0.1370031f,
                            0.03351792f,0.3450895f,0.07160597f,-0.1011399f,-0.03752302f,0.1787003f,0.133546f,-0.1293214f,-0.06439282f,
                            0.131815f,0.07750108f,0.03392179f,-0.07503923f,0.07357085f,0.1045987f,0.1062317f,-0.1142621f,0.1669526f,0.2111712f,
                            -0.1406838f,-0.1000771f,0.1719687f,0.1077243f,-0.02333795f,-0.01302138f,0.02333795f,0.01302138f,
                        }
        };


        public static readonly List<ICellIdentifier> IdentifierList = new List<ICellIdentifier>()
            {
                new SurfIndentifier(5,N5),
                new SurfIndentifier(7,N7),
                new ColorIndentifier(0,Color.FromArgb(0,0,0)),
                new ColorIndentifier(1,Color.FromArgb(71,87,190),.22),
                new ColorIndentifier(2,Color.FromArgb(40,110,18)),
                new ColorIndentifier(3,Color.FromArgb(171,27,31),0.23),
                new ColorIndentifier(4,Color.FromArgb(24,26,137)),
                new ColorIndentifier(4,Color.FromArgb(14,15,136)),
                new ColorIndentifier(6,Color.FromArgb(24,128,134)),
                new ColorIndentifier(8,Color.FromArgb(174,28,31)),
                new MineIdentifier(),
                new DefaultIdentifier(CellStatus.Covered),
                //new ColorIndentifier( Color.FromArgb(88,126,193),-1,CellStatus.Mine),
                //new ColorIndentifier( Color.FromArgb(209,178,108),-1,CellStatus.Mine),
                //new ColorIndentifier( Color.FromArgb(102,105,190),-1,CellStatus.Mine),
            };
    }
}
