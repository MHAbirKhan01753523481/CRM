using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class Manager:Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<WorkTask> ManagerTasks { get; set; }
    }
}
