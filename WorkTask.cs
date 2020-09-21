using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Mark { get; set; }
        public string Comment { get; set; }
        public string Reply { get; set; }
        public string Requirement { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CompliteTime { get; set; }
       // public DateTime FixedTime { get; set; }
        public Manager Manager { get; set; }
        public int ManagerId { get; set; }
        public SalesMan SalesMan { get; set; }
        public int SalesManId { get; set; }
        public decimal PaidAmmount { get; set; }
        public decimal DewAmmount { get; set; }

        public string IconClass { get; set; }
     
        public string IconColor { get; set; }

    }
}
