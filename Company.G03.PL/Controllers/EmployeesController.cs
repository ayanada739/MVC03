using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.Helper;
using Company.G03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace Company.G03.PL.Controllers
{
    public class EmployeesController : Controller
    {

        //private readonly IEmployeeRepository _employeeRepository; //Null
        //private readonly IDepartmentRepository _departmentRepository; //Null
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper

            ) //Ask CLR To Create Object From departmentRepository
        {

            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string InputSearch)
        {
            var employees = Enumerable.Empty<Employee>();
             
            if (string.IsNullOrEmpty(InputSearch))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(InputSearch);
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
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
           if(ModelState.IsValid)
            {
                try
                {
                    if( model.Image is not null)
                    {
                        model.ImageName = DocumentSetting.Upload(model.Image, folderName: "images");
                    }


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


                     await _unitOfWork.EmployeeRepository.AddAsync(employee);
                    var Count = await _unitOfWork.CompleteAsync();
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

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id is null) return BadRequest();

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(Id.Value);

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


        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewData["departments"] = departments;


                if (id is null) return BadRequest();

                var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

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
        public async Task<IActionResult> Edit([FromRoute] int? Id, EmployeeViewModel model)
        {
            try
            {
                if (Id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSetting.Delete(model.ImageName, folderName: "images");

                    }
                    if(model.Image is not null)
                    {
                       model.ImageName = DocumentSetting.Upload(model.Image, folderName: "images");

                    }
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
                    _unitOfWork.EmployeeRepository.Update(employee);
                    var Count = await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(Id.Value);

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
        public async Task<IActionResult> Delete([FromRoute] int? Id, EmployeeViewModel model)
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
                     _unitOfWork.EmployeeRepository.Delete(employee);
                    var Count = await _unitOfWork.CompleteAsync();
                    if (Count > 0)
                    {
                        if (model.ImageName is not null)
                        {
                            DocumentSetting.Delete(model.ImageName, folderName: "images");
                        }
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
