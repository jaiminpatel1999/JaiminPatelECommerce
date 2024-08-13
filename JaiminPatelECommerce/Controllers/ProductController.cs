using JaiminPatelECommerce.Models;
using JaiminPatelECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JaiminPatelECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Action to display a list of all available products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAvailableProducts();
            return View(products);
        }

        // Action to display details of a specific product
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Action to display the create product form (admin only)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // Action to handle the creation of a new product (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // Action to display the edit product form (admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Action to handle the editing of an existing product (admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // Action to display the delete confirmation form (admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Action to handle the deletion of a product (admin only)
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
