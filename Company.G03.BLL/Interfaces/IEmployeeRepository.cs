﻿using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByNameAsync(string name);
        //Employee GetByName(string name);
        //IEnumerable<Employee> GetAll();
        //Employee Get(int? Id);
        //int Add(Employee entity);
        //int Update(Employee department);
        //int Delete(Employee entity);
         
    }
}
