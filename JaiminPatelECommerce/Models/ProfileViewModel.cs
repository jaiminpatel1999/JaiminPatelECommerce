﻿using System.ComponentModel.DataAnnotations;

namespace JaiminPatelECommerce.Models
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
