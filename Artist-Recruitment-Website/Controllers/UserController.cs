using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dtos;

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
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            var newUser = new User() {FullName = register.FullName, Email = register.Email, DateOfBirth = register.DateOfBirth,  PhoneNumber = register.PhoneNumber, CreatedAt = DateTime.Today};
            var reasult = await userManager.CreateAsync(newUser, register.Password);
            if(reasult.Succeeded)
            {
                await signInManager.SignInAsync(newUser, isPersistent: false);
                return View();
            }
            ModelState.AddModelError("", "Invalid Register");
            return View(register);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(Login login)
        {
            var res = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.remmberMe, lockoutOnFailure:false);
            if(res.Succeeded)
            {
                return View();
            }
            ModelState.AddModelError("", "Invalid Login");
            return View(login);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
