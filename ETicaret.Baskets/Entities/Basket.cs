namespace ETicaret.Baskets.Entities
{
    public class Basket
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public bool OrderComplete { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
