using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Model
{
    [ExcludeFromCodeCoverage]
    public class AddProductModel
    {
        [Required(ErrorMessage ="Name is required")]
        [MinLength(2,ErrorMessage ="Minimum length must be 2")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description of product is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
    }
}
