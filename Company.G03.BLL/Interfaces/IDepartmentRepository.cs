using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department Get(int? Id);
        int Add(Department entity);
        void Update(Department department);
        int Delete(Department entity);
        void Delete(int id);
    }
}
