using System;

namespace lab_2
{
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
                //if (message is EmailMessage)
                //{
                //    mailCounter++;
                //    EmailMessage email = (EmailMessage) message;
                //}

                //2 jesli message nie jest typu EmailMessage to email jest rowny null
                EmailMessage email = message as EmailMessage;
                Console.WriteLine(email.Subject);
                mailCounter += email == null ? 0 : 1;
            }
            Console.WriteLine($"Liczba wysylanych emaili: {mailCounter}");

            IFly[] flyingObjects = new IFly[3];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Plane();
            ISwim swimming = flyingObjects[0] as ISwim;


            string[] names = { "Adam", "Ewa", "Karol" };
            //IAggregate aggregate = new StringAggregate(names);
            IAggregate aggregate = new StringAggregate(names);
            aggregate = new SimpleAggregate() { FirstName = "Karol", LastName = "Okrasa" };
            //
            IIterator iterator = aggregate.createIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
        }
    }

    interface IAggregate
    {
        IIterator createIterator();
    }
    interface IIterator
    {
        bool HasNext();
        string GetNext();
    }

    //Simple
    class SimpleAggregate : IAggregate
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public IIterator createIterator()
        {
            return new SimpleIterator(this);
        }
    }

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

    //String
    class StringAggregate : IAggregate
    {
        internal string[] names;
        public StringAggregate(string[] names)
        {
            this.names = names;
        }

        public IIterator createIterator()
        {
            return StringIterator(this);
        }
    }


    class StringIterator : IIterator
    {
        private StringAggregate aggregate;
        private int index = 0;
        public StringIterator(StringAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public bool HasNext()
        {
            return ;
        }

        public string GetNext()
        {
            throw new NotImplementedException();
        }
    }
}
