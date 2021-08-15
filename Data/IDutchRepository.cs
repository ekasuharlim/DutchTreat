using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();

        Product GetProduct(int id);

        Order GetOrder(int id);

        IEnumerable<Order> GetAllOrder(bool includeOrderItem);

        IEnumerable<Order> GetAllOrder(string userName, bool includeOrderItem);
        void AddEntity(object entity);
        bool SaveAll();
    }
}