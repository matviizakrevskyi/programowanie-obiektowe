using System;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    class Person
    {
        private string name;
        public string Name 
        { 
            get
            {
                return name;    
            }
            set
            {
                name = value;
            }
        }

        public Person() { }

        public Person(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public string GetName(int i)
        {
            return name + " " + i;
        }
    }

    class loshok : Person
    {
        private Person kek;

        public string LastName { get; set; }

        public (string, string) GetNameandLastName()
        {
            return (kek.Name, this.LastName);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person Vlad = new Person();
            Person Matvei = new Person("LOL");
            //set
            Vlad.Name = "Vlad";

            loshok kukold = new loshok();
            Console.WriteLine("fgdf" + kukold.Name + " " + kukold.LastName);
            Console.WriteLine($"sfdsf {kukold.Name} {kukold.LastName}");


            //get
            Console.WriteLine(Vlad.Name);
            string kikki = Vlad.Name;

            string ihuyudy = Vlad.GetName();

            string a = "lol";

            a = a ?? "";
            Console.WriteLine(a);
            a = "lol";

            a = (a is null) ? "" : a;
            Console.WriteLine(a);
            a = "lol";

            a = a == null ? "" : a;
            Console.WriteLine(a);
            a = "lol";

            if (a == null) a = "";
            Console.WriteLine(a);
            //a = "lol";

            Console.WriteLine();
            Console.WriteLine();
            //Console.WriteLine("Hello World!");


            string[] names = { "Ewa", "Karol", "Adam" };
            Random random = new Random();
            int count = 3;
            IEnumerable<int> randomInts = Enumerable.Range(0, count).Select(i => random.Next(3));
            



            foreach (var item in randomInts)
                Console.WriteLine(item);

            List<string> randomNames = new List<string>();
            foreach (int i in randomInts)
                randomNames.Add(names[i]);

            foreach (string s in randomNames)
                Console.WriteLine(s);


            string[] jikm = { "0", "1", "0", "1", "0" };
            Console.WriteLine(jikm.Contains("4"));




            Console.WriteLine();
            Console.WriteLine();

            //int funF(int n)
            //{
            //    if (n < 2) return n;
            //    else return n - funG(n);
            //}

            //int funG(int n)
            //{
            //    if (n < 2) return n;
            //    else return funF(n - 1);
            //}

            //Console.WriteLine(funF(3));
            //Console.WriteLine(funF(4));
            //Console.WriteLine(funF(5));
            //Console.WriteLine(funF(6));

            //Console.WriteLine(true | false);

            //int[] a = { 1, 2, 3, 4 };

            //throw new Exception();
            //throw new StackMemoryException();
            //throw new NullPointerException();

            //int x = 3;
            //int y = 4;
            //if (x = y) Console.WriteLine("x = " + x);
            //else Console.WriteLine("y = " + y);



            //Console.WriteLine(4 < 4);


            int[] aa = { 1, 2, 3, 4 };
            int[] bb = { 5, 6, 7, 8 };

            Stack<int> S = new Stack<int>(aa);
            Queue<int> Q = new Queue<int>(bb);

            S.Pop(); Q.Dequeue();
            S.Push(S.Peek()); Q.Dequeue();
            S.Push(S.Peek());
            S.Pop(); Q.Dequeue();
            Q.Enqueue(Q.Peek()); S.Pop();
            S.Pop(); Q.Dequeue();

            foreach (var item in S)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            foreach (var item in Q)
            {
                Console.WriteLine(item);
            }

        }
    }
    
}
