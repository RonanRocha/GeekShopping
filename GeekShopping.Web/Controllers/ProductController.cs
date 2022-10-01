using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        private async Task<string> GetToken()
        {


            var token = await HttpContext.GetTokenAsync("access_token");
            return token;

 
             
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var token = await GetToken();
            var products = await _service.FindAllProducts(token);
            return View(products);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Update(int id)
        {
            var token = await GetToken();
            var product = await _service.FindProductById(id,token);
            if(product == null) return NotFound();
            return View(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var token = await GetToken();
                var response = await _service.UpdateProduct(productVm,token);
                if (response != null) return RedirectToAction(nameof(Index));

            }

            return View(productVm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productVm)
        {
            if(ModelState.IsValid)
            {
                var token = await GetToken();
                var response = await _service.CreateProduct(productVm, token);
                if (response != null) return RedirectToAction(nameof(Index));
                
            }

            return View(productVm);
            
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var token = await GetToken();
            var product = await _service.FindProductById(id, token);
            if (product == null) return NotFound();
            return View(product);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                var token = await GetToken();
                var response = await _service.DeleteProduct(productVm.Id, token);
                if (response) return RedirectToAction(nameof(Index));

            }

            return View(productVm);

        }
    }
}
