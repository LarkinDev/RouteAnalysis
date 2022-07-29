using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteAnalysis
{
    class Segment
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Segment(Point _a, Point _b)
        {
            A = _a;
            B = _b;
        }
    }
}
