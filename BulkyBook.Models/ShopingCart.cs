using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class ShopingCart
    {
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please Enter a Value Between 1 And 1000")]
        public int Count { get; set; }
    }
}
