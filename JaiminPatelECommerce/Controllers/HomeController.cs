using JaiminPatelECommerce.Models;
using JaiminPatelECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JaiminPatelECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly CartService _cartService;

        public HomeController(ProductService productService, CartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        // Action to display the homepage with a list of products
        public async Task<IActionResult> Index()
        {
            // Fetch all available products from the database
            var products = await _productService.GetTopProducts();

            // Return the view with the list of products
            return View(products);
        }

        // Action to display the details of a specific product
        public async Task<IActionResult> Details(int id)
        {
            // Fetch product details by id
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Action to add a product to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            // Fetch product details by id
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                ImageUrl = product.ImageUrl
            };

            // Add the item to the cart using the CartService
            _cartService.AddToCart(cartItem);

            return RedirectToAction("Index");
        }

        // Action to remove a product from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart using the CartService
            _cartService.RemoveFromCart(id);

            return RedirectToAction("Index");
        }

        // Action to update the quantity of a product in the cart
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            // Update the item quantity in the cart using the CartService
            _cartService.UpdateQuantity(id, quantity);

            return RedirectToAction("Index");
        }
    }
}
