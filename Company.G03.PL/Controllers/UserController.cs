using Company.G03.DAL.Models;
using Company.G03.PL.Helper;
using Company.G03.PL.ViewModels;
using Company.G03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        // Get, GetAll, Add, Update, Delete
        // Index, Details, Edit, Delete

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string InputSearch)
        {
            var users = Enumerable.Empty<UserViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToListAsync();

            }
            else
            {
                users = await _userManager.Users.Where(U => U.Email
                                  .ToLower()
                                  .Contains(InputSearch.ToLower()))
                                  .Select(U => new UserViewModel()
                                  {
                                      Id = U.Id,
                                      FirstName = U.FirstName,
                                      LastName = U.LastName,
                                      Email = U.Email,
                                      Roles = _userManager.GetRolesAsync(U).Result
                                  }).ToListAsync();
            }


            return View(users);
        }
        public async Task<IActionResult> Details(string? Id, string ViewName = "Details")
        {
            if (Id is null) return BadRequest();

            var userFromDb = await _userManager.FindByIdAsync(Id);
            if (userFromDb is null) return BadRequest();

            var user = new UserViewModel()
            {
                Id = userFromDb.Id,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Email = userFromDb.Email,
                Roles = _userManager.GetRolesAsync(userFromDb).Result
            };

            return View(ViewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, ViewName: "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? Id, UserViewModel model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userFromDb = await _userManager.FindByIdAsync(Id);
                    if (userFromDb is null) return BadRequest();

                    userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;


                    await _userManager.UpdateAsync(userFromDb);
                   
                    return RedirectToAction(nameof(Index));
                
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? Id)
        {
            return await Details(Id, ViewName: "Edit");

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? Id, UserViewModel model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userFromDb = await _userManager.FindByIdAsync(Id);
                    if (userFromDb is null) return BadRequest();

                     

                    await _userManager.DeleteAsync(userFromDb);

                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }

    }
}
