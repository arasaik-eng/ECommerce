namespace ECommerce.Api.Products.Db
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Inventory { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
