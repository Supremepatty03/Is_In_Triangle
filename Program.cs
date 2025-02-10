using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_c_;

namespace Lab1_c_
{
    internal class Program
    {
        static void Main()
        {
            Point2D A = new Point2D(0.001, 0.01);
            Point2D B = new Point2D(3.01, 2.01);
            Point2D C = new Point2D(3.01, 0.01);
            Point2D Point = new Point2D(1, 0.01);

            TriangleCheker cheсker = new TriangleCheker();

            Triangle triangle = new Triangle(A, B, C, cheсker);

            bool IsInside = triangle.Contains(Point);

            Console.WriteLine($"Точка принадлежит треугольнику: {IsInside}");

        }
    }
}
C: \Users\User\source\repos\Lab1_c#\