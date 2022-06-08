using System;

namespace Zadanie_domowe
{
    public enum Quantitys
    {
        mm,
        m,
        Km
    }


    /// <summary>
    /// Klasa reprezentująca długość wyrażoną w metrach lub jej wielokrotnościach i podwielokrotnościach.
    /// </summary>
    class Length
    {
        private decimal _length;
        public decimal _Length 
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }

        private Quantitys quantity;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="length">Długość</param>
        /// <param name="quantity">Wielkość fizyczna(mm, m, Km)</param>
        public Length(decimal length, Quantitys quantity)
        {
            _length = length;
            this.quantity = quantity;
        }
        
        /// <summary>
        /// Konstruktor, który nie akceptuje wartości
        /// </summary>
        public Length()
        {
            _length = 0;
            quantity = Quantitys.m;
        }

        /// <summary>
        /// Operator dodawania, w którym wartość długości jest podawana w metrach 
        /// </summary>
        public static decimal operator +(Length a, Length b)
        {
            return a.toM() + b.toM();
        }

        /// <summary>
        /// Operator odejmowania, w którym wartość długości jest podawana w metrach 
        /// </summary>
        public static decimal operator -(Length a, Length b)
        {
            return a.toM() - b.toM();
        }

        public static decimal operator /(Length l, decimal x)
        {
            return l._length / x;
        }
        public static decimal operator *(Length l, decimal x)
        {
            return l._length * x;
        }

        public static bool operator ==(Length a, Length b)
        {
            return a.toM() == b.toM();
        }

        public static bool operator !=(Length a, Length b)
        {
            return a.toM() != b.toM();
        }

        /// <summary>
        /// Zmienia stopień wielkości
        /// </summary>
        /// <param name="q">nowy stopień wielkości</param>
        public void To(Quantitys q)                      
        {
            //from mm
            if (quantity == Quantitys.mm && q == Quantitys.m)
            {
                quantity = Quantitys.m;
                _length = _length / 1000;
            }
            if (quantity == Quantitys.mm && q == Quantitys.Km)
            {
                quantity = Quantitys.Km;
                _length = _length / 1000000;
            }
            //from m
            if (quantity == Quantitys.m && q == Quantitys.mm)
            {
                quantity = Quantitys.mm;
                _length = _length * 1000;
            }
            if (quantity == Quantitys.m && q == Quantitys.Km)
            {
                quantity = Quantitys.Km;
                _length = _length / 1000;
            }
            //from km
            if (quantity == Quantitys.Km && q == Quantitys.mm)
            {
                quantity = Quantitys.mm;
                _length = _length * 1000000;
            }
            if (quantity == Quantitys.Km && q == Quantitys.m)
            {
                quantity = Quantitys.m;
                _length = _length * 1000;
            }
        }

        /// <summary>
        /// Metoda zwracająca wartość długości w metrach
        /// </summary>
        public decimal toM() 
        {
            if (this.quantity == Quantitys.mm) 
                return this._length / 1000;
            if (this.quantity == Quantitys.m) 
                return this._length;
            if (this.quantity == Quantitys.Km) 
                return this._length * 1000;
            else
                throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            return $"{_length} ({quantity})";
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            Length l1 = new Length(123, Quantitys.m);
            Length l2 = new Length(67, Quantitys.Km);
            Length l3 = new Length(5012, Quantitys.mm);

            Console.WriteLine($"l1 = {l1} \nl2 = {l2} \nl3 = {l3}");

            Console.WriteLine("\n" + $"l1 + l2 = {l1 + l2}");
            Console.WriteLine($"l3 * 4 = {l3 * 4}");
            Console.WriteLine($"l2 / 5 = {l2 / 5}");
            Console.WriteLine($"l1 == l2 is {l1 == l2}");
        }
    }
}
