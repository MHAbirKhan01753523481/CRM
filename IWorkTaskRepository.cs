using ProjectMaking.BaseManager;
using ProjectMaking.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Repository
{
    public interface IWorkTaskRepository : IBaseManager<WorkTask>
    {
        List<WorkTask> GetAllWithWorkTaskData();
        dynamic GetCompleteTask();
        dynamic GetActiveTask();
        dynamic GstAllComments();
       
    }
}
