using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JaiminPatelECommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
    }
}
