using System;
using System.Collections.Generic;

namespace lab_6
{
    class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public int CompareTo(Student other)
        {
            if (Name.CompareTo(other.Name) == 0)
            {
                return Ects.CompareTo(other.Ects);
            }
            return Name.CompareTo(other.Name);
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Students Equels");
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Students HashCode");
            return HashCode.Combine(Name, Ects);
        }

        public override string ToString()
        {
            return $"Name = {Name}, Ects = {Ects}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> names = new List<string>();
            names.Add("Ewa");
            names.Add("Karol");
            names.Add("Adam");
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine(names.Contains("Adam"));

            //--
            Console.WriteLine();

            ICollection<Student> students = new List<Student>();
            students.Add(new Student { Name = "Ewa", Ects = 4});
            students.Add(new Student { Name = "Karol", Ects = 8 });
            students.Add(new Student { Name = "Adam", Ects = 1 });
            students.Remove(new Student { Name = "Karol", Ects = 8 });
            foreach (Student student in students)
            {
                Console.WriteLine(student.Name + ", " + student.Ects);
            }
            Console.WriteLine(students.Contains(new Student { Name = "Karol", Ects = 8 }));

            //--
            Console.WriteLine();

            List<Student> list = (List<Student>)students;
            Console.WriteLine(list[0]);
            list.Insert(1, new Student { Name = "Robert", Ects = 12 });
            foreach (Student student in list)
            {
                Console.WriteLine(student.Name + ", " + student.Ects);
            }
            int index = list.IndexOf(new Student { Name = "Adam", Ects = 1 });
            Console.WriteLine("Karol ma pozycje " + index);
            //list.RemoveAt(index);

            //--
            Console.WriteLine("---------------------SET---------------------");
            ISet<string> set = new HashSet<string>();
            set.Add("Ewa");
            set.Add("Karol");
            set.Add("Adam");
            set.Add("Adam");
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }

            //--
            Console.WriteLine();

            ISet<Student> studentsGroup = new HashSet<Student>();
            studentsGroup.Add(new Student { Name = "Ewa", Ects = 4 });
            studentsGroup.Add(new Student { Name = "Adam", Ects = 12 });
            studentsGroup.Add(new Student { Name = "Karol", Ects = 8 });
            studentsGroup.Add(new Student { Name = "Ewa", Ects = 64 });
            foreach (var item in studentsGroup)
            {
                Console.WriteLine(item.Name + ", " + item.Ects);
            }
            studentsGroup.Contains(new Student { Name = "Ewa", Ects = 4 });
            //studentsGroup.Remove(new Student { Name = "Ewa", Ects = 4 });

            //--
            Console.WriteLine();


            studentsGroup = new SortedSet<Student>(studentsGroup);
            foreach (var item in studentsGroup)
            {
                Console.WriteLine(item.Name + ", " + item.Ects);
            }

            studentsGroup.Contains(new Student { Name = "Ewa", Ects = 4 });

            //--
            Console.WriteLine();

            Dictionary<Student, string> phoneBook = new Dictionary<Student, string>();
            phoneBook[list[0]] = "123678456";
            phoneBook[list[1]] = "543647234";
            phoneBook[list[2]] = "123677345";
            Console.WriteLine(phoneBook.Keys);
            if (phoneBook.ContainsKey(new Student { Name = "Adam", Ects = 1 }))
            {
                Console.WriteLine("Jest telefon");
            }
            else
            {
                Console.WriteLine("Brak telefonu");
            }

            foreach (var item in phoneBook)
            {
                Console.WriteLine(item.Key + ", Phone = " + item.Value);
            }

            //--
            Console.WriteLine();

            string[] arr = { "adam", "ewa", "karol", "ewa", "ania", "karol", "adam", "adam", "ewa" };
            //oblicz ile razy wystepuje kazde z imion w tablicy arr
            Dictionary<string, int> counters = new Dictionary<string, int>();
            

        }
    }
}
