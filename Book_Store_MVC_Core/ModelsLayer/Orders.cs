using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsLayer
{
    public class Orders
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CartId { get; set; }
    }
}
