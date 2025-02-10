using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_c_
{
    public class Objects
    {
        public struct Point2D
        {
            public double x { get; } 
            public double y { get; }

            public Point2D(double x, double y)
            {
                this.x = x;
                this.y = y;
            }


            public double Distance(Point2D other)
            {
                return Math.Sqrt((x - other.x) * (x - other.x) + (y - other.y) * (y - other.y));
            }
        }

        public interface IChecker
        {
            bool Check(Point2D other, Triangle triangle);
        }
        public class Triangle
        {
            public Point2D A { get; }
            public Point2D B { get; }
            public Point2D C { get; }

            private readonly IChecker checker;

            public Triangle (Point2D a, Point2D b, Point2D c, IChecker checker)
            {
                A = a;
                B = b;
                C = c;
                this.checker = checker;

            }
            public bool Contains(Point2D point)
            {
               return checker.Check(point, this);
            }

        }
        public class TriangleCheker : IChecker
        {
            public bool Check (Point2D point, Triangle triangle)
            {
                double Area (Point2D p1, Point2D p2, Point2D p3)
                {
                    double a = p1.Distance(p2);
                    double b = p2.Distance(p3);
                    double c = p1.Distance(p3);
                    double perimetr = (a + b + c) / 2;

                    return Math.Sqrt(perimetr * (perimetr - a) * (perimetr - b) * (perimetr - c));
                }
                double AreaNoP = Area (triangle.A, triangle.B, triangle.C);

                double AreaP1 = Area(point, triangle.B, triangle.C);
                double AreaP2 = Area(triangle.A, point, triangle.C);
                double AreaP3 = Area(triangle.A, triangle.B, point);

                return Math.Abs(AreaNoP - (AreaP1 + AreaP2 + AreaP3)) < 1e-4;  //1 * 10^-7

            }
        }


    }
}
