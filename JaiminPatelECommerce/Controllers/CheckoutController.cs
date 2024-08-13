using JaiminPatelECommerce.Models;
using JaiminPatelECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JaiminPatelECommerce.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        public CheckoutController(CartService cartService, OrderService orderService, UserService userService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userService = userService;
        }

        // Action to display the checkout form
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cartItems = _cartService.GetCartItems();
            if (cartItems.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            var user = await _userService.GetCurrentUserAsync();
            var checkoutViewModel = new CheckoutViewModel
            {
                CartItems = cartItems,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TotalAmount = _cartService.CalculateTotalAmount(cartItems)
            };

            return View(checkoutViewModel);
        }

        // Action to process the checkout form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cartItems = _cartService.GetCartItems();
                var user = await _userService.GetCurrentUserAsync();
                var totalAmount = _cartService.CalculateTotalAmount(cartItems);

                var order = await _orderService.CreateOrder(user.Id, cartItems, totalAmount);

                // Clear the cart after successful order creation
                _cartService.ClearCart();

                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }
            model.CartItems = _cartService.GetCartItems();
            return View(model);
        }

        // Action to display the order confirmation page
        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
