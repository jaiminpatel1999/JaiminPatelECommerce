using JaiminPatelECommerce.Models;
using JaiminPatelECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JaiminPatelECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ProductService _productService;

        public CartController(CartService cartService, ProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        // Display the shopping cart
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        // Add an item to the cart
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _productService.GetProductById(productId).Result;

            if (product == null || !product.IsAvailable || product.Stock <= 0)
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

            _cartService.AddToCart(cartItem);

            return RedirectToAction("Index");
        }

        // Update the quantity of an item in the cart
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest();
            }

            _cartService.UpdateQuantity(productId, quantity);
            return RedirectToAction("Index");
        }

        // Remove an item from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        // Clear the entire cart
        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
