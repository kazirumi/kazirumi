using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test10.Models
{
    public class Order
    {
        public Order()
        {
             OrderDetails = new List<OrderDetails>();
        }
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name ="Phone No")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public int orderNumber { get; set; }
        [Display(Name ="Order No")]
        public string orderNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }

}
