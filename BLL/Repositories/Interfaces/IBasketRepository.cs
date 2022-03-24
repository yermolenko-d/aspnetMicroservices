using DLL.Entities;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasket(string userName);
        Task<Basket> UpdateBasket(Basket basket);
        Task DeleteBasket(string userName);
    }
}
