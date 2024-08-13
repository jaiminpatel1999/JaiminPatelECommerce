using JaiminPatelECommerce.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JaiminPatelECommerce.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartCookieKey = "JaiminPatelECommerceCart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Get all items in the cart
        public List<CartItem> GetCartItems()
        {
            var cartCookie = _httpContextAccessor.HttpContext.Request.Cookies[CartCookieKey];
            if (string.IsNullOrEmpty(cartCookie))
            {
                return new List<CartItem>();
            }

            return JsonConvert.DeserializeObject<List<CartItem>>(cartCookie) ?? new List<CartItem>();
        }

        // Add an item to the cart
        public void AddToCart(CartItem item)
        {
            var cartItems = GetCartItems();

            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cartItems.Add(item);
            }

            SaveCartItems(cartItems);
        }

        // Remove an item from the cart
        public void RemoveFromCart(int productId)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItems(cartItems);
            }
        }

        // Update the quantity of an item in the cart
        public void UpdateQuantity(int productId, int quantity)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                SaveCartItems(cartItems);
            }
        }

        // Clear the cart
        public void ClearCart()
        {
            SaveCartItems(new List<CartItem>());
        }

        // Calculate the total amount of the items in the cart
        public decimal CalculateTotalAmount(List<CartItem> cartItems)
        {
            return cartItems.Sum(item => item.Price * item.Quantity);
        }

        // Save cart items to the cookie
        private void SaveCartItems(List<CartItem> cartItems)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Set cookie expiration to 7 days
                IsEssential = true, // Indicates that the cookie is essential
                HttpOnly = true // Prevents client-side scripts from accessing the cookie
            };
            var cartJson = JsonConvert.SerializeObject(cartItems);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CartCookieKey, cartJson, cookieOptions);
        }
    }
}
