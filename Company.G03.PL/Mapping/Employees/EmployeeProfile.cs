using AutoMapper;
using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels.Employees;

namespace Company.G03.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
             


        }
    }
}
