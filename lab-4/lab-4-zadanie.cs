using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    //class App
    //{
    //    public static void Main(string[] args)
    //    {
    //        //-EX - 1
    //        Exercise1 exercise1 = new();
    //        (int, int) point1 = (2, 4);
    //        Direction4 dir = Direction4.UP;
    //        (int, int) screensize = (200, 100);
    //        Console.WriteLine(exercise1.NextPoint(dir, point1, screensize));


    //        Console.WriteLine();
    //        //--EX - 2
    //        Exercise2 exercise2 = new();
    //        Console.WriteLine(exercise2.direction);


    //        Console.WriteLine();
    //        //--EX - 3
    //        Car[] _cars = new Car[]
    //        {
    //            new Car(),
    //            new Car(Model: "Fiat", true),
    //            new Car(),
    //            new Car(Power: 100),
    //            new Car(Model: "Fiat", true),
    //            new Car(Power: 125),
    //            new Car()
    //        };
    //        Console.WriteLine("Count of cars: " + Exercise3.CarCounter(_cars));


    //        Console.WriteLine();
    //        //--EX - 4
    //        Student[] students = {
    //            new Student("Kowal","Adam", 'A'),
    //            new Student("Nowak","Ewa", 'A')
    //        };
    //        Exercise4.AssignStudentId(students);
    //        foreach (var st in students)
    //        {
    //            Console.WriteLine($"Student: {st.LastName} {st.FirstName} Group: {st.Group} StudentID: {st.StudentId}");
    //        }
    //    }
    //}

    enum Direction8
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_LEFT,
        DOWN_LEFT,
        UP_RIGHT,
        DOWN_RIGHT
    }

    enum Direction4
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    //Cwiczenie 1
    //Zdefiniuj metodę NextPoint, która powinna zwracać krotkę ze współrzędnymi piksela przesuniętego jednostkowo w danym kierunku względem piksela point.
    //Krotka screenSize zawiera rozmiary ekranu (width, height)
    //Przyjmij, że początek układu współrzednych (0,0) jest w lewym górnym rogu ekranu, a prawy dolny ma współrzęne (witdh, height) 
    //Pzzykład
    //dla danych wejściowych 
    //(int, int) point1 = (2, 4);
    //Direction4 dir = Direction4.UP;
    //var point2 = NextPoint(dir, point1);
    //w point2 powinny być wartości (2, 3);
    //Jeśli nowe położenie jest poza ekranem to metoda powinna zwrócić krotkę z point
    class Exercise1
    {
        public (int, int) NextPoint(Direction4 direction, (int, int) point, (int, int) screenSize)
        {
            switch (direction)
            {
                case Direction4.UP:
                    if (point.Item2 - 1 >= 0)
                        return (point.Item1, point.Item2 - 1);
                    return point;
                case Direction4.DOWN:
                    if (point.Item2 + 1 <= screenSize.Item2)
                        return (point.Item1, point.Item2 + 1);
                    return point;
                case Direction4.LEFT:
                    if (point.Item1 - 1 >= 0)
                        return (point.Item1 - 1, point.Item2);
                    return point;
                case Direction4.RIGHT:
                    if (point.Item1 + 1 >= 0)
                        return (point.Item1 + 1, point.Item2);
                    return point;
            }



            return point;
        }
    }
    //Cwiczenie 2
    //Zdefiniuj metodę DirectionTo, która zwraca kierunek do piksela o wartości value z punktu point. W tablicy screen są wartości
    //pikseli. Początek układu współrzędnych (0,0) to lewy górny róg, więc punkt o współrzęnych (1,1) jest powyżej punktu (1,2) 
    //Przykład
    // Dla danych weejsciowych
    // static int[,] screen =
    // {
    //    {1, 0, 0},
    //    {0, 0, 0},
    //    {0, 0, 0}
    // };
    // (int, int) point = (1, 1);
    // po wywołaniu - Direction8 direction = DirectionTo(screen, point, 1);
    // w direction powinna znaleźć się stała UP_LEFT
    class Exercise2
    {
        static int[,] screen =
        {
            {1, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        private static (int, int) point = (1, 1);

        public Direction8 direction = DirectionTo(screen, point, 1);

        public static Direction8 DirectionTo(int[,] screen, (int, int) point, int value)
        {
            Direction8 dr = new();

            if (screen[point.Item1 + 1, point.Item2] == value)
                dr = Direction8.DOWN;
            if (screen[point.Item1 + 1, point.Item2 - 1] == value)
                dr = Direction8.DOWN_LEFT;
            if (screen[point.Item1 + 1, point.Item2 + 1] == value)
                dr = Direction8.DOWN_RIGHT;
            if (screen[point.Item1, point.Item2 - 1] == value)
                dr = Direction8.LEFT;
            if (screen[point.Item1, point.Item2 + 1] == value)
                dr = Direction8.RIGHT;
            if (screen[point.Item1 - 1, point.Item2] == value)
                dr = Direction8.UP;
            if (screen[point.Item1 - 1, point.Item2 - 1] == value)
                dr = Direction8.UP_LEFT;
            if (screen[point.Item1 - 1, point.Item2 + 1] == value)
                dr = Direction8.UP_RIGHT;

            return dr;
        }
    }

    //Cwiczenie 3
    //Zdefiniuj metodę obliczającą liczbę najczęściej występujących aut w tablicy cars
    //Przykład
    //dla danych wejściowych
    // Car[] _cars = new Car[]
    // {
    //     new Car(),
    //     new Car(Model: "Fiat", true),
    //     new Car(),
    //     new Car(Power: 100),
    //     new Car(Model: "Fiat", true),
    //     new Car(Power: 125),
    //     new Car()
    // };
    //wywołanie CarCounter(Car[] cars) powinno zwrócić 3
    record Car(string Model = "Audi", bool HasPlateNumber = false, int Power = 100);

    class Exercise3
    {
        public static int CarCounter(Car[] cars)
        {
            Dictionary<Car, int> dict_cars = new();
            foreach (var car in cars)
            {
                if (dict_cars.ContainsKey(car))
                { 
                    dict_cars[car]++;
                }
                else
                {
                    dict_cars.Add(car, 1);
                }
            }

            List<int> list_count = new();
            foreach (var car_count in dict_cars.Values)
            {
                list_count.Add(car_count);
            }

            return list_count.Max();
        }
    }

    record Student(string LastName, string FirstName, char Group, string StudentId = "");
    //Cwiczenie 4
    //Zdefiniuj metodę AssignStudentId, która każdemu studentowi nadaje pole StudentId wg wzoru znak_grupy-trzycyfrowy-numer.
    //Przykład
    //dla danych wejściowych
    //Student[] students = {
    //  new Student("Kowal","Adam", 'A'),
    //  new Student("Nowak","Ewa", 'A')
    //};
    //po wywołaniu metody AssignStudentId(students);
    //w tablicy students otrzymamy:
    // Kowal Adam 'A' - 'A001'
    // Nowal Ewa 'A'  - 'A002'
    //Należy przyjąc, że są tylko trzy możliwe grupy: A, B i C
    class Exercise4
    {
        public static void AssignStudentId(Student[] students)
        {
            int count = 0;
            foreach (var st in students)
            {
                count++;
                if (count.ToString().Length == 1)
                {
                    students[count - 1] = new Student(st.LastName, st.FirstName, st.Group, $"{st.Group}00{count}");
                }
                if (count.ToString().Length == 2)
                {
                    students[count - 1] = new Student(st.LastName, st.FirstName, st.Group, $"{st.Group}0{count}");
                }
                if (count.ToString().Length == 3)
                {
                    students[count - 1] = new Student(st.LastName, st.FirstName, st.Group, $"{st.Group}{count}");
                }
            }

        }
    }
}
