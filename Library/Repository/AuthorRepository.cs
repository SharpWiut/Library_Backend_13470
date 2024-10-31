using Library.DbContexts;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {

        private readonly BookContext _dbContext;

        public AuthorRepository(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _dbContext.Authors.ToList();
        }



        public void DeleteAuthor(int authorId)
        {
            var author = _dbContext.Authors.Find(authorId);
            _dbContext.Authors.Remove(author);
            Save();
        }

        public Author GetAuhtorById(int Id)
        {
            var author = _dbContext.Authors.Find(Id);
            return author;
        }

      
        public void InsertAuhtor(Author author)
        {
            _dbContext.Add(author);
            Save();
        }
       
        public void UpdateAuthor(Author author)
        {
            _dbContext.Entry(author).State =
             Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
