﻿using Library.Models;

namespace Library.Repository
{
    public interface IBookRepository
    {
        void InsertBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
        Book GetBookById(int Id);
        IEnumerable<Book> GetBooks();
    }
}
