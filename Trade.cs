using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMaking.Models.EntityModel
{
    public class Trade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Client Client { get; set; }
        public int ClientId  { get; set; }
        public List<Service> Services { get; set; }
    }
}
