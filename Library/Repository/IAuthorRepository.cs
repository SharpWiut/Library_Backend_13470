using Library.Models;

namespace Library.Repository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        void InsertAuhtor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int AuthorID);
        Author GetAuhtorById(int Id);
    }
}
