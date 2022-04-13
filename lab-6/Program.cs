using System;
using System.Collections.Generic;

namespace lab_6
{
    class Student
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Students Equels");
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
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

            //
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
            List<Student> list = (List<Student>)students;
            Console.WriteLine(list[0]);
            list.Insert(1, new Student { Name = "Robert", Ects = 12 });
            foreach (Student student in list)
            {
                Console.WriteLine(student.Name + ", " + student.Ects);
            }
            int index = list.IndexOf(new Student { Name = "Adam", Ects = 1 });
            Console.WriteLine("Karol ma pozycje " + index);
            list.RemoveAt(index);

            ISet<string> set = new HashSet<string>();
        }
    }
}
