//      Точка, треугольник, треугольная пирамида

//      а) Наследование. Определите иерархию  классов (см. варианты), связанных отношением наследования.
//           Определите в этих классах методы, которые перемещает фигуру по плоскости, возвращают ее площадь, 
//           периметр (для объемных  фигур – периметр основания), и строку символов, отражающую имя класса и состояние объекта. 
//           Продемонстрируйте  работу   с классами.

//      б*) Добавьте абстрактный класс «Фигура» в вашу систему классов, 
//           включите в него все методы прочих классов.Создайте  массив ссылок на базовый класс «Фигура»,
//           заполните его  различными фигурами, продемострируйте работу   с методами  различных элементов массива.

using System;

namespace Nure {
    internal abstract class Shape {
        public abstract void Move ();
        public abstract string Print ();
        public abstract double Area ();
        public abstract double Perimeter ();
    }

    internal class Point : Shape {
        protected double x;
        protected double y;

        public Point (double a, double by) {
            x = a;
            y = by;
        }
        public Point () {
            x = 0;
            y = 0;
        }

        public override void Move () {
            Console.WriteLine ("Введите на какое число переместить координату Х ");
            x += Convert.ToInt64 (Console.ReadLine ()); //считываемая строка в формате string по умолчанию не может преобразовываться в число, потому нужна конвертация
            Console.WriteLine ("Введите на какое число переместить координату Y ");
            y += Convert.ToInt64 (Console.ReadLine ());
        }

        public override string Print () {
            return (GetType ().Name + "\nX = " + x + "\nY = " + y);
        }

        public override double Area () {
            return 0;
        }

        public override double Perimeter () {
            return 0;
        }

        public double X => x;

        public double Y => y;

    }

    internal class Triangle : Point {
        public Point a;
        public Point b;
        public Point c;

        public Triangle () {
            a = new Point ();
            b = new Point ();
            c = new Point ();
        }

        public Triangle (Point x, Point y, Point z) {
            a = x;
            b = y;
            c = z;
        }

        public override void Move () {
            Console.WriteLine ("Точка A");
            a.Move ();
            Console.WriteLine ("Точка B");
            b.Move ();
            Console.WriteLine ("Точка C");
            c.Move ();
        }

        public override double Area () {
            return Math.Abs ((b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y)) / 2;
        }

        public override double Perimeter () {
            return Math.Sqrt (Math.Pow (b.X - a.X, 2) + Math.Pow ((b.Y - a.Y), 2)) +
                Math.Sqrt (Math.Pow (c.X - a.X, 2) + Math.Pow (c.Y - a.Y, 2)) +
                Math.Sqrt (Math.Pow ((c.X - b.X), 2) + Math.Pow ((c.Y - b.Y), 2));
        }

        public override string Print () //вывод состояния объекта
        {
            return GetType ().Name + "\nA = " + a.Print () + "\nB = " + b.Print () + "\nC = " + c.Print ();
        }
    }

    internal class TriangularPyramid : Triangle //класс пирамида, состоящий из треугольников
    {
        private readonly Point h;

        public TriangularPyramid (Point x, Point y, Point z, Point hh) //конструктор с параметрами
        {
            a = x;
            b = y;
            c = z;
            h = hh;
        }

        public override void Move () //переопределенный метод сдвига
        {
            Console.WriteLine ("Основание треугольника, сторона a");
            a.Move ();
            Console.WriteLine ("Основание треугольника, сторона  b");
            b.Move ();
            Console.WriteLine ("Основание треугольника, сторона  c");
            c.Move ();
            Console.WriteLine ("Вершина треугольника, сторона  h");
            h.Move ();
        }

        public override double Area () //переопределенный метод нахождения площади
        {
            return a.Area () + b.Area () + c.Area () + h.Area ();
        }

        public override double Perimeter () //находим периметр, возвращает значение
        {
            // TODO. Возвращаемое значение неверно. (У меня не было времени искать правильную формулу.) 
            return a.Perimeter () + b.Perimeter () + c.Perimeter () - h.Perimeter ();
        }

        public override string Print () //переопределнный вывод состояния объекта
        {
            return GetType ().Name + "\nA = " + a.Print () + "\nB = " + b.Print () + "\nC = " + c.Print () + "\nH = " + h.Print ();
        }
    }

    internal class Program {
        private static void Main (string[] args) {
            Shape[] a = new Shape[6];

            Point point1 = new Point (1, 2);
            Point point2 = new Point (13, 22);
            Point point3 = new Point (17, 23);
            a[0] = point1;
            a[1] = point2;
            a[2] = point3;

            point1.Move ();
            Console.WriteLine ("*********************");
            Console.WriteLine (point1.Print ());
            Console.WriteLine ("*********************");
            Triangle triangle1 = new Triangle (point1, point2, point3);
            Triangle triangle2 = new Triangle (point2, point1, point3);
            Triangle triangle3 = new Triangle (point3, point2, point1);

            a[3] = triangle1;
            a[4] = triangle2;
            a[5] = triangle3;

            triangle1.Move ();
            Console.WriteLine ("*********************");
            Console.WriteLine (triangle1.Print ());
            Console.WriteLine ("*Площадь треугольника*");
            Console.WriteLine (triangle1.Area ());
            Console.WriteLine ("*Периметр Треугольника*");
            Console.WriteLine (triangle1.Perimeter ());
            //создаем пирамиду из 1 треугольника как основание и 1 точка как вершина
            TriangularPyramid pyramid = new TriangularPyramid (point1, point2, point3, new Point (40, 50));

            a[6] = pyramid;
            Console.WriteLine ("*********************");
            pyramid.Move ();
            Console.WriteLine ("*********************");
            Console.WriteLine (pyramid.Print ());
            Console.WriteLine ("***Площадь пирамиды**");
            Console.WriteLine (pyramid.Area ());
            Console.WriteLine ("**Периметр пирамиды**");
            Console.WriteLine (pyramid.Perimeter ());
        }
    }
}