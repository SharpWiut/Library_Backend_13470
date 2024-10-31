using Library.Models;
using Library.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var authors = _authorRepository.GetAuthors();
            return new OkObjectResult(authors);
        }


        // GET: api/Author/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetGetByID(int id)
        {
            var author = _authorRepository.GetAuhtorById(id);
            return new OkObjectResult(author);
        }

        // POST: api/Author
        [HttpPost]
        public IActionResult Post([FromBody] Author author)
        {
            using (var scope = new TransactionScope())
            {
                _authorRepository.InsertAuhtor(author);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
            }
        }


        // PUT: api/Author/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Author author)
        {
            if (author != null)
            {
                using (var scope = new TransactionScope())
                {
                    _authorRepository.UpdateAuthor(author);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        // DELETE: api/Author/Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _authorRepository.DeleteAuthor(id);
            return new OkResult();
        }

    }
}
