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

        /* [HttpGet]
        public ActionResult<Book> GetBook()
        { }

        [HttpPost]
        public ActionResult<Book> PostBook()
        { }

        [HttpPut]
        public ActionResult<Book> PutBook()
        { }

        [HttpDelete]
        public ActionResult<Book> DeleteBook()
        { } */

    }
}
