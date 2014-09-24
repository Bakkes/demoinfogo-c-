using CSGODemoParser.Demo.Parser;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo
{
    public class DemoFile
    {
        public DemoHeader Header { get; private set; }
        private IDemoParser parser;

        public DemoFile(IDemoParser parser)
        {
            this.parser = parser;
            this.Header = parser.ParseHeader();
        }

        
    }
}
