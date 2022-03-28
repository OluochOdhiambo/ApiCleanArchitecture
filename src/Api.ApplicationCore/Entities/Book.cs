using System;
using System.Collections.Generic;
using System.Text;

namespace Api.ApplicationCore.Entities
{
    public class Book
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [MaxLength(30)]
        public string Title { get; set; } = string.Empty;

        public int Pages { get; set; }
        public string Summary { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
