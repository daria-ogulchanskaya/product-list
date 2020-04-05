using Microsoft.AspNetCore.Http;
using ProductsList.Extensions;
using ProductsList.Models;
using System.Collections.Generic;

namespace ProductsList.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpContextAccessor _http;

        public ProductsService(IHttpContextAccessor http) =>
            _http = http;

        public IEnumerable<Product> All()
        {
            var products = _http.HttpContext.Session.GetObject<List<Product>>("products");
            if (products == null)
                return null;

            return products;
        }

        public Product Get(int id)
        {
            var products = _http.HttpContext.Session.GetObject<List<Product>>("products");
            if (products == null)
                return null;

            return products.Find(x => x.Id == id);
        }

        public bool Add(Product product)
        {
            var products = _http.HttpContext.Session.GetObject<List<Product>>("products");
            if (products == null)
                products = new List<Product>();

            products.Add(product);

            _http.HttpContext.Session.SetObject("products", products);

            return true;
        }

        public bool Remove(Product product)
        {
            var products = _http.HttpContext.Session.GetObject<List<Product>>("products");

            if (products == null || !products.Remove(product))
                return false;

            _http.HttpContext.Session.SetObject("products", products);

            return true;
        }

        public bool Update(Product product)
        {
            var products = _http.HttpContext.Session.GetObject<List<Product>>("products");
            if (products == null)
                return false;

            var old = products.Find(x => x.Id == product.Id);
            if (old == null)
                return false;

            products.Remove(old);
            products.Add(product);

            _http.HttpContext.Session.SetObject("products", products);

            return true;
        }
    }
}
