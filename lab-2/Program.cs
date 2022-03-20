using System;
using System.Collections.Generic;

namespace lab_2
{
    //---
    abstract class AbstractMessage
    {
        public string Content { get; init; }
        abstract public void Send();
    }
    class EmailMessage : AbstractMessage
    {
        public string To { get; init; }
        public string From { get; init; }
        public string Subject { get; init; }
        public override void Send()
        {
            Console.WriteLine($"Sending email from {From} with content {Content}");
        }
    }
    class SmsMessage : AbstractMessage
    {
        public string PhoneNumber { get; init; }
        public override void Send()
        {
            Console.WriteLine($"Sending sms tp {PhoneNumber} with content {Content}");
        }
    }
    class MessangerMessage : AbstractMessage
    {
        public override void Send()
        {
            Console.WriteLine($"Sending messanger message ... {Content}");
        }
    }
    //---

    //----
    interface IFly
    {
        void Fly();
    }
    interface ISwim
    {
        void Swim();
    }
    interface IFlyandSwim : IFly, ISwim { }
    class Duck : IFlyandSwim
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }
        public void Swim()
        {
            throw new NotImplementedException();
        }
    }
    class Wasp : IFly
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }
    }
    class Plane : IFlyandSwim
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }
        public void Swim()
        {
            throw new NotImplementedException();
        }
    }
    //----

    //--
    public abstract class Vehicle
    {
        public double Weight { get; init; }
        public int MaxSpeed { get; init; }
        protected int _mileage;
        public int Mealeage
        {
            get { return _mileage; }
        }
        public abstract decimal Drive(int distance);
        public override string ToString()
        {
            return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
        }
    }
    public class ElectricScooter : Vehicle
    {
        private int batteriesLevel { get; set; }
        public int BatteriesLevel
        {
            get
            {
                return batteriesLevel;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Wartość nie może być mniejsza niż 0!!!");

                if (value >= 100)
                    throw new ArgumentOutOfRangeException("wartość nie może być większa niż 100!!!");

                batteriesLevel = value;
            }
        }
        private decimal maxRange { get; set; }
        public decimal MaxRange 
        {
            get
            {
                return maxRange;
            }
            set
            {
                maxRange = value;
            }
        }

        public ElectricScooter(double weight, int maxspeed, decimal maxrange)
        {
            Weight = weight;
            MaxSpeed = maxspeed;
            this.maxRange = maxrange;
        }
        public ElectricScooter() { }
        public void ChargeBatteries()
        {
            batteriesLevel = 100;
        }
        public override decimal Drive(int distance)
        {
            if (batteriesLevel <= 0)
                return -1;

            if (((int)(batteriesLevel * distance / maxRange)) > batteriesLevel)
                return -1;

            batteriesLevel -= (int) (batteriesLevel * distance / maxRange);
            return Math.Round((decimal) distance / (decimal) MaxSpeed, 3);
        }
        public override string ToString()
        {
            return $"ElectricScooter{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage}, BatteriesLevel: {batteriesLevel}, MaxRange: {maxRange} }}";
        }
    }
    //--


    class Program
    {
        static void Main(string[] args)
        {
            string messageType = "email";
            switch (messageType)
            {
                case "email":
                    Console.WriteLine("Wysylanie emaila");
                    break;
                case "sms":
                    Console.WriteLine("Wysywanie sms");
                    break;
                case "messanger":
                    Console.WriteLine("Wysylanie powiadomienia");
                    break;
            }

            AbstractMessage[] messages = new AbstractMessage[4];
            messages[0] = new EmailMessage() { Content = "Hello", To = "adam@gmail.com", From = "", Subject = "" };
            messages[1] = new SmsMessage() { Content = "Hello from sms", PhoneNumber = "345265345"};
            messages[2] = new EmailMessage() { Content = "Czesc", To = "ola@gmail.com", From = "", Subject = "" };
            messages[3] = new MessangerMessage() { Content = "Wiadomosc" };
            int mailCounter = 0;
            //Kod odpowiedzialny za wysylke
            foreach (var message in messages)
            {
                message.Send();
                //1
                if (message is EmailMessage)
                {
                    mailCounter++;
                    EmailMessage email = (EmailMessage)message;
                }

                //2 jesli message nie jest typu EmailMessage to email jest rowny null
                //EmailMessage email = message as EmailMessage;
                //Console.WriteLine(email.Subject);
                //mailCounter += email == null ? 0 : 1;
            }
            Console.WriteLine($"Liczba wysylanych emaili: {mailCounter}");

            IFly[] flyingObjects = new IFly[3];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Plane();
            ISwim swimming = flyingObjects[0] as ISwim;


            string[] names = { "Adam", "Ewa", "Karol" };
            IAggregate aggregate = new StringAggregate(names);
            aggregate = new SimpleAggregate() { FirstName = "Karol", LastName = "Okrasa" };
            //
            IIterator iterator = aggregate.createIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }


            //--
            ElectricScooter escooter = new ElectricScooter(50, 60, 200);
            escooter.ChargeBatteries();
            Console.WriteLine(escooter.ToString());
            Console.WriteLine(escooter.Drive(79));
            Console.WriteLine(escooter.ToString());
            //--



            //-
            Console.WriteLine();
            string[] vvvalues = { "1", "2", "3", "4", "5" };
            IAggregate vvaggregate = new ViceVersaAggregate(vvvalues);
            IIterator vviterator = vvaggregate.createIterator();
            while (vviterator.HasNext())
            {
                Console.WriteLine(vviterator.GetNext());
            }

            Console.WriteLine();
            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
            IAggregate eoAggregate = new EvenOddAggregate(values);
            IIterator eoIterator = eoAggregate.createIterator();
            while (eoIterator.HasNext())
            {
                Console.WriteLine(eoIterator.GetNext());
            }

            Console.WriteLine();
            IAggregate mAggregate = new MultipleAggregate(values, 3);
            IIterator mIterator = mAggregate.createIterator();
            while (mIterator.HasNext())
            {
                Console.WriteLine(mIterator.GetNext());
            }
            //-
        }
    }


    //Interfaces
    interface IAggregate
    {
        IIterator createIterator();
    }
    interface IIterator
    {
        bool HasNext();
        string GetNext();
    }

    //Simple         #1
    //------Aggregate
    class SimpleAggregate : IAggregate
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public IIterator createIterator()
        {
            return new SimpleIterator(this);
        }
    }
    //------Iterator
    class SimpleIterator : IIterator
    {
        private SimpleAggregate aggregate;
        private int count = 0;

        public SimpleIterator(SimpleAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public string GetNext()
        {
            switch (++count)
            {
                case 1:
                    return aggregate.FirstName;
                case 2:
                    return aggregate.LastName;
                default:
                    throw new Exception();
            }
        }

        public bool HasNext()
        {
            return count < 2;
        }
    }

    //String         #2
    //------Aggregate
    class StringAggregate : IAggregate
    {
        internal string[] names;
        public StringAggregate(string[] names)
        {
            this.names = names;
        }

        public IIterator createIterator()
        {
            return new StringIterator(this);
        }
    }
    //------Iterator
    class StringIterator : IIterator
    {
        private StringAggregate aggregate;
        private int index = 0;
        public StringIterator(StringAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public string GetNext()
        {
            return aggregate.names[index++];
        }
        public bool HasNext()
        {
            return index < aggregate.names.Length;
        }

    }

    //4 => 1         #3
    //------Aggregate
    class ViceVersaAggregate : IAggregate
    {
        internal string[] vvValues;
        public ViceVersaAggregate(string[] vvvalues)
        {
            vvValues = vvvalues;
        }
        public IIterator createIterator()
        {
            return new ViceVersaIterator(this);
        }
    }
    //------Iterator
    class ViceVersaIterator : IIterator
    {
        private ViceVersaAggregate aggregate;
        private int index = 0;
        public ViceVersaIterator(ViceVersaAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public string GetNext()
        {
            index++;
            return aggregate.vvValues[aggregate.vvValues.Length - index];
        }

        public bool HasNext()
        {
            return index < aggregate.vvValues.Length;
        }
    }

    //2,4,3,1        #4
    //-------Aggregate
    class EvenOddAggregate : IAggregate
    {
        internal int[] eoValues;
        public EvenOddAggregate(int[] values)
        {
            eoValues = values;
        }
        public IIterator createIterator()
        {

            return new EvenOddIterator(this);
        }
    }
    //-------Iterator
    class EvenOddIterator : IIterator
    {
        private EvenOddAggregate aggregate;
        private int index = 0;
        public EvenOddIterator(EvenOddAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public string GetNext()
        {
            index++;
            List<int> trueEOValues = new List<int>();

            List<int> even = new List<int>();
            foreach (var item in aggregate.eoValues)
                if (item % 2 == 0)
                    even.Add(item);

            List<int> odd = new List<int>();
            foreach (var item in aggregate.eoValues)
                if (item % 2 == 1)
                    odd.Add(item);

            even.Sort();
            foreach (var item in even)
                trueEOValues.Add(item);

            odd.Sort();
            odd.Reverse();
            foreach (var item in odd)
                trueEOValues.Add(item);

            return trueEOValues[index - 1].ToString();
        }

        public bool HasNext()
        {
            return index < aggregate.eoValues.Length;
        }
    }

    //k=3 => 3,6 ... #5
    //--------------Aggregate
    class MultipleAggregate : IAggregate
    {
        internal int[] Values;
        internal int k;
        public MultipleAggregate(int[] values, int k)
        {
            Values = values;
            this.k = k;
        }
        public IIterator createIterator()
        {

            return new MultipleIterator(this);
        }
    }
    //--------------Iterator
    class MultipleIterator : IIterator
    {
        private MultipleAggregate aggregate;
        private int index = 0;
        public MultipleIterator(MultipleAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public string GetNext()
        {
            index++;

            List<int> mValues = new List<int>();
            foreach (var item in aggregate.Values)
                if (item % aggregate.k == 0)
                    mValues.Add(item);

            return mValues[index - 1].ToString();
        }

        public bool HasNext()
        {
            List<int> mValues = new List<int>();
            foreach (var item in aggregate.Values)
                if (item % aggregate.k == 0)
                    mValues.Add(item);

            return index < mValues.Count;
        }
    }
}
