using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_test.Models;
using api_test.Services;

namespace api_test.Controllers
{
    [Route("api/author/{authorId}/[controller]")] // First looks for the author, then for the action
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks(int authorId)
        {
            var author = AuthorDataStore.Current.Authors.FirstOrDefault(x => x.Id == authorId);

            if (author == null)
                return NotFound("The requested Author could not be found.");

            return Ok(author.Books);
        }

        [HttpGet("{bookId}")]
        public ActionResult<Book> GetBook(int authorId, int bookId)
        {
            var author = AuthorDataStore.Current.Authors.FirstOrDefault(x => x.Id == authorId);

            if (author == null)
                return NotFound("The requested Author could not be found.");

            var book = author.Books?.FirstOrDefault(x => x.Id == bookId); // The question mark stops the following code if "Books" is empty

            if (book == null)
                return NotFound("The requested Book could not be found.");

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> PostBook(int authorId, BookInsert bookInsert)
        {
            var author = AuthorDataStore.Current.Authors.FirstOrDefault(x => x.Id == authorId);

            if (author == null)
                return NotFound("The requested Author could not be found.");

            var existingBook = author.Books?.FirstOrDefault(b => b.Title == bookInsert.Title);

            if (existingBook != null)
                return BadRequest("This book already exists.");

            var maxBookId = author.Books.Max(m => m.Id);

            var newBook = new Book()
            {
                Id = maxBookId + 1,
                Title = bookInsert.Title,
                Pages = bookInsert.Pages
            };

            author.Books.Add(newBook);

            return CreatedAtAction(nameof(GetBook),
                new { authorId = authorId, bookId = newBook.Id},
                newBook
            );
        }

        [HttpPut]
        public ActionResult<Book> PutBook(int authorId, int bookId, BookInsert bookInsert)
        {
            // Validations
            var author = AuthorDataStore.Current.Authors.FirstOrDefault(x => x.Id == authorId);

            if (author == null)
                return NotFound("The requested Author could not be found.");

            var existingBook = author.Books?.FirstOrDefault(b => b.Id == bookId);

            if (existingBook == null)
                return BadRequest("The requested book doesn't exist.");

            var sameNameBook = author.Books?.FirstOrDefault(b => b.Id != bookId && b.Title == bookInsert.Title);

            if (sameNameBook != null)
                return BadRequest("This book already exists.");

            // Assigning
            existingBook.Title = bookInsert.Title;
            existingBook.Pages = bookInsert.Pages;

            return NoContent();
        }

        /* [HttpDelete]
        public ActionResult<Book> DeleteBook()
        { } */

    }
}
