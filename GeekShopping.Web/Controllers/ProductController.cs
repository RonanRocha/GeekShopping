using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.FindAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _service.FindProductById(id);
            if(product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateProduct(productVm);
                if (response != null) return RedirectToAction(nameof(Index));

            }

            return View(productVm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productVm)
        {
            if(ModelState.IsValid)
            {
                var response = await _service.CreateProduct(productVm);
                if (response != null) return RedirectToAction(nameof(Index));
                
            }

            return View(productVm);
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.FindProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.DeleteProduct(productVm.Id);
                if (response) return RedirectToAction(nameof(Index));

            }

            return View(productVm);

        }
    }
}
