using ProjectMaking.BaseManager;
using ProjectMaking.Data;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public class TradeRepository:BaseManager<Trade>,ITradeRepository
    {
        private readonly AppDbContext _context;
        public TradeRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
    }
}
