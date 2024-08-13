using JaiminPatelECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace JaiminPatelECommerce.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Method to register a new user
        public async Task<IdentityResult> RegisterUser(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        // Method to login a user
        public async Task<SignInResult> LoginUser(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        // Method to log out the current user
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        // Method to check if a user is signed in
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_signInManager.Context.User);
        }

        // Method to update user details
        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        // Method to change user password
        public async Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
    }
}
