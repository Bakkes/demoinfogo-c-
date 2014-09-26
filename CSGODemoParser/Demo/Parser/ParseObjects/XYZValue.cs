using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser.ParseObjects
{
    abstract class XYZValue
    {
        public float x, y, z;

        public XYZValue()
        {
            x = y = z = 0.0f;
        }

        public string ToString()
        {
            return x + ", " + y + ", " + z;
        }
    }
}
