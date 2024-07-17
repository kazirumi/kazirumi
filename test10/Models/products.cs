using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test10.Models
{
    public class products
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Image { get; set; }
        [Required]
        [Display(Name="Available")]
        public bool IsAvailable { get; set; }
        [Required]
        [Display(Name="Product Color")]
        public string ProductColor { get; set; }
        [Required]
        [Display(Name="Product Type")]
        public int producttypeId { get; set; }
        [ForeignKey("producttypeId")]
        public producttype producttype { get; set; }
        [Required]
        [Display(Name="Special Tag")]
        public int SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public SpecialTag SpecialTag { get; set; }
    }
}
