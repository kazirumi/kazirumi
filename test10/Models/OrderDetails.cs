using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test10.Models
{
    public class OrderDetails
    {
        public int id { get; set; }
        public int productsid { get; set; }
        [ForeignKey("productsid")]
        public int Orderid { get; set; }
        [ForeignKey("Orderid")]
        [Display(Name ="Quantity")]
        public int productQuantity { get; set; }
        [Display(Name ="Price")]
        public decimal productPrice { get; set; }

        public Order Order { get; set; }
        public products products { get; set; }
    }
}
