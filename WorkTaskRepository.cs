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
    public class WorkTaskRepository:BaseManager<WorkTask>,IWorkTaskRepository
    {
        private readonly AppDbContext _context;
        public WorkTaskRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

     
        public dynamic GetActiveTask()
        {
           return _context.ManagerTasks.ToList().Where(x => x.Reply != null).Count();
        }

        public List<WorkTask> GetAllWithWorkTaskData()
        {
            return _context.ManagerTasks.Include(m => m.Manager).Include(s => s.SalesMan).ToList();
        }

        public dynamic GetCompleteTask()
        {
            return _context.ManagerTasks.ToList().Where(x => x.Comment != null && x.Mark.ToString() != null).Count();
        }

        public dynamic GstAllComments()
        {
          return _context.ManagerTasks.ToList().Where(x => x.Comment != null).Count();
        }
    }
}
