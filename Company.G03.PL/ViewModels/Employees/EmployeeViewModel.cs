using Company.G03.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!!")]
        public string Name { get; set; }


        [RegularExpression(pattern: @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Range(25, 60, ErrorMessage = "Age must be between 05 ,60")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Salary is Required!!")]

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
         public DateTime HiringDate { get; set; }
         public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; }//Navigational Property-Optional
    }
}
