using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_8
{
    record Student (int id, string Name, int Ects);
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 4, 5, 7, 3, 2, 8, 9 };
            IEnumerable<int> evenNumbers =
                from n in ints
                where n % 2 == 0     //where !(n % 2 == 0)
                select n;

            Console.WriteLine(string.Join(", ", evenNumbers));
            //

            IEnumerable<int> fiveNumbers =
                from n in ints
                where n > 5
                select n;
            Console.WriteLine(string.Join(", ", fiveNumbers));



            Predicate<int> intPredicate = n =>
            {
                Console.WriteLine("wywolanie predykatu dla " + n);
                return n % 2 == 0;
            };

            evenNumbers =
                from n in ints
                where intPredicate.Invoke(n)
                select n;

            //evenNumbers =
            //    from n in evenNumbers
            //    where n > 5
            //    select n * 2;

            Console.WriteLine("wywolanie ewaluacji wyrazenia LINQ");
            Console.WriteLine(string.Join(", ", evenNumbers));
            Console.WriteLine(evenNumbers.Sum());
            Console.WriteLine(evenNumbers.Average());
            Console.WriteLine(evenNumbers.Count());
            Console.WriteLine(evenNumbers.Max());
            Console.WriteLine(evenNumbers.Min());

            //
            Console.WriteLine();
            //


            Student[] students =
            {
                new Student(1, "Ewa", 67),
                new Student(2, "Karol", 67),
                new Student(3, "Ewa", 63),
                new Student(4, "Ania", 67),
                new Student(5, "Karol", 37)
            };

            IEnumerable<string> enumerable =
                from s in students
                orderby s.Ects orderby s.Name descending
                select s.Name + " " + s.Ects;
            Console.WriteLine(string.Join("\n", enumerable));

            Console.WriteLine(string.Join(", ",
                from n in ints
                orderby n descending
                select n
                ));

            //Array.Sort(ints);
            Console.WriteLine(string.Join(", ", ints.OrderByDescending(i => i)));

            Console.WriteLine(string.Join(", ", students.OrderByDescending(s => s.Name).ThenBy(s => s.Ects)));

            IEnumerable<IGrouping<string, Student>> studentNameGroup =
                from s in students
                group s by s.Name;

            foreach(var item in studentNameGroup)
            {
                Console.WriteLine(item.Key + " " + string.Join(", ", item));
            }

            IEnumerable<(string Key, int)> NameCounter = 
                from s in students
                group s by s.Name into groupItem
                select (groupItem.Key, groupItem.Count());

            Console.WriteLine(string.Join(", ", NameCounter));

            string str = "ala ma kota ala lubi koty karol lubi psy";
            //ile razy wystepuje kazde 
            //IEnumerable<(string, int)> strCounter =
            //    from s in str


            evenNumbers = ints.Where(i => i % 2 == 0).Select(i => i + 2);
            (int id, string Name) p = students
                .Where(s => s.Ects > 20)
                .OrderBy(s => s.id)
                .Select(s => (s.id, s.Name))
                .First(s => s.Name.StartsWith("A"));
            Console.WriteLine(p);

            int[] powerInts = Enumerable
                .Range(0, ints.Length)
                .Select(i => ints[i] * ints[i])
                .ToArray();

            Console.WriteLine(string.Join(", ", powerInts));

            Random random = new Random();
            random.Next(5); //zwroca losowa liczbe od 0 do 4
            //wygeneruje tablice 100 liczb losowych od 0 do 9
            //za pomoca Enumerable

            //IEnumerable<int> intrandom =

            int[] randomInts = Enumerable.Range(0, 100).Select(i => random.Next(10)).ToArray();
            int page = 0;
            int size = 3;
            List<Student> pageStudent = students.Skip(page * size).Take(size).ToList();
            Console.WriteLine(string.Join(", ", pageStudent));
                

        }
    }
}
