using System;

namespace api_test.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<Book>? Books { get; set; } = new List<Book>();
}
