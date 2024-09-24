using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository; //Null
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork) //Ask CLR To Create Object From departmentRepository
        {
            //_departmentRepository = departmentRepository;
             _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
              await _unitOfWork.DepartmentRepository.AddAsync(model);
            var Count =await _unitOfWork.CompleteAsync();
            if (Count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int? Id , string viewName = "Details")
        {
            if (Id is null) return BadRequest();

            var Department =await _unitOfWork.DepartmentRepository.GetAsync(Id.Value);

            if (Department is null) return NotFound();

            return View(viewName,Department);
        }


        public async Task<IActionResult> Edit(int? id)
        { 

            if (id == null)
            {
                return BadRequest();
            }

            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);


            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Department model)
        {
            if (ModelState.IsValid)
            if (id == null)
            {
 
            }
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Implement the update logic here
                _unitOfWork.DepartmentRepository.Update(model);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null) return BadRequest();
            var department =await _unitOfWork.DepartmentRepository.GetAsync(Id.Value);  

            if (department == null)
            {
                return NotFound();  
            }

            return View(department);  
        }



        //public IActionResult DeleteConfirmed(int Id)
        //{
        //    var department = _departmentRepository.Get(Id);  
        //    if (department == null)
        //    {
        //        return NotFound();  
        //    }

        //    _departmentRepository.Delete(Id);
        //    return RedirectToAction(nameof(Index));  
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? Id, Department model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Delete(model);
                    var Count =await _unitOfWork.CompleteAsync();
                    if (Count>0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch(Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }


    }


}
