using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        


        // GET: api/Book
        [HttpGet]
        public IActionResult Get()
        {
            var books = _bookRepository.GetBooks();
            return new OkObjectResult(books);
        }


        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetBooks")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return new OkObjectResult(book);
            //return "value";
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            // Check if the AuthorId is valid
            var author = _authorRepository.GetAuhtorById(book.AuthorId);
            if (author == null)
            {
                return BadRequest("The Author field is required."); // or "Author not found."
            }

            using (var scope = new TransactionScope())
            {
                _bookRepository.InsertBook(book);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
            }
        }
        // PUT: api/Book/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            if (book != null)
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepository.UpdateBook(book);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return new OkResult();
        }

    }
}
