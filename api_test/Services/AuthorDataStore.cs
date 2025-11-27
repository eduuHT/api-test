using System;
using api_test.Models;

namespace api_test.Services;

public class AuthorDataStore
{
    public List<Author> Authors { get; set; }
    public static AuthorDataStore current { get; } = new AuthorDataStore();

    // Constructor
    public AuthorDataStore()
    {
        Authors = new List<Author>
        {
            new Author {
                Id = 1,
                FirstName = "Jose",
                LastName = "Gentile",
                Books = new List<Book> {
                    new Book {
                        Id = 1,
                        Pages = 256,
                        Title = "Entiende la Tecnología"
                    }
                }
            },
            new Author {
                Id = 2,
                FirstName = "Brais",
                LastName = "Moure",
                Books = new List<Book> {
                    new Book {
                        Id = 2,
                        Pages = 325,
                        Title = "Git & Github Desde Cero"
                    }
                }
            }
        };
    }
}
