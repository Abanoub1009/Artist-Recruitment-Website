using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models;
using BL.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Artist_Recruitment_Website.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        //private readonly IArtistProfileService _artistProfileService;

        public UserController(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            //this._artistProfileService = artistProfileService;
        }

        public IActionResult LoginSignup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Login login)
        {
            if (!ModelState.IsValid)
                return View("LoginSignup");

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.remmberMe, false);

            if (result.Succeeded)
            {
                // Check if user has an artist profile
                var user = await userManager.FindByEmailAsync(login.Email);
                
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View("LoginSignup");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View("_RegisterPartial", register);
            }

            // Check if email already exists
            var existingUserByEmail = await userManager.FindByEmailAsync(register.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View("LoginSignup", register);
            }

            // Check if phone number already exists
            var existingUserByPhone = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == register.PhoneNumber);
            if (existingUserByPhone != null)
            {
                ModelState.AddModelError("PhoneNumber", "This phone number is already registered.");
                return View("LoginSignup", register);
            }

            var user = new User
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.Email,
                PhoneNumber = register.PhoneNumber,
                DateOfBirth = register.DateOfBirth
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                // Redirect to Create ArtistProfile after successful registration
                return RedirectToAction("Create", "ArtistProfile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("LoginSignup");
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
