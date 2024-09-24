using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace Company.G03.PL.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )

        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp
        [HttpGet] // /Acount/SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost] // /Acount/SignUp
        public async Task<IActionResult> SignUpAsync(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                //SignUp
                try
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user is null)
                    {
                        user = await _userManager.FindByEmailAsync(model.Email);
                        if (user is null)
                        {
                            user = new ApplicationUser()
                            {
                                UserName = model.UserName,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Email = model.Email,
                                IsAgree = model.IsAgree,

                            };
                            var result = await _userManager.CreateAsync(user, model.Password);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("SignIn");
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);

                            }

                        }
                        ModelState.AddModelError(string.Empty, "Email is Already Exists !!");
                        return View(model);
                    }
                    ModelState.AddModelError(string.Empty, "UserName is Already Exists !!");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(model);
        }
        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost] // /Account/SignIn

        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is not null)
                    {
                        //Check Password
                        var Flag = await _userManager.CheckPasswordAsync(user, model.Password);
                        if (Flag)
                        {
                            //SignIn

                           var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,lockoutOnFailure: false );
                            if (result.Succeeded)
                            {
                                RedirectToAction(actionName: "Index", controllerName: "Home");
                            }
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login !!");

                }
                catch (Exception ex)
                    {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
           
            }
            return View(model);
        }

        #endregion




    }
}
