using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Jan", 5);
            //Object obj = person;
            //Object obj1;


            Money money = new Money(128, Currency.PLN);
            decimal cost = money;
            double price = (double) money;
            string str = (string)money;
            Console.WriteLine(str);
            Console.WriteLine(money.ToString());

            Money[] prices =
            {
                Money.Of(10, Currency.PLN),
                Money.Of(15, Currency.USD),
                Money.Of(13, Currency.USD),
                Money.Of(12, Currency.PLN),
                Money.Of(56, Currency.PLN),
                Money.Of(34, Currency.EUR),
                Money.Of(71, Currency.PLN),
                Money.Of(9, Currency.EUR)
            };
            Array.Sort(prices);
            foreach (var p in prices)
            {
                Console.WriteLine(p);
            }
        }
    }

    interface Equal
    {
        public bool Equals(Object obj);
    }
    interface IComparable
    {
        public int CompareTo(Money other);
    }

    public class Person : Equal
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
                if (value.Length >= 2)
                {
                    name = value;
                }
            }
        }
        private int _ects;
        public int Ects
        {
            get
            {
                return _ects;
            }
            set
            {
                _ects = value;

            }
        }

        public Person(string name, int ects)
        {
            Ects = ects;
            Name = name;
        }

        public bool Equals(Person other)
        {
            return other.Name.Equals(Name) && other.Ects == Ects;
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                Ects == person.Ects &&
                Name == person.Name;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }


    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public class Money : IComparable
    {
        
        private decimal _value;
        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        private Currency _currency;
        public Currency Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;
            }
        }
        public Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }


        public static Money operator *(Money money, decimal factor)
        {
            return new Money(money.Value * factor, money.Currency);
        }
        public static bool operator <(Money a, Money b)
        {
            IsSameCurriences(a, b);
            return a.Value < b.Value;
        }
        public static bool operator >(Money a, Money b)
        {
            IsSameCurriences(a, b);
            return a.Value > b.Value; 
        }
        private static void IsSameCurriences(Money a, Money b)
        {
            if (a._currency != b.Currency)
            {
                throw new ArgumentException("Different currencies");
            }
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public static explicit operator string(Money money)
        {
            return $"{money.Value} {money.Currency}";
            //return money.Value.ToString() + " " + money.Currency.ToString();
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency {_currency}";
        }

        public int CompareTo(Money other)
        {
            //return Currency.CompareTo(other.Currency);
            int currencyCon = Currency.CompareTo(other.Currency);
            if (currencyCon == 0)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                return currencyCon;
            }
        }
    }
}
