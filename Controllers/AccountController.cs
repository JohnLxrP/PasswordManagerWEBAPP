using Microsoft.AspNetCore.Mvc;
using PasswordManagerWEBAPP.Data.Repositories;
using PasswordManagerWEBAPP.ViewModels;

namespace PasswordManagerWEBAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _repo;

        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var userModel = new RegisterUserViewModel
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Password = userViewModel.Password
                };
                var result = await _repo.SignUpUserAsync(userModel);
                if (result)
                {
                    TempData["RegistrationSuccess"] = "Registration successful! You can now log in.";
                    return RedirectToAction("Login");
                }
            }
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.SignInUserAsync(userViewModel);
                if (result is not null)
                {
                    HttpContext.Session.SetString("JWToken", result);
                    TempData["LoginSuccess"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login credentials");
            }
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Add your logout logic here if necessary
            TempData["LogoutSuccess"] = "You have been successfully logged out.";
            return RedirectToAction("Login");
        }
    }
}
