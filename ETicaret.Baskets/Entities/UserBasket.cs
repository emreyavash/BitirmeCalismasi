namespace ETicaret.Baskets.Entities
{
    public class UserBasket
    {
        public string UserId { get; set; }
        public List<Basket> Baskets { get; set; } = new List<Basket>();
        public UserBasket()
        {
            
        }
        public UserBasket(string userId)
        {
            UserId = userId;
        }
    }
}
