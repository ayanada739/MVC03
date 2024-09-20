using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; //Null
        private readonly IDepartmentRepository _departmentRepository; //Null
        private readonly IMapper _mapper;

        public EmployeesController(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper

            ) //Ask CLR To Create Object From departmentRepository
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }


        public IActionResult Index(string InputSearch)
        {
            var employees = Enumerable.Empty<Employee>();
             
            if (string.IsNullOrEmpty(InputSearch))
            {
                 employees = _employeeRepository.GetAll();

            }
            else
            {
                 employees = _employeeRepository.GetByName(InputSearch);
            }

            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
 
            // View's Dictionary:transfer Data From Action To View (One Ways)
            
            //// 1. viewData : Property Inherited From Controller Class,  Dictionary
            //ViewData[index: "Data01"] = "Hello World From ViewData";

            //// 2. ViewBag: Property Inherited From Controller Class, dynamic
            //ViewBag.Data02 = "Hello World From ViewData";

            // 3. tempData: Property Inherited From Controller Class, Dictionary
            // Transfor Data From Request To Another
            //TempData[key:"Data03" ] = "Hello World From TempData"

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments =_departmentRepository.GetAll();
            ViewData[index: "departments"] = departments;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    //Casting EmployeeViewModel (ViewModel) to Employee (model)
                    //Mapping:
                    //1. Manual Mapping

                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Address = model.Address,
                    //    Name = model.Name,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber
                    //};

                    //2. Auto Mapping

                    var employee = _mapper.Map<Employee>(model);



                    var Count = _employeeRepository.Add(employee);
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

            //Mapping: Employee >> EmployeeViewModel

            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Address = employee.Address,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    WorkFor = employee.WorkFor,
            //    Email = employee.Email,
            //    PhoneNumber = employee.PhoneNumber
            //};

            //Auto Mapping
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }


        public IActionResult Edit(int? id)
        {
            try
            {
                var departments = _departmentRepository.GetAll();
                ViewData[index: "departments"] = departments;


                if (id is null) return BadRequest();

                var employee = _employeeRepository.Get(id.Value);

                if (employee == null)  return NotFound();


                //Mapping: Employee >> EmployeeViewModel
                //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                //{
                //    Id = employee.Id,
                //    Address = employee.Address,
                //    Name = employee.Name,
                //    Salary = employee.Salary,
                //    Age = employee.Age,
                //    HiringDate = employee.HiringDate,
                //    IsActive = employee.IsActive,
                //    WorkFor = employee.WorkFor,
                //    Email = employee.Email,
                //    PhoneNumber = employee.PhoneNumber
                //};
                //Auto Mapping
                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
                return View(employeeViewModel);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(actionName: "Error", controllerName: "Home");
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? Id, EmployeeViewModel model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Address = model.Address,
                    //    Name = model.Name,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber
                    //};

                    //Auto Mapping
                    var employee = _mapper.Map<Employee>(model);
                    var Count = _employeeRepository.Update(employee);
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

            //Mapping: Employee >> EmployeeViewModel
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Address = employee.Address,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    WorkFor = employee.WorkFor,
            //    Email = employee.Email,
            //    PhoneNumber = employee.PhoneNumber
            //};

            //Auto Mapping
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
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
        public IActionResult Delete([FromRoute] int? Id, EmployeeViewModel model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Address = model.Address,
                    //    Name = model.Name,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber
                    //};

                    //Auto Mapping
                    var employee = _mapper.Map<Employee>(model);
                    var Count = _employeeRepository.Delete(employee);
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
