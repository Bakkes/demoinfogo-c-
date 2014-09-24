using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    interface IDemoParser
    {
        DemoHeader ParseHeader();
        void Parse();
    }
}
