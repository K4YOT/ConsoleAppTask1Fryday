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
                // CRUD операции для Authors
                var author = new Author { Name = "Лев Толстой", Country = "Россия" };
                context.Authors.Add(author);
                context.SaveChanges();

                // CRUD операции для Books
                var book = new Book { Title = "Война и мир", PublishedYear = 1869, AuthorId = author.AuthorId };
                context.Books.Add(book);
                context.SaveChanges();

                // Чтение данных

                {
                    // Явная загрузка авторов вместе с книгами
                    var books = context.Books
                        .Include(b => b.Author) // Загружаем автора
                        .Where(b => b.PublishedYear > 1800)
                        .ToList();

                    // Вывод данных с проверкой на null
                    foreach (var b in books)
                    {
                        string authorName = b.Author != null ? b.Author.Name : "Автор неизвестен";
                        Console.WriteLine($"{b.Title} - {authorName}");
                    }
                }
            }
        }
    }
}