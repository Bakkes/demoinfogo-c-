using CSGODemoParser.Demo;
using CSGODemoParser.Demo.Parser;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class Program
    {
        static void Main(string[] args)
        {
            FullParser qp = new FullParser(new CSGODemoReader("X:/draft.dem")); //match730_003030909602041430032_0894226252_185 - Copy (5) - Copy.dem
            DemoFile d = new DemoFile(qp);

            Console.ReadLine();
        }
    }
}
