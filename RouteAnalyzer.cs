using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteAnalysis
{
    class RouteAnalyzer
    {
        void changeCoordinates(string direction, double val, ref double x, ref double y,ref double path)
        {
            switch (direction.ToUpper())
            {
                case "W": x -= val;break;
                case "E": x += val;break;
                case "S": y -= val; break;
                case "N": y += val; break;
            }
            path += val;
        }
         List<Segment> getSegments(string s)
        {
            var strArr = s.Split(',');
            var points = new List<Point>();
            var segments = new List<Segment>();
            points.Add(new Point(0, 0, 0));
            foreach (var str in strArr)
            {
                var val = double.Parse(str.Substring(0, str.Length - 1));
                var dir = str[str.Length - 1].ToString();
                double x = points[points.Count - 1].X;
                double y = points[points.Count - 1].Y;
                double path = points[points.Count - 1].Path;
                changeCoordinates(dir, val, ref x, ref y, ref path);
                var p = new Point(x, y, path);
                points.Add(p);
                segments.Add(new Segment(points[points.Count - 2], p));
            }
            return segments;
        }
        bool isBelongSegment(Segment l, Point p)
        {
            double AX = l.B.X > l.A.X ? l.A.X : l.B.X;
            double BX = l.B.X > l.A.X ? l.B.X : l.A.X;
            double AY = l.B.Y > l.A.Y ? l.A.Y : l.B.Y;
            double BY = l.B.Y > l.A.Y ? l.B.Y : l.A.Y;

            if ((p.Y == l.A.Y && p.X >= AX && p.X < BX) || (p.X == l.A.X && p.Y >= AY && p.Y <= BY))
            {
                return true;
            }
            return false;
        }
        Point Intersection(Segment l1, Segment l2)
        {
            double xo = l1.A.X, yo = l1.A.Y;
            double p = l1.B.X - l1.A.X, q = l1.B.Y - l1.A.Y;

            double x1 = l2.A.X, y1 = l2.A.Y;
            double p1 = l2.B.X - l2.A.X, q1 = l2.B.Y - l2.A.Y;
            double x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) /
         (q * p1 - q1 * p);
            double y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) /
                (p * q1 - p1 * q);


            return new Point(x, y, -1);
        }
        double getPathToCrossPoint(Segment ln, Point p)
        {
            double newPath = 0;
            if (ln.B.X == p.X && ln.A.X == p.X)
            {
                newPath = ln.B.Path - Math.Abs(ln.B.Y - p.Y);
            }
            else
            {
                newPath = ln.B.Path - Math.Abs(ln.B.X - p.X);
            }
            return newPath;
        }
        List<Point> getPoints(List<Segment> lines1, List<Segment> lines2, double a, double b)
        {
            var podoubles = new List<Point>();
            foreach (var ln1 in lines1)
            {
                foreach (var ln2 in lines2)
                {
                    var p = Intersection(ln1, ln2);
                    if (isBelongSegment(ln1, p) && isBelongSegment(ln2, p))
                    {
                        double path1 = getPathToCrossPoint(ln1, p);
                        double path2 = getPathToCrossPoint(ln2, p);
                        if (path1 >= a && path1 <= b && path2 >= a && path2 <= b)
                        {
                            podoubles.Add(p);
                        }
                    }
                }
            }
            return podoubles;
        }
        public void StartAnalysis(string path)
        {
            var str = File.ReadAllLines(path);
            var a = double.Parse(str[0].Split('-')[0]);
            var b = double.Parse(str[0].Split('-')[1]);
            var lines1 = getSegments(str[1]);
            var lines2 = getSegments(str[2]);
            lines1.RemoveAt(0);
            lines2.RemoveAt(0);
            var crossPoints = getPoints(lines1, lines2, a, b);
            if (crossPoints.Count == 0)
            {
                Console.WriteLine("Intersection not found!");
            }
            else
            {
                Console.WriteLine("Points count: " + crossPoints.Count);
                Console.WriteLine("First point:\nX: " + crossPoints[0].X + " Y: " + crossPoints[0].Y);
            }
            
            
        }
    }
}
