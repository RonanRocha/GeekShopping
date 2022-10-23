using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService service, ICartService cartService)
        {
            _logger = logger;
            _productService = service;
            _cartService = cartService;
        }

        private async Task<string> GetToken()
        {

            var token = await HttpContext.GetTokenAsync("access_token");
            return token;
        }

        public async Task<IActionResult> Index()
        {
 
            var products = await _productService.FindAllProducts();
            return View(products);
        }


        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            var token = await  GetToken();
            var product = await _productService.FindProductById(id, token);
            return View(product);

        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductViewModel viewModel)
        {

            var token = await GetToken();

            CartViewModel cart = new CartViewModel()
            {
                CartHeader = new CartHeaderViewModel
                {
                    UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value
                }
            };


            CartDetailViewModel cartDetail = new CartDetailViewModel()
            {
                Count = viewModel.Count,
                ProductId =  viewModel.Id,
                Product = await _productService.FindProductById(viewModel.Id, token)
            };


            List<CartDetailViewModel> cartDetailList = new List<CartDetailViewModel>();
            cartDetailList.Add(cartDetail);
            cart.CartDetails = cartDetailList;

            var response = await _cartService.AddItemToCart(cart, token);
            if (response != null) return RedirectToAction(nameof(Index));



            return View(viewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

    }
}