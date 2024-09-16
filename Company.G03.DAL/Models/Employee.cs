using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage= "Name is Required!!")]
        public string Name { get; set; }

        [Range(25,60, ErrorMessage ="Age must be between 05 ,60")]
        public int? Age { get; set; }

        [RegularExpression(pattern: @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$" ,
            ErrorMessage ="Address must be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary is Required!!")]
        public decimal Salary { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
