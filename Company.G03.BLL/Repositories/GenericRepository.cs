using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Repositories
{
     public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context) //ASK CLR Create Object From AppDbContext
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_context.Employees.Include(E => E.WorkFor).ToList();
            }
            return _context.Set<T>().ToList();
        }

        public T Get(int? Id)
        {
            return _context.Set<T>().Find(Id);
        }
        public void Add(T entity)
        {
             _context.Add(entity);
         }

        public void Update(T entity)
        {
            _context.Update(entity);
         }

        public void Delete(T entity)
        {
            _context.Remove(entity);
         }

       

      

       
    }
}
