using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class ProductRepository:BaseManager<Product>,IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
