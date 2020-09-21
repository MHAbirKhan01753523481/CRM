using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class ServiceRepository:BaseManager<Service>,IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
