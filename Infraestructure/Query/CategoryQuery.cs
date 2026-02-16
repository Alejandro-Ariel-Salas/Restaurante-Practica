using Aplication.Interfaces.IQuery;
using Domain.Entityes;
using Infraestructure.Perssistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class CategoryQuery: ICategoryQuery
    {
        private readonly AppDbContext _context;

        public CategoryQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<List<Category>> GetAllCategorys()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
