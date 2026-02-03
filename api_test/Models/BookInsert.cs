using System;

namespace api_test.Models;

public class BookInsert
{
    public int Pages { get; set; }
    public string Title { get; set; } = string.Empty;
}
