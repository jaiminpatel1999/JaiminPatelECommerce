using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JaiminPatelECommerce.Models
{
    public class CheckoutViewModel
    {
        [ValidateNever]
        public List<CartItem> CartItems { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string ExpiryDate { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVV { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
