using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository; //Null

        public DepartmentController(IDepartmentRepository departmentRepository) //Ask CLR To Create Object From departmentRepository
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
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
            var Count = _departmentRepository.Add(model);
            if (Count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Details(int? Id)
        {
            if (Id is null) return BadRequest();

            var Department = _departmentRepository.Get(Id.Value);

            if (Department is null) return NotFound();

            return View(Department);
        }


        public IActionResult Edit(int id)
        {
            var department = _departmentRepository.Get(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        
        public IActionResult Delete(int Id)
        {
            var department = _departmentRepository.Get(Id);  

            if (department == null)
            {
                return NotFound();  
            }

            return View(department);  
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            var department = _departmentRepository.Get(Id);  
            if (department == null)
            {
                return NotFound();  
            }

            _departmentRepository.Delete(Id);
            return RedirectToAction(nameof(Index));  
        }


    }


}
