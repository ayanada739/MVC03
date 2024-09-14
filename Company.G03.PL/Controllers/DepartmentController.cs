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
            if(Count > 0)
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
         

    }
}
