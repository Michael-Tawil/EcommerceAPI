using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTOs
{
    public class UpdateProductRequest
    {

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0.01,Double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public int StockQuantity { get; set; } 
    }
}
