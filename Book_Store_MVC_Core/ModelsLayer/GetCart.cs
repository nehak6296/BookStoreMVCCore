using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLayer
{
    public class GetCart
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int CartBookQuantity { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
