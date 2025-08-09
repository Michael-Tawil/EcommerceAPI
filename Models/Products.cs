
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName  = "Decimal(18,2)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
