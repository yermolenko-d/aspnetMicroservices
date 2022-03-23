using DLL.Entities;
using MongoDB.Driver;

namespace DLL.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
