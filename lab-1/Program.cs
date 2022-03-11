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

            Money money1 = Money.ParseValue("12,45", Currency.PLN);
            Console.WriteLine(money1.ToString());
            Money money = Money.OfWithExeption(128, Currency.PLN);
            decimal cost = money;
            double price = (double)money1;
            float pricefloat = (float)money;
            Console.WriteLine(cost + ", " + price + ", " + pricefloat);
            string str = (string)money;
            Console.WriteLine(str);
            Console.WriteLine(money.ToString());

            Console.WriteLine();
            Tank tank1 = new Tank(100);
            tank1.refuel(48);
            tank1.Show();
            tank1.consume(2);
            tank1.Show();
            Tank tank2 = new Tank(250);
            tank2.refuel(120);
            tank1.refuel(tank2, 100);
            tank1.Show();
            tank1.refuel(tank2, 20);
            tank1.Show();

            Console.WriteLine();
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
            //Array.Sort(prices);
            //foreach (var p in prices)
            //{
            //    Console.WriteLine(p);
            //}

        }
    }

    //interfaces
    interface Equal
    {
        public bool Equals(Object obj);
    }
    interface IComparable
    {
        public int CompareTo(Money other);
    }

    //Person
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

    //Money
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money : IComparable
    {

        private decimal _value;
        private Currency _currency;

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

        //constructor
        private Money(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        //Of
        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money OfWithExeption(decimal value, Currency currency)
        {
            if (value >= 0)
            {
                return value < 0 ? null : new Money(value, currency);
            }
            else
            {
                throw new ArgumentException("Argument exception!!!");
            }
        }

        public static Money ParseValue(string valueStr, Currency currency)
        {
            return new Money(decimal.Parse(valueStr), currency);
        }

        //operators
        public static Money operator *(Money money, decimal factor)
        {
            return new Money(money.Value * factor, money.Currency);
        }
        public static decimal operator +(Money a, Money b)
        {
            return a.Value + b.Value;
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

        public static explicit operator float(Money money)
        {
            return (float)money.Value;
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

    //Tank
    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }
        public Tank(int capacity)
        {
            Capacity = capacity;
        }

        public void refuel(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Argument cant be non positive!");
            }
            if (_level + amount > Capacity)
            {
                throw new ArgumentException("Argument is to large!");
            }
            _level += amount;
        }

        public void consume(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Argument cant be non positive!");
            }
            if (amount > _level)
            {
                throw new ArgumentException("Argument is to large!");
            }
            _level += amount;
        }

        public bool refuel(Tank sourceTank, int amount)
        {
            if (amount < 0)
                return false;
            if (amount > sourceTank._level)
                return false;
            if (amount + this._level > this.Capacity)
                return false;

            sourceTank._level -= amount;
            this._level += amount;
            return true;
        }


        public void Show()
        {
            Console.WriteLine(this.Level);
        }
    }
}
