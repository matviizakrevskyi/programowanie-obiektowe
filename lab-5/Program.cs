using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_5
{
    public record  Book (string Tittle, string Author, string Isbn);
    class Library : IEnumerable<Book>
    {
        internal Book[] _books =
        {
            new Book("C#", "Freeman", "123"),
            new Book("Asp.NET", "Freeman", "456"),
            null,
            new Book("Java", "Bloch", "561")
        };

        public Book this[string isbn]
        {
            get
            {
                foreach (Book book in _books)
                {
                    if (book != null && book.Isbn.Equals(isbn))
                    {
                        return book;
                    }
                }
                return null;
            }
        }
        public Book this[int index]
        {
            get
            {
                return _books[index - 1];
            }
            set
            {
                _books[index - 1] = value;
            }
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return new BookEnumerator(this);
            //foreach (Book book in _books)
            //{
            //    if (book != null)
            //    {
            //        yield return book;
            //    }
            //}
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    class Team : IEnumerable<string>
    {
        public string Leader;
        public string ScrumMaster;
        public string Developer;

        public IEnumerator<string> GetEnumerator()
        {
            yield return Leader;      //wartosc zwrocona przy pierszym wywolaniu MoveNext
            yield return ScrumMaster; //...                   drugim   wywolaniu MoveNext
            yield return Developer;   //...
            for (int i = 0; i < 5; i++)
            {
                yield return "Vacat";
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class BookEnumerator : IEnumerator<Book>
    {
        private Library _library;
        int index = -1;
        public BookEnumerator(Library library)
        {
            this._library = library;
        }
        public Book Current
        {
            get
            {
                while(_library._books[index] == null && index < _library._books.Length - 1)
                {
                    index++;
                }
                return _library._books[index];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            return ++index < _library._books.Length;
        }

        public void Reset()
        {
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library books = new Library();
            IEnumerator<Book> enumerator = books.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            //enumerator.Reset();
            for (var e = books.GetEnumerator(); e.MoveNext();)
            {
                Console.WriteLine(e.Current);
            }
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }

            Team team = new Team() { Leader = "Kowalski", Developer = "Nowak", ScrumMaster = "Szpak"};
            foreach (string member in team)
            {
                Console.WriteLine(member);
            }

            Console.WriteLine(books["561"]);
            books[3] = new Book("Nowa", "Nowy", "111");
            Console.WriteLine(string.Join(", ", books));
        }
    }
}
