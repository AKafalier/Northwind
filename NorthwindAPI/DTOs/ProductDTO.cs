using NorthwindAPI.Models;

namespace NorthwindAPI.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
