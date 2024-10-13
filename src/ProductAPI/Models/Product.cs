
namespace ProductAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        required public string Name { get; set; }
        public decimal Price { get; set; }
        required public string Description { get; set; }
        public int Stock { get; set; }
    }
    public class CreateUpdateProductRequest
    {
        required public string Name { get; set; }
        public decimal Price { get; set; }
        required public string Description { get; set; }
        public int Stock { get; set; }
    }
}