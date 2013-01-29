using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineStudio.Identifier
{
    public class MineIdentifierFactory
    {
        //private static readonly IMineIdentifier Identifier = new DotMineIdentifier();//5.7s
        private static readonly IMineIdentifier Identifier =new LineMineIdentifier();
        public static IMineIdentifier GetDefaultIdentifier()
        {
            return Identifier;
        }
    }
}
