using ETicaret.Baskets.Entities;

namespace ETicaret.Baskets.Repositories.Interface
{
    public interface IBasketRepository
    {
        Task<bool> DeleteBasket(string id);
        Task<bool> SetBasket(UserBasket basket);
        //Task<IEnumerable<Basket>> GetBasketsByUserId(string UserId);
        Task<UserBasket> GetBasketByUserId(string userId);

    }
}
