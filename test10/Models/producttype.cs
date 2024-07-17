using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test10.Models
{
    public class producttype
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "product type")]
        public string prodtype { get; set; }
    }
}
