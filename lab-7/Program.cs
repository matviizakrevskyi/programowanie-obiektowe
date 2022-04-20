using System;

namespace lab_7
{
    delegate double Operation(double a, double b);
    delegate double Power(double a, double b);
    class Program
    {
        static double Addition(double x, double y)
        {
            return x + y;
        }

        static double Mul(double x, double y)
        {
            return x * y;
        }

        static void Main(string[] args)
        {
            Operation add = Addition;
            double result = add.Invoke(3, 5);   //add.Invoke(x, y)  or  add(x, y)
            Console.WriteLine(result);
            add = Mul;
            Console.WriteLine(add.Invoke(3, 5));

            Func<double, double, double> Operator = Addition;
            Func<double, double, double> Div = delegate (double x, double y)
            {
                return x / y;
            };

            Func<int> RandomInt = delegate ()
            {
                return new Random().Next(100);
            };
            Console.WriteLine(RandomInt.Invoke());

            Predicate<int> InRangeFrom0To100 = delegate (int value)
            {
                return value >= 0 && value <= 100;
            };
            Console.WriteLine(InRangeFrom0To100(32));

            Func<int, int, int, bool> InRange = delegate (int value, int min, int max)
            {
                return value >= min && value <= max;
            };

            Action<string> Print = delegate (string s)
            {
                Console.WriteLine(s);
            };

            Print("abc");

            Operation AddLambda = (a, b) => a + b;
            Func<double, double, double> DivLambda = (x, y) =>
            {
                if (y != 0)
                {
                    return x / y;
                }
                else
                {
                    throw new Exception("y is zero!");
                }
            };

            Predicate<string> ThreeCharacters = (s) => s.Length == 3;

            Action<string> PrintUpperLambda = (s) => Console.WriteLine(s.ToUpper());
        }
    }
}
