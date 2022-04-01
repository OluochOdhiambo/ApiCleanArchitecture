using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ApplicationCore.DTOs
{
    public class CreateBookRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int Pages { get; set; }

        [Required]
        public string Summary { get; set; } = string.Empty;

        [Required]
        public bool InStock { get; set; }

        [Required]
        [Range(0.01, 1000)]
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
