using System;
using System.Collections.Generic;

namespace lab_7_zadanie
{
    delegate String IntFormatter(int a);

    delegate double operation(double a, double b);

    class Program
    {
        public static void Main(string[] args)
        {
            int points = 0;
            try
            {
                IntFormatter formatter = HexFormatter();
                if (formatter.Invoke(15).Equals("F") && formatter.Invoke(5).Equals("5") && formatter.Invoke(11).Equals("B"))
                {
                    Console.WriteLine("Zadanie 1: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 1: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 1: 0");
            }
            try
            {
                operation op = AddOperation();
                if (op is operation && op.Invoke(1, 2) == 3 && op.Invoke(100, 3) == 103)
                {
                    Console.WriteLine("Zadanie 2: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 2: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 2: 0");
            }
            try
            {
                double result = Calculate(delegate (double a, double b) { return a * b; }, 2, 4);
                if (result == 8)
                {
                    Console.WriteLine("Zadanie 3: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 3: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 3: 0");
            }
            try
            {
                string result = Repeat().Invoke("AA", 3);
                if (result.Equals("AAAAAA") && Repeat().Invoke("-", 2).Equals("--") && Repeat().Invoke("", 3).Equals(""))
                {
                    Console.WriteLine("Zadanie 4: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 4: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 5: 0");
            }
            try
            {
                if (StringConsumer() is Action<string>)
                {
                    Console.WriteLine("Zadanie 5: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 5: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 5: 0");
            }
            try
            {
                if (DoubleFunction() is Func<double, double> && DoubleFunction().Invoke(4) == 16 && DoubleFunction().Invoke(8) == 64)
                {
                    Console.WriteLine("Zadanie 6: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 6: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 6: 0");
            }
            try
            {
                if (IsPhoneNumber() is Predicate<string> && IsPhoneNumber().Invoke("123456789") && !IsPhoneNumber().Invoke("44567ad") && !IsPhoneNumber().Invoke("12345678967"))
                {
                    Console.WriteLine("Zadanie 7: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 7: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 7: 0");
            }
            try
            {
                List<string> data = new List<string>()
            {
                "123456789 90",
                "272899987 87",
                "-72899987 87",
                "111234bn2 90",
                "272899987 -3",
                "83935 0"

            };
                if (ProcessPeople(data) != null && ProcessPeople(data).Contains(new Person("123456789", 90))
                    && ProcessPeople(data).Contains(new Person("272899987", 87))
                    )
                {
                    Console.WriteLine("Zadanie 8: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 8: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 8: 0");
            }
            try
            {
                Messenger messenger = new Messenger();

                if (MessangerSubsciber(messenger, "") != null && MessangerSubsciber(messenger, "Test").Equals("Test"))
                {
                    Console.WriteLine("Zadanie 9: 1");
                    points++;
                }
                else
                {
                    Console.WriteLine("Zadanie 9: 0");
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Zadanie 9: 0");
            }

            Console.WriteLine("Suma punktów: " + points);
        }

        //Zadanie 1 
        //w metodzie uzupełnij ciało delegata, aby zwracał łańcuch z liczbą `value` zapisaną w kodzie szesnastkowym
        //np. dla value 10 powinien zwrócić "A"
        public static IntFormatter HexFormatter()
        {
            return delegate (int value)
            {
                //usuń zgłoszenie wyjątku i wpisz rozwiązanie
                //throw new NotImplementedException();

                return value.ToString("X");
            };
        }
        //Zadanie 2
        //zwróć delegata typu operation, który dodaje oba argumenty 
        public static operation AddOperation()
        {
            //usuń zgłoszenie wyjątku i wpisz rozwiązanie
            //throw new NotImplementedException();

            return delegate (double x, double y)
            {
                return x + y;
            };
        }

        //Zadanie 3
        //wywołaj przekazanego delegata op z parametrami a i b, a wynik delegata zwróć jako wartość metody Calculate
        public static double Calculate(operation op, double a, double b)
        {
            //usuń zgłoszenie wyjątku i wpisz rozwiązanie
            //throw new NotImplementedException();

            return op(a, b);
        }

        //Zadanie 4
        //Zwróć wartość delegata typu Func, który zwraca powtórzony łańcuch (pierwszy argument) n razy (drugi argument) 
        public static Func<string, int, string> Repeat()
        {
            //usuń zgłoszenie wyjątku i wpisz rozwiązanie
            //throw new NotImplementedException();

            return delegate (string s, int n)
            {
                string rt = "";
                for (int i = 0; i < n; i++)
                    rt += s;

                return rt;
            };
        }

        //Zadanie 5
        //zwroć w metodzie lambdę, która wyświetla na konsoli przekazany łańcuch wielkimi literami
        public static Action<string> StringConsumer()
        {
            //usuń zgłoszenie wyjątku i wpisz rozwiązanie
            //throw new NotImplementedException();

            return (s) => s.ToUpper(); 
        }
        //Zadanie 6
        //zwroć w metodzie lambdę, która zwraca argument podniesiony do kwadratu
        public static Func<double, double> DoubleFunction()
        {
            //usuń zgłoszenie wyjątku i wpisz rozwiązanie
            //throw new NotImplementedException();

            return (x) => x * x;
        }
        //Zadanie 7
        //zwróć w metodzie lambdę, która zwraca prawdę, jeśli argument jest poprawnym numerem telefonu:
        //- ma 9 znaków
        //- każdy znak jest cyfrą
        public static Predicate<string> IsPhoneNumber()
        {
            //usuń zgłoszenie wyjątku i wpisz rozwiązanie
            //throw new NotImplementedException();
            int prs;
            return (s) => s.Length == 9 && int.TryParse(s, out prs);
        }
        public static List<Person> LoadPeople(List<String> RawData, Predicate<string> validator)
        {
            List<Person> list = new List<Person>();
            foreach (var row in RawData)
            {
                string[] tokens = row.Split(" ");
                string phone = tokens[0];
                string ects = tokens[1];
                if (validator != null && validator.Invoke(row))
                {
                    list.Add(new Person(phone, int.Parse(ects)));
                }
            }
            return list;
        }

        //Zadanie 8
        //Podaj w miejscu null lambdę predykatu, która validuje argument wejsciowy w postaci łańcucha
        //łańcuch składa się z dwóch części oddzielonych spacją
        //pierwsza zawiera nr telefonu (9 cyfr)
        //druga zwiera liczbę całkowitą puktów Ects, która nie może być ujemna 
        //jeśli obie części zawierają poprawne dane to predykat zwarac true
        public static List<Person> ProcessPeople(List<String> data)
        {
            return LoadPeople(data, null);
        }

        //Zadanie 9
        //Zdefiniuj słuchacza klasy Messanger w postaci lambdy, który po odbiorze wiadomości przypisze ją do zmiennej lokalnej receivedMessage
        public static string MessangerSubsciber(Messenger messenger, String message)
        {
            string receivedMessage = null;
            //poniżej wpisz lambdę, która jest subskrybentem obiektu messanger
            messenger.SendToAll(message);
            return receivedMessage;
        }
    }
    record Person(string Phone, int Ects);


    class Messenger
    {
        public event EventHandler<string> BrodcastMessage;

        protected virtual void OnBroadcast(string message)
        {
            BrodcastMessage?.Invoke(this, message);
        }

        public void SendToAll(string message)
        {
            OnBroadcast(message);
        }
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");
    //    }
    //}
}
