using Microsoft.EntityFrameworkCore;
using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class SalesRepository:BaseManager<SalesMan>,ISalesRepository
    {
        private readonly AppDbContext _context;
        public SalesRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public List<SalesMan> GetAllSalesJoinData()
        {
            return _context.SalesMen.Include(c => c.Client).Include(p => p.Products).Include(s => s.Sehedule).ToList();
        }
    }
}
