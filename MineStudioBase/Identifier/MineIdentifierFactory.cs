using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineStudio.Identifier
{
    public class MineIdentifierFactory
    {
        //private static readonly IMineIdentifier Identifier = new DotMineIdentifier();//5.7s(7),7s(8/9)
        private static readonly IMineIdentifier Identifier = new AccessMineIdentifier();//.9s
        public static IMineIdentifier GetDefaultIdentifier()
        {
            return Identifier;
        }
    }
}
