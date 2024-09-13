using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repositories
{
    //CLR
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;  

        public DepartmentRepository(AppDbContext context) //ASK CLR Create Object From AppDbContext
        {
            _context = context;
        }
        public IEnumerable<Department> GetAll()
        {
           
            return _context.Department.ToList();
        }

        public Department Get(int? Id)
        {
            //return _context.Department.FirstOrDefault(D => D.Id == Id);
            return _context.Department.Find(Id);

        }


        public int Add(Department entity)
        {
            _context.Department.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Department entity)
        {
            _context.Department.Update(entity);
            return _context.SaveChanges();

        }

        public int Delete(Department entity)
        {
            _context.Department.Remove(entity);
            return _context.SaveChanges();

        }


    }
}
