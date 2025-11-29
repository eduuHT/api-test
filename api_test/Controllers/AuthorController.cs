using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_test.Models;
using api_test.Services;

namespace api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return AuthorDataStore.Current.Authors;
        }

        [HttpGet("{authorId}")]
        public ActionResult<Author> GetAuthor(int authorId)
        {
            var author = AuthorDataStore.Current.Authors.FirstOrDefault(x => x.Id == authorId);

            if (author == null)
                return NotFound("The requested Author could not be found.");

            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> PostAuthor(AuthorInsert authorInsert)
        {
            var maxAuthorId = AuthorDataStore.Current.Authors.Max(x => x.Id);

            // Create the Author Object
            var newAuthor = new Author() {
              Id = maxAuthorId + 1,
              FirstName = authorInsert.FirstName,
              LastName = authorInsert.LastName  
            };

            //Insert the Author Object
            AuthorDataStore.Current.Authors.Add(newAuthor);

            return CreatedAtAction(nameof(GetAuthor),
                new { authorId = newAuthor.Id},
                newAuthor);
        }
    }
}
