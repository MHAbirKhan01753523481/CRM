using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class Sehedule
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Pass { get; set; }
        public List<SalesMan> SalesMan { get; set; }
    }
}
