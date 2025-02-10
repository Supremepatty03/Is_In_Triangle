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
            AdditionalInfo.Greetings();
            while (true)
            {
                Point2D A = InputHandler.GetPoint("Введите координаты точки А");
                Point2D B = InputHandler.GetPoint("Введите координаты точки В");
                Point2D C = InputHandler.GetPoint("Введите координаты точки С");
                Point2D Point = InputHandler.GetPoint("Введите координаты искомой точки");

                TriangleCheker cheсker = new TriangleCheker();

                Triangle triangle = new Triangle(A, B, C, cheсker);

                bool IsInside = triangle.Contains(Point);

                Console.WriteLine($"Точка принадлежит треугольнику:{(IsInside ? " Да " : " Нет ")}\n");



                if (!Functionality.Looping()) { break; }

            }
            AdditionalInfo.Farewell();

        }
    }

}
