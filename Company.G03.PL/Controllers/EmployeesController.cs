﻿using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; //Null

        public EmployeesController(IEmployeeRepository employeeRepository) //Ask CLR To Create Object From departmentRepository
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();

            // View's Dictionary:transfer Data From Action To View (One Ways)
            
            //// 1. viewData : Property Inherited From Controller Class,  Dictionary
            //ViewData[index: "Data01"] = "Hello World From ViewData";

            //// 2. ViewBag: Property Inherited From Controller Class, dynamic
            //ViewBag.Data02 = "Hello World From ViewData";

            // 3. tempData: Property Inherited From Controller Class, Dictionary
            // Transfor Data From Request To Another
            //TempData[key:"Data03" ] = "Hello World From TempData"






            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    var Count = _employeeRepository.Add(model);
                    if (Count > 0)
                    {
                        TempData["Messege"] = "Employee Created!";
                    }
                    else
                    {
                        TempData["Messege"] = "Employee Not Created!";

                    }
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message); 
                }
            }
            
            return View(model);
            
        }

        public IActionResult Details(int? Id)
        {
            if (Id is null) return BadRequest();

            var employee = _employeeRepository.Get(Id.Value);

            if (employee is null) return NotFound();

            return View(employee);
        }


        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            var employee = _employeeRepository.Get(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? Id, Employee model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var Count = _employeeRepository.Update(model);
                    if (Count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id is null) return BadRequest();
            var employee = _employeeRepository.Get(Id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
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
        public IActionResult Delete([FromRoute] int? Id, Employee model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var Count = _employeeRepository.Delete(model);
                    if (Count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
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
