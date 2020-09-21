using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class CategoryRepository:BaseManager<Category>,ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
