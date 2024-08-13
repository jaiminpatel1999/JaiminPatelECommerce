using JaiminPatelECommerce.Models;
using JaiminPatelECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JaiminPatelECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;

        public AdminController(ProductService productService, OrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        // Action to display the admin dashboard
        public IActionResult Index()
        {
            return View();
        }

        // Action to manage products (list all products)
        public async Task<IActionResult> ManageProducts()
        {
            var products = await _productService.GetAvailableProducts();
            return View(products);
        }

        // Action to display the create product form
        public IActionResult CreateProduct()
        {
            return View();
        }

        // Action to handle the creation of a new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProduct(product);
                return RedirectToAction(nameof(ManageProducts));
            }

            return View(product);
        }

        // Action to display the edit product form
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Action to handle the editing of an existing product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(product);
                return RedirectToAction(nameof(ManageProducts));
            }

            return View(product);
        }

        // Action to display the delete product confirmation form
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Action to handle the deletion of a product
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(ManageProducts));
        }

        // Action to view all orders
        public async Task<IActionResult> ManageOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }

        // Action to view details of a specific order
        public async Task<IActionResult> ViewOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

    }
}
