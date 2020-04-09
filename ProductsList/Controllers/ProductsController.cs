using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsList.Models;
using ProductsList.Services;
using ProductsList.ViewModels;
using System.Diagnostics;

namespace ProductsList.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _products;

        public ProductsController(IProductsService products) =>
            _products = products;

        [HttpGet]
        public IActionResult Index() =>
            View();

        [HttpGet]
        public IActionResult All() =>
            View(_products.All());

        [HttpGet]
        [Route("/{id}")]
        public IActionResult Get(int id) =>
            View(_products.Get(id));

        [HttpGet]
        public IActionResult Add() =>
            View();

        [HttpPost]
        public IActionResult Add([FromForm] AddProductViewModel vm)
        {
            var product = new Product
            {
                Name = vm.Name,
                Amount = vm.Amount,
                Unit = vm.Unit
            };

            if (_products.Add(product) && ModelState.IsValid)
                return RedirectToAction(nameof(All));
            else
                return RedirectToAction(nameof(Error));
        }

        [HttpPost]
        public IActionResult Remove([FromForm] RemoveProductViewModel vm)
        {
            var product = _products.Get(vm.Id);

            if (_products.Remove(product) && ModelState.IsValid)
                return RedirectToAction(nameof(All));
            else
                return RedirectToAction(nameof(Error));
        }

        [HttpPost]
        public IActionResult Update([FromForm] UpdateProductViewModel vm)
        {
            var product = new Product
            {
                Id = vm.Id,
                Name = vm.Name,
                Amount = vm.Amount,
                Unit = vm.Unit
            };

            if (_products.Update(product) && ModelState.IsValid)
                return RedirectToAction(nameof(All));
            else
                return RedirectToAction(nameof(Error));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
        }
    }
}
