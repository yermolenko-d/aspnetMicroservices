using DLL.Entities;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<ShopBasket> GetBasket(string userName);
        Task<ShopBasket> UpdateBasket(ShopBasket basket);
        Task DeleteBasket(string userName);
    }
}
