using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsLayer
{
    public class Books
    {
        [Key]
        [Required]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
