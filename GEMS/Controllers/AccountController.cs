using GEMS.Models;
using GEMS.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GEMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = userViewModel.Username,
                    Email = userViewModel.Email,
                    PasswordHash = userViewModel.Password,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Weight = userViewModel.Weight,
                    Height = userViewModel.Height,
                    BirthDate = userViewModel.BirthDate,
                };

                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, userViewModel.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
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
        public async Task<IActionResult> Login(UserLoginViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(userViewModel.Email);
                if (user is not null)
                {
                    bool correctPassword = await _userManager.CheckPasswordAsync(user, userViewModel.Password);

                    if (correctPassword)
                    {
                        await _signInManager.SignInAsync(user, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid Email or Password");
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            if (User.Identity?.IsAuthenticated != true)
                return RedirectToAction("Login");

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound("User not found.");

            var model = new UserUpdateViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Weight = user.Weight,
                Height = user.Height,
                BirthDate = user.BirthDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateViewModel userViewModel)
        {
            if (User.Identity?.IsAuthenticated != true)
                return RedirectToAction("Login");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found.");

            // Only update fields that have values
            if (!string.IsNullOrWhiteSpace(userViewModel.FirstName))
                user.FirstName = userViewModel.FirstName;

            if (!string.IsNullOrWhiteSpace(userViewModel.LastName))
                user.LastName = userViewModel.LastName;

            if (userViewModel.Weight > 0) // Assuming 0 is not a valid weight
                user.Weight = userViewModel.Weight;

            if (userViewModel.Height > 0) // Assuming 0 is not a valid height
                user.Height = userViewModel.Height;

            if (userViewModel.BirthDate != default)
                user.BirthDate = userViewModel.BirthDate;

            // Only update password if provided
            if (!string.IsNullOrWhiteSpace(userViewModel.Password))
            {
                var passwordValidator = new PasswordValidator<AppUser>();
                var result = await passwordValidator.ValidateAsync(_userManager, user, userViewModel.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userViewModel.Password);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    return View(userViewModel);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Update");
            }

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userViewModel);
        }



        public async Task<IActionResult> Delete()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)!;
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return NotFound("User not found.");
            }
        }
    }

}
