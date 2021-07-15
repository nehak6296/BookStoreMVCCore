using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsLayer
{
    public class WishList
    {
        [Key]
        [Required]
        public int WishListId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int WishListQuantity { get; set; }
    }
}
