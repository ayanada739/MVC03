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

        public AccountController(UserManager<ApplicationUser> userManager )

        {
            _userManager = userManager;
        }
        //SignUp
        [HttpGet] // /Acount/SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost] // /Acount/SignUp
        public async Task<IActionResult> SignUpAsync(SignUpViewModel model)
        {
            if(ModelState.IsValid)
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
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message );

                }
            }
            return View(model);
        }
    }
}
