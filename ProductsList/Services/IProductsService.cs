using ProductsList.Models;
using System.Collections.Generic;

namespace ProductsList.Services
{
    public interface IProductsService
    {
        bool Add(Product product);
        bool Remove(Product product);

        bool Update(Product product);

        Product Get(int id);

        IEnumerable<Product> All();
    }
}
