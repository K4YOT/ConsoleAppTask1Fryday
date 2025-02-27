using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTask1Fryday
{
    internal class Program
    {
        public class Author
        {
            public int AuthorId { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public virtual ICollection<Book> Books { get; set; }
        }

        public class Book
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public int PublishedYear { get; set; }
            public int AuthorId { get; set; }
            public virtual Author Author { get; set; }
        }

        public class LibraryContext : DbContext
        {
            public DbSet<Author> Authors { get; set; }
            public DbSet<Book> Books { get; set; }
        }

   
            static void Main(string[] args)
            {
                using (var context = new LibraryContext())
                {
                    var author = new Author { Name = "Анатолий Ливри", Country = "Россия" };
                    context.Authors.Add(author);
                    context.SaveChanges();

                    var book = new Book { Title = "Системный антибелый расизм", PublishedYear = 2022, AuthorId = author.AuthorId };
                    context.Books.Add(book);
                    context.SaveChanges();

                    var books = context.Books.Where(b => b.PublishedYear > 1800).ToList();
                    foreach (var b in books)
                    {
                        Console.WriteLine($"{b.Title} - {b.Author.Name}");
                    }
                }
            }
        }
    }