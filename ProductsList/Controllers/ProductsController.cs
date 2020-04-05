using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsList.Models;
using ProductsList.Services;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProductsList.Controllers
{
    public class ProductsController : Controller
    {
        #region Inner Types

        public class AddProductViewModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [Range(1, 100)]
            public string Amount { get; set; }

            [Required]
            public Unit Unit { get; set; }
        }

        public class RemoveProductViewModel
        {
            [Required]
            public int Id { get; set; }
        }

        public class UpdateProductViewModel
        {
            [Required]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [Range(1, 100)]
            public string Amount { get; set; }

            [Required]
            public Unit Unit { get; set; }
        }

        #endregion

        private readonly IProductsService _products;

        public ProductsController(IProductsService products) =>
            _products = products;

        [HttpGet]
        public ActionResult Index() =>
            View();

        [HttpGet]
        public ActionResult All() =>
            View(_products.All());

        [HttpGet]
        [Route("/{id}")]
        public ActionResult Get(int id) =>
            View(_products.Get(id));

        [HttpPost]
        public ActionResult Add([FromBody] AddProductViewModel vm)
        {
            var product = new Product
            {
                Name = vm.Name,
                Amount = vm.Amount,
                Unit = vm.Unit
            };

            if (_products.Add(product))
                return RedirectToAction(nameof(All));
            else
                return RedirectToAction(nameof(Error));
        }

        [HttpGet]
        public ActionResult Add() =>
            View();

        [HttpPost]
        public ActionResult Remove([FromBody] RemoveProductViewModel vm)
        {
            var product = _products.Get(vm.Id);

            if (_products.Remove(product))
                return RedirectToAction(nameof(All));
            else
                return RedirectToAction(nameof(Error));
        }

        [HttpPost]
        public ActionResult Update([FromBody] UpdateProductViewModel vm)
        {
            var product = new Product
            {
                Id = vm.Id,
                Name = vm.Name,
                Amount = vm.Amount,
                Unit = vm.Unit
            };

            if (_products.Update(product))
                return RedirectToAction(nameof(All));
            else
                return RedirectToAction(nameof(Error));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ??
                HttpContext.TraceIdentifier });
        }
    }
}
