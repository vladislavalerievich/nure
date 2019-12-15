//  Создать класс Triangle, разработав следующие элементы класса:
//      a.	Поля:
//          •	double a, b, c; // стороны треугольника
//      b.Конструктор, позволяющий создать экземпляр класса:
//          •	с заданными длинами сторон;
//          •	с одним параметром, задающим дину равностороннего треугольника;
//      c.Методы, позволяющие:
//          •	вывести длины сторон треугольника на экран;
//          •	получить периметр треугольника;

//  В класс Triangle добавить:
//      a.Свойства:
//          •	позволяющие получить-установить длины сторон треугольника(доступные для чтения и записи);
//          •	позволяющее установить, существует ли треугольник с данными длинами сторон(доступное только для чтения).
//      b.Индексатор, позволяющий по индексу 0 получить значение  угла,  противостоящего стороне а, по индексу 1 – значение угла, противостоящего  стороне b и по индексу 2 – значение угла, противостоящего  стороне c(только для чтения). При других  значениях индекса выдавать «-1».
//       (Чтобы найти углы alpha ,beta надо воспользоваться теоремой косинусов. 
//       Третий угол сразу находится из правила, что сумма всех трёх углов должна быть равна 180°).

using System;

namespace Nure {
    internal class Triangle {
        private double a, b, c;

        public Triangle (double x, double y, double z) {
            a = x;
            b = y;
            c = z;
        }

        public Triangle (double x) {
            a = x;
            b = x;
            c = x;
        }

        public void Print () {
            Console.WriteLine ($"Side a - {a.ToString()}; Side b - {b.ToString()}; Side c - {c.ToString()}");
        }

        public double GetPerimeter () {
            return a + b + c;
        }

        public double GetArea () {
            double perimeter = GetPerimeter ();
            double halfPerimeter = perimeter / 2;
            double area = Math.Sqrt (halfPerimeter * (halfPerimeter - a) * (halfPerimeter - b) * (halfPerimeter - c));
            return Math.Round (area, 2);
        }

        public double A {
            get => a;
            set {
                if (value > 1) {
                    a = value;
                } else {
                    throw new System.ArgumentException ("Side can't be less than 1");
                }
            }
        }

        public double B {
            get => b;
            set {
                if (value > 1) {
                    b = value;
                } else {
                    throw new System.ArgumentException ("Side can't be less than 1");
                }
            }
        }

        public double C {
            get => c;
            set {
                if (value > 1) {
                    c = value;
                } else {
                    throw new System.ArgumentException ("Side can't be less than 1");
                }
            }
        }

        public bool CheckValidity {
            get {
                if (a + b <= c || a + c <= b || b + c <= a) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        public double this [int index] {
            get {
                if (CheckValidity) {
                    // TODO later: memoization 
                    double angleA = Math.Acos ((Math.Pow (b, 2) + Math.Pow (c, 2) - Math.Pow (a, 2)) / (2 * b * c));
                    double angleB = Math.Acos ((Math.Pow (a, 2) + Math.Pow (c, 2) - Math.Pow (b, 2)) / (2 * a * c));
                    double angleC = 180 - (angleA + angleB);

                    switch (index) {
                        case 0:
                            return angleA;
                        case 1:
                            return angleB;
                        case 2:
                            return angleC;
                        default:
                            return -1;
                    }

                } else {
                    return -1;
                }
            }
        }

        public bool СheckSimularity (Triangle triangle) {
            double[] t1 = new double[3] { a, b, c };
            double[] t2 = new double[3] { triangle.a, triangle.b, triangle.c };

            Array.Sort (t1);
            Array.Sort (t2);

            return (t1[0] / t2[0]) == (t1[1] / t2[1]) && (t1[1] / t2[1]) == (t1[2] / t2[2]);
        }
    }

    internal class Program {
        private static void Main (string[] args) {
            Triangle triangle = new Triangle (5, 10, 15);
            // logs sides of a triangle
            triangle.Print ();

            double p = triangle.GetPerimeter ();
            Console.WriteLine ($"Triangle Perimeter: {p};");

            double a = triangle.GetArea ();
            Console.WriteLine ($"Triangle Area: {a};");

            Triangle triangle1 = new Triangle (0) {
                A = 30,
                B = 10,
                C = 20
            };

            bool isTriangleValid = triangle1.CheckValidity;
            Console.WriteLine ($"Is triangle valid: {isTriangleValid}");

            double alpha = triangle1[0];
            double beta = triangle1[1];
            double gamma = triangle1[2];

            Console.WriteLine ($"Angle a is: {alpha} degrees;");
            Console.WriteLine ($"Angle b is: {beta} degrees;");
            Console.WriteLine ($"Angle c is: {gamma} degrees");

            double nonExistingAngle = triangle1[3];
            Console.WriteLine ($"Non existing angle is: {nonExistingAngle};");

            var s = triangle1.СheckSimularity (triangle);
            Console.WriteLine ($"Is triangle1 simular to triangle: {s}");
            Console.ReadKey ();
        }
    }
}