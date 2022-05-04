using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();
            Console.WriteLine(context.Books.Find(1));
            IQueryable<Book> books =
                from book in context.Books
                where book.EditionYear > 2019
                select book;

            Console.WriteLine(string.Join("\n", books));


            var list = 
                from book in context.Books
                join author in context.Authors
                on book.AuthorId equals author.Id
                where book.EditionYear > 2019
                select new { BookAuthor = author.Name, Title = book.Title }; //obiekty klasy anonimowej

            Console.WriteLine("\n" + string.Join("\n", list));

            foreach (var item in list)
            {
                Console.WriteLine(item.BookAuthor);
            }

            list = context.Authors.Join(
                context.Books.Where(b => b.EditionYear > 2019),
                a => a.Id,
                b => b.AuthorId,
                (a, b) => new { BookAuthor = a.Name, Title = b.Title }
                );

            Console.WriteLine("\n" + string.Join("\n", list));

            string xml =
                "<books>" +
                    "<book>" +
                        "<id>1</id>" +
                        "<title>C#</title>" +
                    "</book>" +
                    "<book>" +
                        "<id>2</id>" +
                        "<title>Asp.Net</title>" +
                    "</book>" +
                "</books>";

            XDocument doc = XDocument.Parse(xml);
            var booksId = doc
                .Elements("books")
                .Elements("book")
                .Select(x => new { Id = x.Elements("id").First().Value, Title = x.Elements("title").First().Value});

            foreach (var e in booksId)
            {
                Console.WriteLine(e);
            }
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            xml = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            //Console.WriteLine(xml);
            //przetworz xml na liste (IEnumerable) obiektow z polami Code, Bid i Ask

            var rates = XDocument.Parse(xml)
                .Elements("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(n => new
                {
                    Code = n.Element("Code").Value,
                    Ask = n.Element("Ask").Value,
                    Bid = n.Element("Bid").Value
                });

            Console.WriteLine(string.Join("\n", rates));
        }
    }

    public record Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EditionYear { get; set; }
        public int AuthorId { get; set; }
    }

    public record BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UniqueNumber { get; set; }
    }

    public record Author
    {
        public int Id { get; set; }
        public string Name;
    }

    class AppContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DATASOURCE=d:\\database\\base.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .ToTable("books")
                .HasData(
                new Book() { Id = 1, AuthorId = 1, EditionYear = 2020, Title = "C#" },
                new Book() { Id = 2, AuthorId = 1, EditionYear = 2021, Title = "Asp.Net" },
                new Book() { Id = 3, AuthorId = 2, EditionYear = 2019, Title = "Data  Stucture" },
                new Book() { Id = 4, AuthorId = 2, EditionYear = 2018, Title = "Web application" }
                );

            modelBuilder.Entity<BookCopy>()
                .ToTable("copies")
                .HasData(
                new BookCopy() { Id = 1, BookId = 1, UniqueNumber = "12hcd32fsd12"},
                new BookCopy() { Id = 2, BookId = 1, UniqueNumber = "12vds8vj4f"}
                );

            modelBuilder.Entity<Author>()
                .ToTable("authors")
                .HasData(
                new Author() { Id = 1, Name = "Adam"},
                new Author() { Id = 2, Name = "Ewa" }
                );
        }
    }
}
