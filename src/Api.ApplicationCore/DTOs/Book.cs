using System;
using System.Collections.Generic;
using System.Text;

namespace Api.ApplicationCore.DTOs
{
    public class CreateBookRequest
    {
        //[Required]
        //[StringLength(30, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        public int Pages { get; set; }
        public string Summary { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public double Price { get; set; }
    }

    public class UpdateBookRequest : CreateBookRequest
    {
    }

    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Pages { get; set; }
        public string Summary { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public double Price { get; set; }
    }
}
