using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace test10.Models
{
    public class SpecialTag
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Tag")]
        public string SpecialTagname { get; set; }
    }
}
