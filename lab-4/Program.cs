using System; 

namespace lab_4
{
    enum Degree
    {
        A = 50,
        B = 45,
        C = 40,
        D = 35,
        E = 30,
        F = 20
    }

    record _Student (string Name, int Ects, bool Egzam);
    class ClassStrudent
    {
        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Degree degree = Degree.F;
            Console.WriteLine((int)degree);
            string[] vs = Enum.GetNames<Degree>();
            Console.WriteLine(string.Join(", ", vs));
            Degree[] degrees = Enum.GetValues<Degree>();
            foreach (Degree d in degrees)
            {
                Console.WriteLine($"{d} {(int)d}");
            }

            Console.WriteLine("Wpisz ocene");
            string str = Console.ReadLine();
            try
            {
                Degree studentDegree = Enum.Parse<Degree>(str);
                Console.WriteLine("Wpisales " + studentDegree);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Wpisales nieznane ocene");
            }

            //switch (degree)
            //{
            //    case Degree.A:
            //        Console.WriteLine("Dostales ocene A");
            //        break;
            //    case Degree.B:
            //        break;
            //}

            double ocena = degree switch
            {
                Degree.A => 5.0,
                Degree.B => 4.5,
                Degree.C or Degree.D => 4.0,
                _ => 3.0
            };
            Console.WriteLine(ocena);


            ////---
            //(int, int) point1 = (2, 4);
            //Direction4 dir = Direction4.UP;
            //var point2 = NextPoint(dir, point1);
            //w point2 powinny być wartości (2, 3);
            //---



            _Student student = new _Student("Karol", 12, true);
            Console.WriteLine(student);
            if (student == new _Student("Karol", 13, true))
            {
                Console.WriteLine("Identyczni");
            }
            else
            {
                Console.WriteLine("Rozni");
            }
            _Student[] students =
            {
                new _Student("Karol", 12, true),
                new _Student("Ewa", 17, false),
                new _Student("Robert", 19, true),
                new _Student("Ania", 15, false)
            };

            foreach (_Student st in students)
            {
                Console.WriteLine(
                    st.Name + 
                    st switch
                    {
                        { Ects: >= 17, Egzam: true} => "Zaliczyl",
                        _ => "Niezaliczyl"
                    }
                    );

                //Rownowazny kod
                //if (st.Ects >= 17 && st.Egzam)
                //{
                //}
            }



            Console.WriteLine();
            //-
            Exercise1 exercise1 = new();
            (int, int) point1 = (2, 4);
            Direction4 dir = Direction4.UP;
            (int, int) screensize = (200, 100);
            Console.WriteLine(exercise1.NextPoint(dir, point1, screensize));


            Console.WriteLine();
            //--
            Exercise2 exercise2 = new();
            Console.WriteLine(exercise2.direction);


            Console.WriteLine();
            //--
            Car[] _cars = new Car[]
            {
                 new Car(),
                 new Car(Model: "Fiat", true),
                 new Car(),
                 new Car(Power: 100),
                 new Car(Model: "Fiat", true),
                 new Car(Power: 125),
                 new Car()
            };
            Console.WriteLine("Count of cars: " + Exercise3.CarCounter(_cars));


            Console.WriteLine();
            //--
            Student[] _students = {
              new Student("Kowal","Adam", 'A'),
              new Student("Nowak","Ewa", 'A'),
              new Student("Nowak", "Jan", 'B'),
              new Student("Kowal", "Karol", 'A'),
              new Student("Nowak", "Robert", 'C')
            };
            Exercise4.AssignStudentId(_students);
            foreach (var st in _students)
            {
                Console.WriteLine($"Student: {st.LastName} {st.FirstName} Group: {st.Group} StudentID: {st.StudentId}");
            }
        }
    }
}
