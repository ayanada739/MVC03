using Company.G03.BLL;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository; //Null
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork) //Ask CLR To Create Object From departmentRepository
        {
            //_departmentRepository = departmentRepository;
             _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
             _unitOfWork.DepartmentRepository.Add(model);
            var Count = _unitOfWork.Complete();
            if (Count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Details(int? Id , string viewName = "Details")
        {
            if (Id is null) return BadRequest();

            var Department = _unitOfWork.DepartmentRepository.Get(Id.Value);

            if (Department is null) return NotFound();

            return View(viewName,Department);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

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
            if (id == null)
            {
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


        public IActionResult Delete(int? Id)
        {
            if (Id == null) return BadRequest();
            var department = _unitOfWork.DepartmentRepository.Get(Id.Value);  

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
        public IActionResult Delete([FromRoute] int? Id, Department model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    _unitOfWork.DepartmentRepository.Delete(model);
                    var Count = _unitOfWork.Complete();
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
