using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteAnalysis
{
    class Point
    {
        public double X { get; }
        public double Y { get; }
        public double Path { get; }

        public Point(double x, double y, double path)
        {
            X = x;
            Y = y;
            Path = path;
        }
    }
}
