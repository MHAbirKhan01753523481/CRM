using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class SeheduleRepository: BaseManager<Sehedule>,ISeheduleRepository
    {
        private readonly AppDbContext _context;
        public SeheduleRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
