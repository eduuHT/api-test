using System;

namespace api_test.Models;

public class Book
{
    public int Id { get; set; }

    public int Pages { get; set; }
    public string Title { get; set; } = string.Empty;

}
