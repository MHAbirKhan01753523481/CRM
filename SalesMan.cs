using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class SalesMan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Pass { get; set; }
        public string Image { get; set; }
        public Client Client { get; set; }
        public int ClientId  { get; set; }
        public Product Products  { get; set; }
        public int ProductId { get; set; }
        public Sehedule Sehedule { get; set; }
        public int SeheduleId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string IconClass { get; set; }
        public string IconColor { get; set; }
        public string Comment { get; set; }
        public double Mark { get; set; }
        public string Replay { get; set; }
        //public Manager Managers { get; set; }
        //public int ManagerId { get; set; }
       public List<WorkTask> ManagerTasks { get; set; }
    }
}
