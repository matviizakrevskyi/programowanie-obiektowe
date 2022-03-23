using System;

namespace lab_3
{
    class Stack<T>                                            //mb+   class Stack<T> where T : Student
    {
        private T[] _arr = new T[10];
        private int _last = -1;

        public void Push(T item)
        {
            _arr[++_last] = item;
        }

        public T Pop()
        {
            return _arr[_last--];
        }
    }

    class Student 
    {
        public int Egzam { get; set; }

        public T GetReward<T>(T reward)
        {
            if (Egzam > 50)
            {
                return reward;
            }
            else
            {
                return default;
            }
        }
    }
    class Prymus : Student { }

    class Program
    {

        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            Stack<string> strStack = new Stack<string>();
            Stack<Student> stuStack = new Stack<Student>();

            stack.Push(12);
            stack.Push(13);
            stack.Push(14);
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            Student student = new Student() { Egzam = 55 };
            var reward = student.GetReward("Gratulacje");
            Console.WriteLine(reward);

            ValueTuple<string, decimal, int> product = ValueTuple.Create("laptop", 2000m, 3);
            (string, decimal, int) laptop = ("laptop", 2000m, 3);
            Console.WriteLine(product == laptop);
            product.Item1 = "cell phone";
            Console.WriteLine(product);
            (string name, decimal price, int quantity) tuple = laptop;
            Console.WriteLine(tuple.name);
            tuple = (name: "monitor", price: 1200m, quantity: 1);


        }
    }
}
