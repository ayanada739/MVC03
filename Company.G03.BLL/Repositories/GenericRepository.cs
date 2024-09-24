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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
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
