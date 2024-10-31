using Library.DbContexts;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _dbContext;

        public BookRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteBook(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            _dbContext.Books.Remove(book);
            Save();
        }

        public Book GetBookById(int Id)
        {
            var book = _dbContext.Books.Find(Id);
            _dbContext.Entry(book).Reference(s => s.Author).Load();
            return book;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _dbContext.Books.Include(s => s.Author).ToList();
        }

        public void InsertBook(Book book)
        {
            _dbContext.Add(book);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _dbContext.Entry(book).State =
            Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
    }
}
