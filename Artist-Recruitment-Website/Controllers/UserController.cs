using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Models;

namespace Artist_Recruitment_Website.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        // GET: /User/Register or /User/Login
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
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Invalid login attempt.");
            return View("LoginSignup");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("hello");
                return View("_RegisterPartial", register);
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
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("LoginSignup");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
